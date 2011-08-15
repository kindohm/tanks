using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using Tanks.Web.GameObjects;
using Tanks.Web.Maps;

namespace Tanks.Web.Rounds
{
    public class Round6 : Round
    {

        public Round6(PhysicsSimulator simulator, Game game)
            : base(simulator, game)
        {
        }

        protected override void Initialize(PhysicsSimulator simulator)
        {
            this.enemyCount = 3;
            base.Initialize(simulator);
            this.Wind.RefreshRate = 2500;
            this.wind.MaxXScale = 500;
            this.wind.MaxYScale = 2;
        }

        public override Round GetNextRound()
        {
            return new Round7(this.simulator, this.game);
        }

        protected override MapDefinition CreateMap(PhysicsSimulator simulator)
        {
            return new MapFDefinition(simulator);
        }

        protected override void LoadEnemies(PhysicsSimulator simulator)
        {
            base.LoadEnemies(simulator);
            foreach (Vehicle enemy in this.Enemies)
            {
                enemy.MaxHitPoints = 200;
                enemy.HitPoints = enemy.MaxHitPoints;
                enemy.RateOfFireBonus = .4;
            }
        }

        protected override void PlaceVehicles(Vehicle userVehicle, System.Collections.Generic.List<Vehicle> enemyVehicles)
        {
            userVehicle.Position = new Vector2(600, 600);
            enemyVehicles[0].Position = new Vector2(830, 250);
            enemyVehicles[1].Position = new Vector2(150, 600);
            enemyVehicles[2].Position = new Vector2(375, 600);
        }



    }
}
