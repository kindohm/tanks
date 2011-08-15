using System;
using System.Diagnostics;
using System.Net;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using FarseerGames.FarseerPhysics.Dynamics;
using Tanks.Web.UI;

namespace Tanks.Web.GameObjects
{
    public class Vehicle : BaseGameObject
    {
        protected double rateOfFireBonus;
        protected int ellapsedFire;
        int hitPoints;
        int maxHitPoints;
        int ellapsedFlipCheck;
        int flipCheckInterval;
        double damageBonus;
        bool flipped;
        Vector2 size;
        public bool IsDead { get; set; }
        public Geom Geometry { get; set; }
        TankBrush brush;
        protected bool readyToFire;
        protected Round round;
        bool allowedToFire;
        double percentError;
        bool controlledByUser;
        float movementForce = 10000f;
        ObservableCollection<Weapon> weapons;
        Weapon currentWeapon;
        int weaponIndex;
        bool bursting;
        int burstingInterval = 100;
        int burstingCount = 0;
        double burstBonus = 4d;

        public Vehicle(PhysicsSimulator simulator, Round round)
            : base(simulator)
        {
            this.flipCheckInterval = 50;
            this.round = round;
            this.IsDead = false;
            this.MaxHitPoints = 100;
            this.HitPoints = this.MaxHitPoints;
            this.Geometry = null;
            this.Initialize();
        }

        public bool Bursting
        {
            get { return this.bursting; }
            set {
                bool oldValue = this.bursting;
                this.bursting = value;
                if (this.bursting & oldValue == false)
                {
                    this.rateOfFireBonus = this.rateOfFireBonus * this.burstBonus;
                    foreach (Weapon weapon in this.weapons)
                    {
                        weapon.LowestRateOfFire = weapon.LowestRateOfFire / 2d;
                    }
                }
                else if (!this.bursting & oldValue == true)
                {
                    this.rateOfFireBonus = this.rateOfFireBonus / this.burstBonus;
                    foreach (Weapon weapon in this.weapons)
                    {
                        weapon.LowestRateOfFire = weapon.LowestRateOfFire * 2d;
                    }
                }
            }
        }

        public bool Flipped
        {
            get { return this.flipped; }
        }

        public Weapon CurrentWeapon
        {
            get { return this.currentWeapon; }
        }

        public ObservableCollection<Weapon> Weapons
        {
            get { return this.weapons; }
            set { this.weapons = value; }
        }

        public bool ControlledByUser
        {
            get { return this.controlledByUser; }
            set {
                bool oldValue = this.controlledByUser;
                this.controlledByUser = value;
                if (oldValue != this.controlledByUser)
                {
                    this.OnControlledByUserChanged();
                }
            }
        }

        public double DamageBonus
        {
            get { return this.damageBonus; }
            set { 
                this.damageBonus = value;
                if (this.damageBonus > 10)
                {
                    this.damageBonus = 10;
                }
                if (this.DamageBonusChanged != null)
                {
                    this.DamageBonusChanged(this, EventArgs.Empty);
                }
            }
        }

        public double WeaponCharge
        {
            get
            {
                return (double)this.ellapsedFire / (double)this.GetEllapsedFireMax();
            }
        }

        public double RateOfFireBonus
        {
            get { return this.rateOfFireBonus; }
            set { 
                this.rateOfFireBonus = value;
                if (this.RateOfFireBonusChanged != null)
                {
                    this.RateOfFireBonusChanged(this, EventArgs.Empty);
                }
            }
        }

        public override IDrawingBrush Brush
        {
            get { return this.brush; }
        }

        public bool AllowedToFire
        {
            get { return this.allowedToFire; }
            set { this.allowedToFire = value; }
        }

        public int MaxHitPoints
        {
            get { return this.maxHitPoints; }
            set { 
                int oldValue = this.maxHitPoints;
                this.maxHitPoints = value;
                if (this.maxHitPoints > 10000)
                {
                    this.maxHitPoints = 10000;
                }
                this.OnMaxHitPointsChanged(this.maxHitPoints - oldValue);
            }
        }
      
        public int HitPoints
        {
            get { return this.hitPoints; }
            set {
                int oldValue = this.hitPoints;
                if (value <= this.MaxHitPoints)
                {
                    this.hitPoints = value;
                    if (this.hitPoints < 0)
                    {
                        this.hitPoints = 0;
                    }
                    this.OnHitPointsChanged(this.hitPoints - oldValue);
                }
            }
        }

        public Vector2 Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        public Color Color
        {
            get { return this.brush.Color; }
            set { this.brush.Color = value; }
        }

        public double PercentError
        {
            get { return this.percentError; }
            set { 
                this.percentError = value;
                if (this.percentError < .005)
                {
                    this.percentError = .005;
                }
                if (this.AccuracyChanged != null)
                {
                    this.AccuracyChanged(this, new VehicleEventArgs(this));
                }
            }
        }

        public bool ReadyToFire
        {
            get { return this.readyToFire; }
            set { this.readyToFire = value; }
        }

        void Initialize()
        {

            this.Size = new Vector2(100, 50);
            this.Body = this.GetBody();
            this.Geometry = this.GetGeom(this.Body);

            this.brush = new TankBrush(this);
            this.brush.Extender.Body = this.Body;
            this.brush.Size = this.Size;
            this.brush.Color = Colors.Brown;

            this.ellapsedFire = 1;
            this.readyToFire = true;
            this.weapons = new ObservableCollection<Weapon>();
            this.weapons.Add(new Cannon(this.simulator));
            this.weaponIndex = int.MinValue;
            this.SwitchWeapon();
            
        }

        Weapon GetNextWeapon()
        {
            if (this.weaponIndex == int.MinValue | this.weaponIndex >= this.weapons.Count - 1)
            {
                this.weaponIndex = 0;
            }
            else
            {
                this.weaponIndex++;
            }

            return this.weapons[this.weaponIndex];
        }

        public void SwitchWeapon()
        {
            this.currentWeapon = this.GetNextWeapon();
            if (this.WeaponChanged != null)
            {
                this.WeaponChanged(this, EventArgs.Empty);
            }
        }

        public void ResetHitPoints()
        {
            this.HitPoints = this.MaxHitPoints;
        }

        public void MoveRight()
        {
            Vector2 movement = new Vector2(this.movementForce, 0);
            Vector2 point = new Vector2(100, 50);
            this.Body.ApplyForceAtLocalPoint(ref movement, ref point);
        }

        public void MoveLeft()
        {
            Vector2 movement = new Vector2(-this.movementForce, 0);
            Vector2 point = new Vector2(0, 50);
            this.Body.ApplyForceAtLocalPoint(ref movement, ref point);

        }

        Body GetBody()
        {
            float mass = 5;

            Vertices vertices = new Vertices();
            vertices.Add(new Vector2(0, 50));
            vertices.Add(new Vector2(100, 50));
            vertices.Add(new Vector2(100, 35));
            vertices.Add(new Vector2(80, 35));
            vertices.Add(new Vector2(80, 0));
            vertices.Add(new Vector2(20, 0));
            vertices.Add(new Vector2(20, 35));
            vertices.Add(new Vector2(0, 35));
            vertices.SubDivideEdges(10);
            Body newBody = BodyFactory.Instance.CreatePolygonBody(this.simulator, vertices, mass);
            return newBody;
        }

        Geom GetGeom(Body tankBody)
        {
            Geom newGeom = GeomFactory.Instance.CreateRectangleGeom(this.simulator, tankBody, this.Size.X, this.Size.Y);
            newGeom.FrictionCoefficient = 10f;
            newGeom.RestitutionCoefficient = .5f;
            newGeom.Tag = this;
            return newGeom;
        }

        protected void OnHitPointsChanged(int change)
        {
            if (this.HitPointsChanged != null)
            {
                this.HitPointsChanged(this, new HitPointsChangedEventArgs(this, change));
            }
            if (this.hitPoints == 0)
            {
                this.Die();
            }
        }

        protected void OnMaxHitPointsChanged(int change)
        {
            if (this.MaxHitPointsChanged != null)
            {
                this.MaxHitPointsChanged(this, new HitPointsChangedEventArgs(this, change));
            }
        }

        protected void OnControlledByUserChanged()
        {
            if (this.ControlledByUserChanged != null)
            {
                this.ControlledByUserChanged(this, new VehicleEventArgs(this));
            }
        }
        
        protected virtual void Die()
        {
            this.IsDead = true;
            this.hitPoints = 0;

            if (this.Died != null)
            {
                this.Died(this, new VehicleEventArgs(this));
            }
        }

        public virtual void Update()
        {
            if (this.Bursting)
            {
                this.burstingCount++;
                if (this.burstingCount > this.burstingInterval)
                {
                    this.Bursting = false;
                    this.burstingCount = 0;
                }
            }

            if (!this.IsDead && !this.readyToFire)
            {
                this.ellapsedFire++;
                if (this.ellapsedFire > this.GetEllapsedFireMax())
                {
                    this.readyToFire = true;
                }
            }

            this.CheckFlip();
        }

        int GetEllapsedFireMax()
        {
            int max = this.currentWeapon.BaseRateOfFire - (int)((double)this.currentWeapon.BaseRateOfFire * this.RateOfFireBonus);
            if (max < this.currentWeapon.LowestRateOfFire)
            {
                max = (int)this.currentWeapon.LowestRateOfFire;
            }
            return max;
        }

        void CheckFlip()
        {
            if (!this.flipped)
            {
                this.ellapsedFlipCheck++;
                if (this.ellapsedFlipCheck > this.flipCheckInterval)
                {
                    this.ellapsedFlipCheck = 0;

                    double max = Math.PI * 1.5d;
                    double min = Math.PI * .5d;
                    double rotation = this.Body.Rotation;

                    if (rotation < max
                        & rotation > min)
                    {
                        this.flipped = true;
                        if (this.FlippedOver != null)
                        {
                            this.FlippedOver(this, new VehicleEventArgs(this));
                        }
                    }

                }
            }
        }

        public void TryShoot(Vector2 trajectory, int speed)
        {
            if (!this.IsDead & this.readyToFire & this.AllowedToFire)
            {
                this.Shoot(trajectory, speed);
            }
        }
       
        protected virtual void Shoot(Vector2 trajectory, int speed)
        {
            if (this.IsDead || !this.AllowedToFire)
            {
                return;
            }

            this.ellapsedFire = 0;
            this.readyToFire = false;
            int min = (int)(speed * (1 - this.PercentError));
            int max = (int)(speed * (1 + this.PercentError));
            if (min < max)
            {
                speed = Randomizer.Random.Next(min, max);
            }
            Vector2 finalTrajectory = Vector2.Multiply(trajectory, speed);

            Projectile projectile = this.currentWeapon.GetProjectile();
            projectile.Position = this.Position + this.brush.CannonEnd;
            projectile.DamageBonus = this.DamageBonus;
            projectile.Source = this;
            projectile.Exploded += new EventHandler<ProjectileExplodedEventArgs>(projectile_Exploded);

            if (this.ProjectileCreated != null)
            {
                this.ProjectileCreated(this, new ProjectileEventArgs(projectile));
            }

            projectile.Body.ApplyForce(finalTrajectory);

            if (this.FiredShot != null)
            {
                this.FiredShot(this, new FiredShotEventArgs(this, finalTrajectory));
            }
        }

        void projectile_Exploded(object sender, ProjectileExplodedEventArgs e)
        {
            if (e.Target == null)
            {
                return;
            }

            if (e.Target is Vehicle & this.HitTarget != null)
            {
                this.HitTarget(this, new HitTargetEventArgs(this, e.Target as Vehicle));
            }
            else if (e.Target is Projectile & this.HitProjectile != null)
            {
                this.HitProjectile(this, new VehicleEventArgs(this));
            }
            else if (e.Target is Powerup & this.HitPowerup != null)
            {
                this.HitPowerup(this, new VehicleEventArgs(this));
            }
        }

        public void TakeHit(Projectile projectile)
        {
            if (this.TookHit != null)
            {
                this.TookHit(this, new VehicleEventArgs(this));
            }
        }

        public event EventHandler<ProjectileEventArgs> ProjectileCreated;
        public event EventHandler<FiredShotEventArgs> FiredShot;
        public event EventHandler<HitTargetEventArgs> HitTarget;
        public event EventHandler<VehicleEventArgs> HitProjectile;
        public event EventHandler<VehicleEventArgs> HitPowerup;
        public event EventHandler<HitPointsChangedEventArgs> HitPointsChanged;
        public event EventHandler<HitPointsChangedEventArgs> MaxHitPointsChanged;
        public event EventHandler<VehicleEventArgs> TookHit;
        public event EventHandler<VehicleEventArgs> ControlledByUserChanged;
        public event EventHandler<VehicleEventArgs> Died;
        public event EventHandler<VehicleEventArgs> FlippedOver;
        public event EventHandler<VehicleEventArgs> AccuracyChanged;
        public event EventHandler RateOfFireBonusChanged;
        public event EventHandler DamageBonusChanged;
        public event EventHandler WeaponChanged;
    }
}
