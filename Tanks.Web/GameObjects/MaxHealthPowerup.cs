using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Collisions;
using Tanks.Web.UI;
using System.Windows.Media;

namespace Tanks.Web.GameObjects
{
    public class MaxHealthPowerup : Powerup
    {
        double value;

        public MaxHealthPowerup(PhysicsSimulator simulator) : base(simulator) { }

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        protected override void Initialize()
        {
            this.Color = Colors.Gray;
            base.Initialize();
            this.Value = .25;
            MaxHealthPowerupContentBrush content = new MaxHealthPowerupContentBrush();
            this.PowerupBrush.AddContent(content);
            
            
        }

    }
}
