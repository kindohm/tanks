using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Factories;
using Tanks.Web.UI;

namespace Tanks.Web.GameObjects
{
    public class LlamaProjectile : Projectile
    {
        public LlamaProjectile(PhysicsSimulator simulator)
            : base(simulator)
        {

        }

        protected override void Initialize()
        {
            this.radius = 10f;
            this.mass = 1.5f;
            this.Damage = 20;
            base.Initialize();

           
        }
    }
}
