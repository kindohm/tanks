using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using FarseerGames.FarseerPhysics.Dynamics;
using Tanks.Web.UI;
using Tanks.Web.GameObjects;
using Tanks.Web.Maps;

namespace Tanks.Web
{
    public class TankView : Canvas
    {
        bool waitForNewRound;
        int newRoundWaitCount;
        int roundCount;
        double leftoverUpdateTime;
        Round round;
        Game game;
        ControlPanel controlPanel;
        InfoPanel infoPanel;
        Arrow arrow;
        protected PhysicsSimulator simulator;
        protected List<IDrawingBrush> drawingList;
        protected Canvas viewerCanvas;
        public static MouseHandler MouseHandler;
        ScaleTransform scale;

        public TankView()
        {
            Page.SceneLoop.Update += new EventHandler<SceneLoopEventArgs>(sceneLoop_Update);
            Page.Keyhandler.KeyPressed += new EventHandler<KeyEventArgs>(Keyhandler_KeyPressed);

            scale = new ScaleTransform();
            scale.ScaleX = 1;
            scale.ScaleY = 1;
            this.RenderTransform = scale;



            this.drawingList = new List<IDrawingBrush>();

            viewerCanvas = new Canvas();
            viewerCanvas.Height = Convert.ToDouble(Screen.Height);
            viewerCanvas.Width = Convert.ToDouble(Screen.Width);
            viewerCanvas.IsHitTestVisible = true;
            this.Children.Add(viewerCanvas);

            this.LoadBackground();
            this.Initialize();
        }

        public void Initialize()
        {
            TankView.MouseHandler = new MouseHandler();
            TankView.MouseHandler.Attach(this);

            Splash splash = new Splash();
            splash.NewGameClicked += new EventHandler(splash_NewGameClicked);
            this.viewerCanvas.Children.Add(splash);
        }

        void splash_NewGameClicked(object sender, EventArgs e)
        {
            Splash splash = sender as Splash;
            splash.NewGameClicked -= splash_NewGameClicked;

            this.viewerCanvas.Children.Remove(splash);
            this.InitializeGame();
        }

        public void InitializeGame()
        {
            this.roundCount = 0;
            this.newRoundWaitCount = 0;
            this.simulator = new PhysicsSimulator(new Vector2(0,1700));
            this.game = new Game(this.simulator);
            this.game.GameOver += new EventHandler<GameEventArgs>(game_GameOver);

            this.LoadControlPanel();
            this.LoadInfoPanel();

            this.LoadNextRound();
            foreach (IDrawingBrush b in this.drawingList)
            {
                b.Update();
            }
        }

        void LoadControlPanel()
        {
            controlPanel = new ControlPanel();
            this.viewerCanvas.Children.Add(controlPanel);
            controlPanel.SetValue(Canvas.LeftProperty, 15d);
            controlPanel.SetValue(Canvas.TopProperty, 15d);
            controlPanel.SetValue(Canvas.ZIndexProperty, 1000);
        }

        void LoadInfoPanel()
        {
            infoPanel = new InfoPanel(this.game);
            this.viewerCanvas.Children.Add(infoPanel);
            infoPanel.SetValue(Canvas.LeftProperty, Screen.Width - infoPanel.Width - 15d);
            infoPanel.SetValue(Canvas.TopProperty, 15d);
            infoPanel.SetValue(Canvas.ZIndexProperty, 1000);
        }

        void LoadWeaponsPanel()
        {
            WeaponDisplay weaponDisplay = new WeaponDisplay(this.game.CurrentRound.UserTank);
            this.viewerCanvas.Children.Add(weaponDisplay);
            weaponDisplay.SetValue(Canvas.LeftProperty, 15d);
            weaponDisplay.SetValue(Canvas.TopProperty, 150d);
            weaponDisplay.SetValue(Canvas.ZIndexProperty, 1000);
        }

        void LoadWindArrow()
        {
            arrow = new Arrow(this.round.Wind);
            this.viewerCanvas.Children.Add(arrow);
            arrow.SetValue(Canvas.LeftProperty, (double)Screen.Center.X);
            arrow.SetValue(Canvas.TopProperty, 55d);
            arrow.SetValue(Canvas.ZIndexProperty, 1000);
        }

        void LoadBackground()
        {
            LinearGradientBrush brush = new LinearGradientBrush();
            GradientStop stop = new GradientStop();
            stop.Color = Color.FromArgb(255, 33, 33, 33);
            stop.Offset = 0;
            brush.GradientStops.Add(stop);
            stop = new GradientStop();
            stop.Color = Color.FromArgb(255, 66, 66, 66);
            stop.Offset = 1;
            brush.GradientStops.Add(stop);
            viewerCanvas.Background = brush;
        }

        void NewRoundWait()
        {
            if (this.waitForNewRound)
            {
                this.newRoundWaitCount++;
                if (this.newRoundWaitCount > 200)
                {
                    this.waitForNewRound = false;
                    this.newRoundWaitCount = 0;
                    this.ProcessEndOfRound();
                }
            }
        }

        void ProcessEndOfRound()
        {
            this.simulator.Clear();

            ScoringSummary summary = new ScoringSummary();
            summary.Closed += new EventHandler(summary_Closed);
            summary.Load(this.game.CurrentRound);
            this.viewerCanvas.Children.Add(summary);
            summary.SetValue(Canvas.LeftProperty, (double)Screen.Center.X);
            summary.SetValue(Canvas.TopProperty, (double)Screen.Center.Y);
        }


        void LoadGameOver()
        {
            GameOver gameOver = new GameOver(this.game);
            gameOver.NewGameClicked += new EventHandler(gameOver_NewGameClicked);
            this.viewerCanvas.Children.Add(gameOver);
            gameOver.SetValue(Canvas.TopProperty, (double)Screen.Center.Y);
            gameOver.SetValue(Canvas.LeftProperty, (double)Screen.Center.X);
        }


        void LoadNextRound()
        {
            roundCount++;
            RoundTitle title = new RoundTitle("Round " + roundCount.ToString());
            title.Closed += new EventHandler(title_Closed);
            this.viewerCanvas.Children.Add(title);
            title.SetValue(Canvas.TopProperty, (double)Screen.Center.Y);
            title.SetValue(Canvas.LeftProperty, (double)Screen.Center.X);
            title.SetValue(Canvas.ZIndexProperty, 2000);
        }

        void End()
        {
            this.simulator.Clear();
            this.simulator.Dispose();
            if (this.Ended != null)
            {
                this.Ended(this, EventArgs.Empty);
            }
        }

        public void ClearCanvas()
        {
            for (int i = this.viewerCanvas.Children.Count - 1; i >= 0; i--)
            {
                UIElement child = this.viewerCanvas.Children[i];
                if (child is InfoPanel == false &
                    child is ControlPanel == false)
                {
                    this.viewerCanvas.Children.Remove(child);
                }
            }
        }

        public void ClearCanvas(bool all)
        {
            if (all)
            {
                this.viewerCanvas.Children.Clear();
            }
            else
            {
                this.ClearCanvas();
            }
        }

        void InitializeRound()
        {
            this.drawingList.Clear();
            this.ClearCanvas();
            this.controlPanel.Reset();

            this.drawingList.Add(this.round.Map.Brush);
            this.viewerCanvas.Children.Add(this.round.Map.MapControl);

            this.drawingList.Add(round.UserTank.Brush);
            this.drawingList.Add(round.RightWall.Brush);
            this.drawingList.Add(round.LeftWall.Brush);
            this.drawingList.Add(round.Ceiling.Brush);

            this.round.UserTank.Died += new EventHandler<VehicleEventArgs>(Tank_Died);

            this.viewerCanvas.Children.Add(round.Ceiling.Brush.GetBrush());
            this.viewerCanvas.Children.Add(round.RightWall.Brush.GetBrush());
            this.viewerCanvas.Children.Add(round.LeftWall.Brush.GetBrush());
            this.viewerCanvas.Children.Add(round.UserTank.Brush.GetBrush());

            this.InitializeEnemies();

            this.round.UserTank.ProjectileCreated += new EventHandler<ProjectileEventArgs>(UserTank_ProjectileCreated);
            this.LoadWeaponsPanel();
            this.LoadWindArrow();
        }

        void InitializeEnemies()
        {
            foreach (Vehicle enemy in this.round.Enemies)
            {
                enemy.ProjectileCreated += new EventHandler<ProjectileEventArgs>(UserTank_ProjectileCreated);
                enemy.Color = Colors.Brown;
                enemy.Died += new EventHandler<VehicleEventArgs>(Tank_Died);
                this.drawingList.Add(enemy.Brush);
                this.viewerCanvas.Children.Add(enemy.Brush.GetBrush());
            }
        }

        public void Update()
        {
            if (this.round != null)
            {
                Vector2 vector = new Vector2((float)TankView.MouseHandler.MousePosition.X - this.round.UserTank.Position.X,
                    ((float)TankView.MouseHandler.MousePosition.Y - this.round.UserTank.Position.Y));
                vector = Vector2.Normalize(vector);

                if (TankView.MouseHandler.IsMousePressed)
                {
                    this.round.UserTank.TryShoot(vector, (int)controlPanel.powerSlider.Value * 1700);
                }

                if (Page.Keyhandler.IsKeyPressed(Key.F))
                {
                    this.round.UserTank.MoveRight();
                }
                else if (Page.Keyhandler.IsKeyPressed(Key.S))
                {
                    this.round.UserTank.MoveLeft();
                }

                
            }

            if (Page.Keyhandler.IsKeyPressed(Key.P))
            {
                this.Enlarge();
            }
            else if (Page.Keyhandler.IsKeyPressed(Key.O))
            {
                this.Shrink();
            }


            this.NewRoundWait();

        }

        void Enlarge()
        {
            if (this.scale.ScaleX < 2d)
            {
                this.scale.ScaleX = this.scale.ScaleX + .01;
                this.scale.ScaleY = this.scale.ScaleY + .01;
            }
        }

        void Shrink()
        {
            if (this.scale.ScaleX > .1d)
            {
                this.scale.ScaleX = this.scale.ScaleX - .01;
                this.scale.ScaleY = this.scale.ScaleY - .01;
            }
        }

        void ChangeWeapon()
        {
            this.round.UserTank.SwitchWeapon();
        }

        void Keyhandler_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                this.ChangeWeapon();
            }
        }

        void round_RoundOver(object sender, RoundEventArgs e)
        {
            this.newRoundWaitCount = 0;
            this.waitForNewRound = true;
        }

        void title_Closed(object sender, EventArgs e)
        {
            this.viewerCanvas.Children.Remove(sender as RoundTitle);

            this.round = game.GetNextRound();
            if (this.round != null)
            {
                this.round.RoundOver += new EventHandler<RoundEventArgs>(round_RoundOver);
                this.round.PowerupCreated += new EventHandler<PowerupEventArgs>(round_PowerupCreated);
                this.InitializeRound();
            }
        }

        void round_PowerupCreated(object sender, PowerupEventArgs e)
        {
            e.Powerup.PowerupExpired += new EventHandler(Powerup_PowerupExpired);
            this.viewerCanvas.Children.Add(e.Powerup.Brush.GetBrush());
            this.drawingList.Add(e.Powerup.Brush);
        }

        void Powerup_PowerupExpired(object sender, EventArgs e)
        {
            Powerup powerup = sender as Powerup;
            this.drawingList.Remove(powerup.Brush);
        }

        void Tank_Died(object sender, VehicleEventArgs e)
        {
            if (e.Vehicle != this.round.UserTank)
            {
                this.drawingList.Remove(e.Vehicle.Brush);
            }
        }

        void UserTank_ProjectileCreated(object sender, ProjectileEventArgs e)
        {
            if (e.Projectile.Brush is ProjectileBrush)
            {
                (e.Projectile.Brush as ProjectileBrush).Exploded += new EventHandler(TankView_Exploded);
            }
            this.drawingList.Add(e.Projectile.Brush);
            this.viewerCanvas.Children.Add(e.Projectile.Brush.GetBrush());
        }

        void TankView_Exploded(object sender, EventArgs e)
        {
            ProjectileBrush brush = sender as ProjectileBrush;
            this.drawingList.Remove(brush);
            this.viewerCanvas.Children.Remove(brush);
        }

        void sceneLoop_Update(object sender, SceneLoopEventArgs e)
        {
            double secs = e.ElapsedTime.TotalSeconds + leftoverUpdateTime;
            while (secs > .01)
            {
                this.Update();
                if (this.simulator != null)
                {
                    simulator.Update(.01f);
                }
                if (this.drawingList != null)
                {
                    foreach (IDrawingBrush b in drawingList)
                    {
                        b.Update();
                    }
                }
                secs -= .01;
            }
            leftoverUpdateTime = secs;
        }

        void game_GameOver(object sender, GameEventArgs e)
        {
            this.End();
        }

        void summary_Closed(object sender, EventArgs e)
        {
            this.viewerCanvas.Children.Remove(sender as ScoringSummary);
            if (!this.game.Over)
            {
                this.LoadNextRound();
            }
            else
            {
                this.LoadGameOver();
            }
        }

        void gameOver_NewGameClicked(object sender, EventArgs e)
        {
            this.ClearCanvas(true);
            this.Initialize();
        }

        public event EventHandler Ended;
    }
}
