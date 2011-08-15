using FarseerGames.FarseerPhysics;

namespace Tanks.Web.GameObjects
{
    public abstract class Weapon
    {
        string name;
        string imageName;
        protected PhysicsSimulator simulator;
        protected int baseRateOfFire;
        protected double lowestRateOfFire;

        public Weapon(PhysicsSimulator simulator)
        {
            this.simulator = simulator;
            this.Initialize();
        }

        public string ImageName
        {
            get { return this.imageName; }
            set { this.imageName = value; }
        }

        public double LowestRateOfFire
        {
            get { return this.lowestRateOfFire; }
            set { this.lowestRateOfFire = value; }
        }

        public int BaseRateOfFire
        {
            get { return this.baseRateOfFire; }
            set { this.baseRateOfFire = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        protected virtual void Initialize()
        {
            this.lowestRateOfFire = 10;
            this.baseRateOfFire = 200;
        }

        public abstract Projectile GetProjectile();

    }
}
