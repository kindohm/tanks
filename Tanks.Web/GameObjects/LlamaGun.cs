using FarseerGames.FarseerPhysics;

namespace Tanks.Web.GameObjects
{
    public class LlamaGun : Weapon
    {
        public LlamaGun(PhysicsSimulator simulator) : base(simulator) { }

        protected override void Initialize()
        {
            base.Initialize();
            this.Name = "Llama Gun";
            this.lowestRateOfFire = 50;
            this.BaseRateOfFire = 300;
            this.ImageName = "llama.png";
        }

        public override Projectile GetProjectile()
        {
            Projectile proj = new LlamaProjectile(this.simulator);
            return proj;
        }
    }
}
