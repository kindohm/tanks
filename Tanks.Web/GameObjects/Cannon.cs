using FarseerGames.FarseerPhysics;

namespace Tanks.Web.GameObjects
{
    public class Cannon : Weapon
    {
        public Cannon(PhysicsSimulator simulator) : base(simulator) { }

        protected override void Initialize()
        {
            base.Initialize();
            this.Name = "Cannon";
            this.ImageName = "cannon.png";
            this.lowestRateOfFire = 10;
            this.BaseRateOfFire = 200;
        }

        public override Projectile GetProjectile()
        {
            return new Projectile(this.simulator);
        }
    }
}
