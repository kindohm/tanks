using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Collisions;
using Tanks.Web.UI;
using System.Windows.Media;

namespace Tanks.Web.GameObjects
{
    public class UziPowerup : Powerup
    {
        double value;

        public UziPowerup(PhysicsSimulator simulator) : base(simulator) { }

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        protected override void Initialize()
        {
            this.Color = Colors.Orange;
            base.Initialize();
            this.Value = 0;
            UziPowerupContentBrush content = new UziPowerupContentBrush();
            this.PowerupBrush.AddContent(content);
        }

    }
}
