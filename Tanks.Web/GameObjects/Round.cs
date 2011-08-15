using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using System.Collections.Generic;
using Tanks.Web.Maps;

namespace Tanks.Web.GameObjects
{
    public abstract class Round
    {
        int shotsFired;
        int vehiclesHit;
        int projectilesHit;
        int powerupsHit;
        int targetsKilled;
        int hitsTaken;
        int score;
        int enemiesFlippedOver;
        protected int powerupInterval;
        protected int powerupIntervalCount = 0;
        protected bool enemySingleShot;
        protected bool over;
        protected bool pass;
        protected int enemyCount;
        protected int enemyRateOfFireBonus;
        protected Vehicle userTank;
        protected List<Vehicle> enemies;
        protected MapDefinition map;
        protected Wall rightWall;
        protected Wall leftWall;
        protected Ceiling ceiling;
        protected List<Vehicle> vehicleList;
        protected List<Projectile> projectileList;
        protected List<Powerup> powerups;
        protected Wind wind;
        protected Single enemyPercentError;
        protected PhysicsSimulator simulator;
        protected object initializationData;
        protected Game game;

        public Round(PhysicsSimulator simulator, Game game)
        {
            this.simulator = simulator;
            this.game = game;
            this.Construct();
        }

        public Round(PhysicsSimulator simulator, Game game, object initializationData)
        {
            this.game = game;
            this.simulator = simulator;
            this.initializationData = initializationData;
            this.Construct();
        }

        void Construct()
        {
            this.powerupInterval = 280;
            Page.SceneLoop.Update += new EventHandler<SceneLoopEventArgs>(sceneLoop_Update);
            this.enemyCount = 1;
            this.enemyRateOfFireBonus = 0;
            this.enemyPercentError = .15f;
            this.enemySingleShot = true;
            this.vehicleList = new List<Vehicle>();
            this.projectileList = new List<Projectile>();
            this.powerups = new List<Powerup>();
            this.wind = new Wind();
            this.userTank = new Vehicle(simulator, this);
            this.userTank.HitPoints = 200;
            this.userTank.FiredShot += new EventHandler<FiredShotEventArgs>(userTank_FiredShot);
            this.userTank.HitTarget += new EventHandler<HitTargetEventArgs>(userTank_HitTarget);
            this.userTank.HitProjectile += new EventHandler<VehicleEventArgs>(userTank_HitProjectile);
            this.userTank.HitPowerup += new EventHandler<VehicleEventArgs>(userTank_HitPowerup);
            this.userTank.ProjectileCreated += new EventHandler<ProjectileEventArgs>(userTank_ProjectileCreated);
            this.userTank.TookHit += new EventHandler<VehicleEventArgs>(userTank_TookHit);
            this.userTank.RateOfFireBonus = .5;
            this.userTank.AllowedToFire = true;
            this.userTank.PercentError = .15d;
            this.userTank.ControlledByUser = true;
            this.userTank.Color = Colors.Orange;
            this.map = this.CreateMap(simulator);
            this.ceiling = new Ceiling(simulator);
            this.rightWall = this.CreateRightWall(simulator);
            this.leftWall = this.CreateLeftWall(simulator);
            this.vehicleList.Add(this.userTank);
            this.Initialize(simulator);
        }

        public int EnemiesFlippedOver
        {
            get { return this.enemiesFlippedOver; }
        }

        public int PowerupsHit
        {
            get { return this.powerupsHit; }
        }

        public int ProjectilesHit
        {
            get { return this.projectilesHit; }
        }

        public int ShotsFired
        {
            get { return this.shotsFired; }
        }

        public int VehiclesHit
        {
            get { return this.vehiclesHit; }
        }

        public int HitsTaken
        {
            get { return this.hitsTaken; }
        }

        public int Kills
        {
            get { return this.targetsKilled; }
        }

        public bool Pass
        {
            get { return this.pass; }
        }

        public bool Over
        {
            get { return this.over; }
        }

        public List<Vehicle> VehicleList
        {
            get { return this.vehicleList; }
        }

        public Vehicle UserTank
        {
            get { return this.userTank; }
        }

        public List<Vehicle> Enemies
        {
            get { return this.enemies; }
        }

        public Ceiling Ceiling
        {
            get { return this.ceiling; }
        }

        public MapDefinition Map
        {
            get { return this.map; }
        }

        public Wall RightWall
        {
            get { return this.rightWall; }
        }

        public Wind Wind
        {
            get { return this.wind; }
        }

        public Wall LeftWall
        {
            get { return this.leftWall; }
        }

        public int Score
        {
            get { return this.score; }
            set
            {
                this.score = value;
                if (ScoreChanged != null)
                {
                    this.ScoreChanged(this, new RoundEventArgs(this));
                }
            }
        }

        public abstract Round GetNextRound();

        protected virtual void PlaceVehicles(Vehicle userVehicle, List<Vehicle> enemyVehicles)
        {
            userVehicle.Position = new Vector2(100, 600);
            foreach (Vehicle vehicle in enemyVehicles)
            {
                int x = Randomizer.Random.Next(300, 800);
                vehicle.Position = new Vector2(x, 600);
            }
        }

        protected virtual MapDefinition CreateMap(PhysicsSimulator simulator)
        {
            return new MapADefinition(simulator);
            //return MapCache.Instance.GetMap("MapA", simulator);
        }

        protected virtual Wall CreateRightWall(PhysicsSimulator simulator)
        {
            Wall wall = new Wall(simulator);
            wall.Position = new Vector2(Screen.Width - 10, Screen.Height / 2);
            return wall;
        }

        protected virtual Wall CreateLeftWall(PhysicsSimulator simulator)
        {
            Wall wall = new Wall(simulator);
            wall.Position = new Vector2(10, Screen.Height / 2);
            return wall;
        }

        protected virtual void Initialize(PhysicsSimulator simulator)
        {
            this.LoadEnemies(simulator);
            this.PlaceVehicles(this.userTank, this.Enemies);
        }

        protected virtual void LoadEnemies(PhysicsSimulator simulator)
        {
            this.enemies = new List<Vehicle>();

            for (int i = 1; i <= this.enemyCount; i++)
            {
                EnemyVehicle enemy = new EnemyVehicle(simulator, this);
                enemy.ProjectileCreated += new EventHandler<ProjectileEventArgs>(userTank_ProjectileCreated);
                enemy.FlippedOver += new EventHandler<VehicleEventArgs>(enemy_FlippedOver);
                enemy.RateOfFireBonus = this.enemyRateOfFireBonus;
                enemy.AllowedToFire = true;
                enemy.PercentError = this.enemyPercentError;
                enemy.RandomizeStartingFiring();
                this.enemies.Add(enemy);
                this.vehicleList.Add(enemy);
            }
        }

        void FloatPowerup(Powerup powerup)
        {
            powerup.Body.ApplyForce(new Vector2(0, -2570));
        }

        void End()
        {
            Page.SceneLoop.Update -= sceneLoop_Update;
            this.over = true;

            if (this.pass)
            {
                int percent = (int)((float)this.vehiclesHit / (float)this.shotsFired * 100f);
                this.Score = this.Score + percent * Scoring.PointsPerShotPercent;
                //this.Score = this.Score - this.HitsTaken * Scoring.PointsPerHitAgainst;
                this.Score = this.Score + Scoring.PointsPerRound;
            }

            if (this.RoundOver != null)
            {
                this.RoundOver(this, new RoundEventArgs(this));
            }
        }

        protected void RegisterPowerup(Powerup powerup)
        {
            this.powerups.Add(powerup);
            powerup.PowerupAcquired += new EventHandler<PowerupAcquiredEventArgs>(powerup_PowerupAcquired);
            if (this.PowerupCreated != null)
            {
                this.PowerupCreated(this, new PowerupEventArgs(powerup));
            }
        }

        void powerup_PowerupAcquired(object sender, PowerupAcquiredEventArgs e)
        {
            if (this.PowerupAcquired != null)
            {
                this.PowerupAcquired(this, e);
            }
        }

        void enemy_FlippedOver(object sender, VehicleEventArgs e)
        {
            this.enemiesFlippedOver++;
            this.Score = this.Score + Scoring.PointsPerFlippedEnemy;
        }

        void userTank_TookHit(object sender, VehicleEventArgs e)
        {
            this.hitsTaken++;
            this.Score = this.Score + Scoring.PointsPerHitAgainst;
        }

        void userTank_HitTarget(object sender, HitTargetEventArgs e)
        {
            if (e.Target != e.Source)
            {
                this.vehiclesHit++;
                this.Score = this.Score + Scoring.PointsPerVehicleHit;


                if (e.Target.IsDead)
                {
                    this.targetsKilled++;
                    this.Score = this.Score + Scoring.PointsPerKill;
                }
            }
        }

        void userTank_HitProjectile(object sender, VehicleEventArgs e)
        {
            this.projectilesHit++;
            this.Score = this.Score + Scoring.PointsPerProjectileHit;
        }

        void userTank_HitPowerup(object sender, VehicleEventArgs e)
        {
            this.powerupsHit++;
            this.Score = this.Score + Scoring.PointsPerPowerupHit;
        }

        void userTank_FiredShot(object sender, FiredShotEventArgs e)
        {
            this.shotsFired++;
        }

        protected virtual void userTank_ProjectileCreated(object sender, ProjectileEventArgs e)
        {
            e.Projectile.Exploded += new EventHandler<ProjectileExplodedEventArgs>(projectile_Exploded);
            this.projectileList.Add(e.Projectile);
        }

        protected virtual void projectile_Exploded(object sender, ProjectileExplodedEventArgs e)
        {
            this.projectileList.Remove(e.Projectile);
        }

        protected virtual void sceneLoop_Update(object sender, SceneLoopEventArgs e)
        {
            foreach (Vehicle item in this.vehicleList)
            {
                item.Update();
            }

            foreach (Projectile projectile in this.projectileList)
            {
                this.wind.Blow(projectile);
            }

            foreach (Powerup powerup in this.powerups)
            {
                this.FloatPowerup(powerup);
            }

            this.ProcessPowerupInterval();

            bool isOverNow = false;
            if (this.userTank.IsDead)
            {
                isOverNow = true;
                pass = false;
            }
            else
            {
                bool flag = true;
                foreach (EnemyVehicle item in this.Enemies)
                {
                    if (!item.IsDead)
                    {
                        flag = false;
                        break;
                    }

                }
                if (flag)
                {
                    isOverNow = true;
                    pass = true;
                }
            }

            if (isOverNow)
            {
                this.End();
            }
        }

        void ProcessPowerupInterval()
        {
            this.powerupIntervalCount++;
            if (this.powerupIntervalCount > this.powerupInterval)
            {
                this.CreateNextPowerup();
            }
        }

        protected virtual void CreateNextPowerup()
        {
            this.powerupIntervalCount = 0;
            int random = Randomizer.Random.Next(0, 100);
            Powerup powerup = null;

            if (random > 88)
            {
                powerup = new BurstPowerup(this.simulator);
            }
            else if (random > 75)
            {
                if (this.game.Player.HasLlama)
                {
                    this.CreateNextPowerup();
                    return;
                }
                powerup = new LlamaGunPowerup(this.simulator);
            }
            else if (random > 63)
            {
                powerup = new RateOfFirePowerup(this.simulator);
            }
            else if (random > 50)
            {
                if (this.game.Player.HasUzi)
                {
                    this.CreateNextPowerup();
                    return;
                }
                powerup = new UziPowerup(this.simulator);
            }
            else if (random > 38)
            {
                powerup = new DamagePowerup(this.simulator);
            }
            else if (random > 25)
            {
                powerup = new MaxHealthPowerup(this.simulator);
            }
            else if (random > 12)
            {
                powerup = new HealthPowerup(this.simulator);
            }
            else
            {
                powerup = new AccuracyPowerup(this.simulator);
            }

            float x = (float)Randomizer.Random.Next(100, Screen.Width - 100);
            powerup.Position = new Vector2(x, 100);
            this.RegisterPowerup(powerup);
        }



        public event EventHandler<RoundEventArgs> RoundOver;
        public event EventHandler<RoundEventArgs> ScoreChanged;
        public event EventHandler<PowerupEventArgs> PowerupCreated;
        public event EventHandler<PowerupAcquiredEventArgs> PowerupAcquired;
    }
}
