using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using Tanks.Web.GameObjects;
using Tanks.Web.Maps;

namespace Tanks.Web.Rounds
{
    public class Round1 : Round
    {

        public Round1(PhysicsSimulator simulator, Game game)
            : base(simulator, game)
        {
        }

        public override Round GetNextRound()
        {
            return new Round2(this.simulator, this.game);
        }

        protected override void Initialize(PhysicsSimulator simulator)
        {
            base.Initialize(simulator);
            this.Wind.RefreshRate = 2600;
            this.Wind.MaxXScale = 200;
            this.Wind.MaxYScale = 200;
        }

        protected override void PlaceVehicles(Vehicle userVehicle, System.Collections.Generic.List<Vehicle> enemyVehicles)
        {
            userVehicle.Position = new Vector2(200, 300);
            enemyVehicles[0].Position = new Vector2(800, 300);
        }

        protected override void LoadEnemies(PhysicsSimulator simulator)
        {
            base.LoadEnemies(simulator);
            foreach (EnemyVehicle vehicle in this.enemies)
            {
                vehicle.RateOfFireBonus = 0;
                vehicle.MaxHitPoints = 75;
                vehicle.HitPoints = vehicle.MaxHitPoints;
            }
        }

        protected override Tanks.Web.Maps.MapDefinition CreateMap(PhysicsSimulator simulator)
        {
            //return MapCache.Instance.GetMap("MapA", simulator);
            return new MapADefinition(simulator);
        }

    }
}
