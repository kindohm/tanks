using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Collisions;
using Tanks.Web.UI;
using System.Windows.Media;

namespace Tanks.Web.GameObjects
{
    public class HealthPowerup : Powerup
    {
        double value;

        public HealthPowerup(PhysicsSimulator simulator) : base(simulator) { }

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        protected override void Initialize()
        {
            this.Color = Color.FromArgb(255,255,200,200);
            base.Initialize();
            this.Value = .2;
            HealthPowerupContentBrush content = new HealthPowerupContentBrush();
            this.PowerupBrush.AddContent(content);


        }

    }
}
