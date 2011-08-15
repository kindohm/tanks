using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using Tanks.Web.GameObjects;
using Tanks.Web.Maps;

namespace Tanks.Web.Rounds
{
    public class Round7 : Round
    {

        public Round7(PhysicsSimulator simulator, Game game)
            : base(simulator, game)
        {
        }

        protected override void Initialize(PhysicsSimulator simulator)
        {
            this.enemyCount = 4;
            base.Initialize(simulator);
            this.Wind.RefreshRate = 2500;
            this.wind.MaxXScale = 500;
            this.wind.MaxYScale = 40;
        }

        public override Round GetNextRound()
        {
            return new Round8(this.simulator, this.game);
        }

        protected override MapDefinition CreateMap(PhysicsSimulator simulator)
        {
            //return MapCache.Instance.GetMap("MapG", simulator);
            return new MapGDefinition(simulator);
        }

        protected override void LoadEnemies(PhysicsSimulator simulator)
        {
            base.LoadEnemies(simulator);
            foreach (Vehicle enemy in this.Enemies)
            {
                enemy.MaxHitPoints = 250;
                enemy.HitPoints = enemy.MaxHitPoints;
                enemy.RateOfFireBonus = .6;
            }
        }

        protected override void PlaceVehicles(Vehicle userVehicle, System.Collections.Generic.List<Vehicle> enemyVehicles)
        {
            userVehicle.Position = new Vector2(500, 600);
            enemyVehicles[0].Position = new Vector2(200, 150);
            enemyVehicles[1].Position = new Vector2(250, 600);
            enemyVehicles[2].Position = new Vector2(700, 600);
            enemyVehicles[3].Position = new Vector2(900, 590);
        }
    }
}
