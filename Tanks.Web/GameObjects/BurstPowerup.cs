using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Collisions;
using Tanks.Web.UI;
using System.Windows.Media;

namespace Tanks.Web.GameObjects
{
    public class BurstPowerup : Powerup
    {
        double value;

        public BurstPowerup(PhysicsSimulator simulator) : base(simulator) { }

        protected override void Initialize()
        {
            this.Color = Colors.Green;
            base.Initialize();
            BurstPowerupContentBrush content = new BurstPowerupContentBrush();
            this.PowerupBrush.AddContent(content);
        }

    }
}
