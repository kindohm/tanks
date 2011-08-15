using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Tanks.Web.UI;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Factories;

namespace Tanks.Web.GameObjects
{
    public class Projectile : BaseGameObject
    {
        IDrawingBrush brush;
        protected float mass = 1;
        protected float radius = 5f;
        protected int damage = 10;
        protected double damageBonus;

        public Projectile(PhysicsSimulator simulator):base(simulator)
        {
            this.Initialize();
        }

        public int Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        public double DamageBonus
        {
            get { return this.damageBonus; }
            set { this.damageBonus = value; }
        }

        public int TotalDamage
        {
            get
            {
                return this.Damage + (int)((double)this.Damage * this.DamageBonus);
            }
        }

        protected virtual void Initialize()
        {
            this.Body = this.GetBody();

            this.Geometry = this.GetGeometry(this.Body);
            this.Geometry.OnCollision += new Geom.CollisionEventHandler(HandleCollision);
            this.Geometry.Tag = this;

            this.brush = this.GetProjectileBrush();
        }

        protected virtual Body GetBody()
        {
            return BodyFactory.Instance.CreateCircleBody(this.simulator, radius, this.mass);
        }

        protected virtual Geom GetGeometry(Body projectileBody)
        {
            Geom geom = GeomFactory.Instance.CreateCircleGeom(this.simulator, this.Body, radius, 20);
            geom.FrictionCoefficient = 50f;
            geom.RestitutionCoefficient = .01f;
            geom.CollisionGridCellSize = 1f;
            geom.Tag = this;
            return geom;
        }

        protected virtual IDrawingBrush GetProjectileBrush()
        {
            ProjectileBrush newBrush = new ProjectileBrush(this.Geometry, this.Damage);
            newBrush.Extender.Body = this.Body;
            newBrush.Radius = radius;
            newBrush.mainColor.Color = Colors.Black;
            return newBrush;
        }

        public Geom Geometry { get; set; }
        public Vehicle Source { get; set; }

        public override IDrawingBrush Brush
        {
            get { return this.brush; }
        }

        bool HandleCollision(Geom geom1, Geom geom2, ContactList list)
        {
            Geom other;
            if (geom1 == this.Geometry)
            {
                other = geom2;
            }
            else
            {
                other = geom1;
            }

            BaseGameObject tag = other.Tag as BaseGameObject;
            if (tag == null)
            {
                this.Geometry.Body.Dispose();
                this.Geometry.Dispose();
                return true;
            }


            if (tag is Vehicle)
            {
                this.HitVehicle((Vehicle)tag);
            }

            if (tag is Projectile)
            {
                System.Diagnostics.Debug.WriteLine("lkjsdf");
            }

            this.Geometry.Body.Dispose();
            this.Geometry.Dispose();

            if (this.Exploded != null)
            {
                this.Exploded(this, new ProjectileExplodedEventArgs(this, tag));
            }

            return true;
        }

        void HitVehicle(Vehicle vehicle)
        {
            vehicle.HitPoints = vehicle.HitPoints - this.TotalDamage;
            vehicle.TakeHit(this);
        }

        public event EventHandler<ProjectileExplodedEventArgs> Exploded;
    }
}
