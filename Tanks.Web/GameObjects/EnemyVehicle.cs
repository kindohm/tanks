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
using System.Diagnostics;

namespace Tanks.Web.GameObjects
{
    public class EnemyVehicle : Vehicle
    {
        public EnemyVehicle(PhysicsSimulator simulator, Round round)
            : base(simulator, round) { }

        public void RandomizeStartingFiring()
        {
            this.readyToFire = false;
            int x = Randomizer.Random.Next(1, this.CurrentWeapon.BaseRateOfFire - 1);
            this.ellapsedFire = x;
        }

        public override void Update()
        {
            base.Update();
            this.ProcessOffense();
        }

        void ProcessOffense()
        {
            if (this.readyToFire & this.AllowedToFire)
            {
                Vehicle target = this.SelectTarget();
                if (target != null)
                {
                    float xDistance = target.Position.X - this.Position.X;
                    float yDistance = target.Position.Y - this.Position.Y;
                    Vector2 xVector = new Vector2(xDistance, Math.Abs(xDistance) * -1);
                    xVector.Normalize();
                    int speed = this.Get45Speed(Math.Abs(xDistance), yDistance);
                    this.Shoot(xVector, speed);
                }
            }
        }

        int Get45Speed(float xDistance, float yDistance)
        {
            //quadratic equation
            //y = Ax^2 + Bx + C
            double a = -.1;
            double b = 190;
            double c = 20000;
            double y = a * xDistance * xDistance + b * xDistance + c;

            y = y - 50*yDistance; //hacky ?? too lazy to figure out something decent

            return (int)y;
        }

        Vehicle SelectTarget()
        {
            foreach (Vehicle vehicle in this.round.VehicleList)
            {
                if (vehicle is EnemyVehicle == false && vehicle.IsDead == false)
                {
                    return vehicle;
                }
            }
            return null;
        }

        protected override void Die()
        {
            base.Die();
            this.Geometry.Body.Dispose();
            this.Geometry.Dispose();
        }

    }
}
