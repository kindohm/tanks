using System;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using Tanks.Web.GameObjects;
using Tanks.Web.Maps;

namespace Tanks.Web.Rounds
{
    public class Round9 : Round
    {
        double difficulty;

        public Round9(PhysicsSimulator simulator, Game game)
            : base(simulator, game)
        {
        }

        public Round9(PhysicsSimulator simulator, Game game, double percentDifficulty) : base(simulator, game, percentDifficulty) { }

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
            return new Round9(this.simulator, this.game, this.difficulty + .1);
        }

        protected override MapDefinition CreateMap(PhysicsSimulator simulator)
        {
            int random = Randomizer.Random.Next(2, 9);

            switch (random)
            {
                case 2:
                    this.map = new MapBDefinition(simulator);
                    break;
                case 3:
                    this.map = new MapCDefinition(simulator);
                    break;
                case 4:
                    this.map = new MapDDefinition(simulator);
                    break;
                case 5:
                    this.map = new MapEDefinition(simulator);
                    break;
                case 6:
                    this.map = new MapFDefinition(simulator);
                    break;
                case 7:
                    this.map = new MapGDefinition(simulator);
                    break;
                case 8:
                    this.map = new MapHDefinition(simulator);
                    break;
                default:
                    throw new Exception("Unknown random map index.");
            }
            
            return this.map;
        }

        protected override void LoadEnemies(PhysicsSimulator simulator)
        {
            if (this.initializationData == null)
            {
                this.difficulty = 0;
            }
            else
            {
                this.difficulty = (double)this.initializationData;
            }
            base.LoadEnemies(simulator);

            foreach (Vehicle enemy in this.Enemies)
            {
                enemy.MaxHitPoints = 250 + (int)((double)250 * this.difficulty);
                enemy.HitPoints = enemy.MaxHitPoints;
                if (enemy.RateOfFireBonus == 0d)
                {
                    enemy.RateOfFireBonus = .6d;
                }
                enemy.RateOfFireBonus = enemy.RateOfFireBonus + (enemy.RateOfFireBonus * this.difficulty)/2.0;
                enemy.DamageBonus = enemy.DamageBonus + this.difficulty;
            }
        }

        protected override void PlaceVehicles(Vehicle userVehicle, System.Collections.Generic.List<Vehicle> enemyVehicles)
        {
            if (this.map is MapADefinition)
            {
                userVehicle.Position = new Vector2(500, 650);
                enemyVehicles[0].Position = new Vector2(100, 650);
                enemyVehicles[1].Position = new Vector2(300, 650);
                enemyVehicles[2].Position = new Vector2(700, 650);
                enemyVehicles[3].Position = new Vector2(900, 650);
            }
            else if (this.map is MapBDefinition)
            {
                userVehicle.Position = new Vector2(500, 600);
                enemyVehicles[0].Position = new Vector2(100, 600);
                enemyVehicles[1].Position = new Vector2(300, 550);
                enemyVehicles[2].Position = new Vector2(700, 550);
                enemyVehicles[3].Position = new Vector2(900, 650);
            }
            else if (this.map is MapCDefinition)
            {
                userVehicle.Position = new Vector2(700, 420);
                enemyVehicles[0].Position = new Vector2(100, 440);
                enemyVehicles[1].Position = new Vector2(250, 440);
                enemyVehicles[2].Position = new Vector2(800, 410);
                enemyVehicles[3].Position = new Vector2(900, 400);
            }
            else if (this.map is MapDDefinition)
            {
                userVehicle.Position = new Vector2(350, 500);
                enemyVehicles[0].Position = new Vector2(100, 450);
                enemyVehicles[1].Position = new Vector2(210, 460);
                enemyVehicles[2].Position = new Vector2(800, 470);
                enemyVehicles[3].Position = new Vector2(900, 470);
            }

            else if (this.map is MapEDefinition)
            {
                userVehicle.Position = new Vector2(500, 600);
                enemyVehicles[0].Position = new Vector2(100, 460);
                enemyVehicles[1].Position = new Vector2(300, 500);
                enemyVehicles[2].Position = new Vector2(700, 600);
                enemyVehicles[3].Position = new Vector2(900, 580);
            }
            else if (this.map is MapFDefinition)
            {
                userVehicle.Position = new Vector2(600, 600);
                enemyVehicles[0].Position = new Vector2(830, 250);
                enemyVehicles[1].Position = new Vector2(100, 600);
                enemyVehicles[2].Position = new Vector2(325, 600);
                enemyVehicles[3].Position = new Vector2(450, 590);
            }
            else if (this.map is MapGDefinition)
            {
                userVehicle.Position = new Vector2(500, 600);
                enemyVehicles[0].Position = new Vector2(200, 150);
                enemyVehicles[1].Position = new Vector2(250, 600);
                enemyVehicles[2].Position = new Vector2(700, 600);
                enemyVehicles[3].Position = new Vector2(900, 590);
            }
            else if (this.map is MapHDefinition)
            {
                userVehicle.Position = new Vector2(500, 600);
                enemyVehicles[0].Position = new Vector2(100, 350);
                enemyVehicles[1].Position = new Vector2(900, 350);
                enemyVehicles[2].Position = new Vector2(100, 600);
                enemyVehicles[3].Position = new Vector2(900, 600);
            }
        }
    }
}
