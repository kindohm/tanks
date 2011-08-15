using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Tanks.Web.GameObjects;

namespace Tanks.Web.UI
{
    public partial class InfoPanel : UserControl
    {
        Game game;

        public InfoPanel()
        {
            InitializeComponent();
        }

        public InfoPanel(Game game)
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(InfoPanel_Loaded);
            this.game = game;
        }

        void InfoPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.game.RoundChanged += new EventHandler<GameEventArgs>(game_RoundChanged);
            this.game.ScoreChanged += new EventHandler<GameEventArgs>(game_ScoreChanged);
            if (this.game.CurrentRound != null)
            {
                this.SetUpRound(this.game.CurrentRound);
            }
        }

        void SetUpRound(Round round)
        {
            //round.Wind.Changed += new EventHandler<WindEventArgs>(Wind_Changed);
            round.UserTank.MaxHitPointsChanged += new EventHandler<HitPointsChangedEventArgs>(UserTank_MaxHitPointsChanged);
            round.UserTank.RateOfFireBonusChanged += new EventHandler(UserTank_RateOfFireBonusChanged);
            round.UserTank.DamageBonusChanged += new EventHandler(UserTank_DamageBonusChanged);
            round.UserTank.AccuracyChanged += new EventHandler<VehicleEventArgs>(UserTank_AccuracyChanged);
            this.UserTank_MaxHitPointsChanged(null, null);
            this.game_ScoreChanged(null, null);
            this.UserTank_RateOfFireBonusChanged(null, null);
            this.UserTank_DamageBonusChanged(null, null);
            this.UserTank_AccuracyChanged(null, null);
           // this.Wind_Changed(null, null);
        }

        void UserTank_AccuracyChanged(object sender, VehicleEventArgs e)
        {
            this.accuracyBlock.Text = this.game.CurrentRound.UserTank.PercentError.ToString("##.0%");
        }

        void UserTank_DamageBonusChanged(object sender, EventArgs e)
        {
            int count = 0;
            foreach (Powerup powerup in game.Player.Powerups)
            {
                if (powerup is DamagePowerup)
                {
                    count++;
                }
            }
            this.damageBlock.Text = "+" + count.ToString();
        }

        void UserTank_RateOfFireBonusChanged(object sender, EventArgs e)
        {
            int count = 0;
            foreach (Powerup powerup in game.Player.Powerups)
            {
                if (powerup is RateOfFirePowerup)
                {
                    count++;
                }
            }
            this.rateOfFireBlock.Text = "+" + count.ToString();
        }

        void UserTank_MaxHitPointsChanged(object sender, HitPointsChangedEventArgs e)
        {
            this.maxHPBlock.Text = this.game.CurrentRound.UserTank.MaxHitPoints.ToString();
        }

        void game_RoundChanged(object sender, GameEventArgs e)
        {
            this.SetUpRound(this.game.CurrentRound);
        }

        void game_ScoreChanged(object sender, GameEventArgs e)
        {
            this.scoreBlock.Text = this.game.Score.ToString("###,###,###");
        }

       
    }
}
