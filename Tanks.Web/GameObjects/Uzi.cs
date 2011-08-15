using FarseerGames.FarseerPhysics;

namespace Tanks.Web.GameObjects
{
    public class Uzi : Weapon
    {
        public Uzi(PhysicsSimulator simulator) : base(simulator) { }

        protected override void Initialize()
        {
            base.Initialize();
            this.Name = "Uzi";
            this.ImageName = "uzi.png";
            this.lowestRateOfFire = 5;
            this.BaseRateOfFire = 50;
        }

        public override Projectile GetProjectile()
        {
            Projectile proj = new LittleBullet(this.simulator);
            return proj;
        }
    }
}
