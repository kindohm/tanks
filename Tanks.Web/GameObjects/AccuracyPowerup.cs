using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Collisions;
using Tanks.Web.UI;
using System.Windows.Media;

namespace Tanks.Web.GameObjects
{
    public class AccuracyPowerup : Powerup
    {
        double value;

        public AccuracyPowerup(PhysicsSimulator simulator) : base(simulator) { }

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        protected override void Initialize()
        {
            this.Color = Colors.Purple;
            base.Initialize();
            this.Value = .25;
            AccuracyPowerupContentBrush content = new AccuracyPowerupContentBrush();
            this.PowerupBrush.AddContent(content);
        }

    }
}
