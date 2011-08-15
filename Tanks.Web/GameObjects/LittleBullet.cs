using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Factories;

namespace Tanks.Web.GameObjects
{
    public class LittleBullet : Projectile
    {
        public LittleBullet(PhysicsSimulator simulator):base(simulator)
        {

        }

        protected override void Initialize()
        {
            this.mass = .5f;
            this.Damage = 2;
            base.Initialize();
        }
    }
}
