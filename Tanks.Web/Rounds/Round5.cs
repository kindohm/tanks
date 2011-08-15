using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using Tanks.Web.GameObjects;
using Tanks.Web.Maps;

namespace Tanks.Web.Rounds
{
    public class Round5 : Round
    {

        public Round5(PhysicsSimulator simulator, Game game)
            : base(simulator, game)
        {
        }

        protected override void Initialize(PhysicsSimulator simulator)
        {
            this.enemyCount = 3;
            base.Initialize(simulator);
            this.Wind.RefreshRate = 2500;
            this.wind.MaxXScale = 500;
            this.wind.MaxYScale = 40;
        }

        public override Round GetNextRound()
        {
            return new Round6(this.simulator, this.game);
        }

        protected override MapDefinition CreateMap(PhysicsSimulator simulator)
        {
            return new MapEDefinition(simulator);
            //return MapCache.Instance.GetMap("MapE", simulator);

        }


        protected override void LoadEnemies(PhysicsSimulator simulator)
        {
            base.LoadEnemies(simulator);
            foreach (Vehicle enemy in this.Enemies)
            {
                enemy.MaxHitPoints = 155;
                enemy.HitPoints = enemy.MaxHitPoints;
                enemy.RateOfFireBonus = .25;
            }
        }

        protected override void PlaceVehicles(Vehicle userVehicle, System.Collections.Generic.List<Vehicle> enemyVehicles)
        {
            userVehicle.Position = new Vector2(550, 550);
            enemyVehicles[0].Position = new Vector2(850, 550);
            enemyVehicles[1].Position = new Vector2(300, 500);
            enemyVehicles[2].Position = new Vector2(100, 450);
        }



    }
}
