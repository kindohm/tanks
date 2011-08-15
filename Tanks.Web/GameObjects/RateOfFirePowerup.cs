using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Collisions;
using Tanks.Web.UI;
using System.Windows.Media;

namespace Tanks.Web.GameObjects
{
    public class RateOfFirePowerup : Powerup
    {
        double value;

        public RateOfFirePowerup(PhysicsSimulator simulator) : base(simulator) { }

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        protected override void Initialize()
        {
            this.Color = Colors.Yellow;
            base.Initialize();
            this.Value = .1;
            RateOfFirePowerupContentBrush content = new RateOfFirePowerupContentBrush();
            this.PowerupBrush.AddContent(content);
        }

    }
}
