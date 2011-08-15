using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using Tanks.Web.GameObjects;
using Tanks.Web.Maps;

namespace Tanks.Web.Rounds
{
    public class Round4 : Round
    {

        public Round4(PhysicsSimulator simulator, Game game)
            : base(simulator, game)
        {
        }

        protected override void Initialize(PhysicsSimulator simulator)
        {
            this.enemyCount = 2;
            base.Initialize(simulator);
            this.Wind.RefreshRate = 2500;
            this.wind.MaxXScale = 500;
            this.wind.MaxYScale = 40;
        }

        public override Round GetNextRound()
        {
            return new Round5(this.simulator, this.game);
        }

        protected override MapDefinition CreateMap(PhysicsSimulator simulator)
        {
            return new MapDDefinition(simulator);

        }


        protected override void LoadEnemies(PhysicsSimulator simulator)
        {
            base.LoadEnemies(simulator);
            foreach (Vehicle enemy in this.Enemies)
            {
                enemy.MaxHitPoints = 130;
                enemy.HitPoints = enemy.MaxHitPoints;
                enemy.RateOfFireBonus = .2;
            }
        }

        protected override void PlaceVehicles(Vehicle userVehicle, System.Collections.Generic.List<Vehicle> enemyVehicles)
        {
            userVehicle.Position = new Vector2(150, 490);
            enemyVehicles[0].Position = new Vector2(850, 500);
            enemyVehicles[1].Position = new Vector2(740, 500);
        }

        

    }
}
