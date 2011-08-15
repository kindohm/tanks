using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using Tanks.Web.GameObjects;
using Tanks.Web.Maps;

namespace Tanks.Web.Rounds
{
    public class Round3 : Round
    {
        public Round3(PhysicsSimulator simulator, Game game)
            : base(simulator, game)
        {
        }

        protected override void Initialize(PhysicsSimulator simulator)
        {
            this.enemyCount = 2;
            base.Initialize(simulator);
            this.Wind.RefreshRate = 2400;
            this.wind.MaxXScale = 200;
            this.wind.MaxYScale = 20;

        }

        public override Round GetNextRound()
        {
            return new Round4(this.simulator, this.game);
        }

        protected override MapDefinition CreateMap(PhysicsSimulator simulator)
        {
            return new MapCDefinition(simulator);
            //return MapCache.Instance.GetMap("MapC", simulator);
        }

        protected override void LoadEnemies(PhysicsSimulator simulator)
        {
            base.LoadEnemies(simulator);
            foreach (Vehicle enemy in this.Enemies)
            {
                enemy.MaxHitPoints = 100;
                enemy.HitPoints = 100;
                enemy.RateOfFireBonus = 0;
            }
        }

        protected override void PlaceVehicles(Vehicle userVehicle, System.Collections.Generic.List<Vehicle> enemyVehicles)
        {
            userVehicle.Position = new Vector2(100, 400);
            enemyVehicles[0].Position = new Vector2(900, 400);
            enemyVehicles[1].Position = new Vector2(770, 400);
        }

     }
}
