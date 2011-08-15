using System;
using System.Windows.Media;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using Tanks.Web.UI;
using Tanks.Web.Maps;

namespace Tanks.Web.GameObjects
{
    public abstract class Powerup : BaseGameObject
    {
        PowerupBrush brush;
        Geom geometry;
        int expirationStart = 200;
        int expirationDelayCount = 0;
        bool expired;
        Color color;
        int hitPoints;

        public Powerup(PhysicsSimulator simulator)
            : base(simulator)
        {
            this.Initialize();
        }

        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public bool Expired
        {
            get { return this.expired; }
        }

        public override IDrawingBrush Brush
        {
            get { return this.brush; }
        }

        public IPowerupBrush PowerupBrush
        {
            get { return this.brush; }
        }

        protected virtual void Initialize()
        {
            float mass = 1;
            this.hitPoints = 5;
            Vector2 size = new Vector2(50, 50);
            this.Body = BodyFactory.Instance.CreateRectangleBody(this.simulator, size.X, size.Y, mass);
            this.Body.IsStatic = false;
            this.Position = new Vector2(Screen.Width / 2, 50);

            this.geometry = GeomFactory.Instance.CreateRectangleGeom(this.simulator, this.Body, size.X, size.Y);
            this.geometry.RestitutionCoefficient = .05f;
            this.geometry.FrictionCoefficient = 1f;
            this.geometry.OnCollision += new Geom.CollisionEventHandler(HandleCollision);
            this.geometry.Tag = this;

            this.brush = new PowerupBrush(this, this.Color);
            this.brush.Extender.Body = this.Body;
            this.brush.Size = size;
        }

        protected virtual bool HandleCollision(Geom geom1, Geom geom2, ContactList list)
        {
            Geom other;
            if (geom1 == this.geometry)
            {
                other = geom2;
            }
            else
            {
                other = geom1;
            }

            if (other.Tag is MapUnit)
            {
                Page.SceneLoop.Update += new System.EventHandler<SceneLoopEventArgs>(SceneLoop_Update);
            }
            else if (other.Tag is Projectile)
            {
                this.HandleProjectileHit(other.Tag as Projectile);
            }

            return true;
        }

        void HandleProjectileHit(Projectile projectile)
        {
            this.Body.Position = this.Body.Position;
            this.hitPoints = this.hitPoints - projectile.Damage;
            if (this.hitPoints <= 0)
            {
                this.Acquire(projectile);
                this.Expire();
            }
        }

        void SceneLoop_Update(object sender, SceneLoopEventArgs e)
        {
            this.expirationDelayCount++;
            if (this.expirationDelayCount > this.expirationStart)
            {
                Page.SceneLoop.Update -= SceneLoop_Update;
                this.Expire();
            }
        }

        void Acquire(Projectile projectile)
        {
            if (this.PowerupAcquired != null)
            {
                this.PowerupAcquired(this, new PowerupAcquiredEventArgs(projectile.Source, this));
            }
        }


        void Expire()
        {
            this.expired = true;
            if (this.PowerupExpired != null)
            {
                this.Body.Dispose();
                this.geometry.Dispose();
                this.PowerupExpired(this, EventArgs.Empty);
            }
        }

        public event EventHandler PowerupExpired;
        public event EventHandler<PowerupAcquiredEventArgs> PowerupAcquired;

    }
}
