using System.Collections.Generic;
using Tanks.Web.GameObjects;
using FarseerGames.FarseerPhysics;

namespace Tanks.Web
{
    public class Player
    {
        PhysicsSimulator simulator;
        List<Powerup> powerups;
        bool hasUzi;
        bool hasLlama;

        public Player(PhysicsSimulator simulator)
        {
            this.simulator = simulator;
            this.Initialize();
        }

        public List<Powerup> Powerups
        {
            get { return this.powerups; }
        }

        public bool HasUzi
        {
            get { return this.hasUzi; }
            set { this.hasUzi = value; }
        }

        public bool HasLlama
        {
            get { return this.hasLlama; }
            set { this.hasLlama = value; }
        }

        void Initialize()
        {
            this.powerups = new List<Powerup>();
        }

        public void ApplyAllPowerups(List<Vehicle> vehicles)
        {

            foreach (Vehicle vehicle in vehicles)
            {
                this.ApplyAllPowerups(vehicle);
            }
            
        }

        public void ApplyAllPowerups(Vehicle vehicle)
        {
            foreach (Powerup powerup in this.Powerups)
            {
                Player.ApplyPowerup(this, vehicle, powerup);
            }
        }

        public static void ApplyPowerup(Player player, List<Vehicle> vehicles, Powerup powerup)
        {
            foreach (Vehicle vehicle in vehicles)
            {
                Player.ApplyPowerup(player, vehicle, powerup);
            }
        }

        public static void ApplyPowerup(Player player, Vehicle vehicle, Powerup powerup)
        {
            if (powerup is RateOfFirePowerup)
            {
                Player.ApplyRateOfFirePowerup(vehicle, powerup as RateOfFirePowerup);
            }
            else if (powerup is MaxHealthPowerup)
            {
                Player.ApplyMaxHealthPowerup(vehicle, powerup as MaxHealthPowerup);
            }
            else if (powerup is HealthPowerup)
            {
                Player.ApplyHealthPowerup(vehicle, powerup as HealthPowerup);
            }
            else if (powerup is UziPowerup && player != null)
            {
                Player.ApplyUziPowerup(player, vehicle, powerup as UziPowerup);
            }
            else if (powerup is LlamaGunPowerup && player != null)
            {
                Player.ApplyLlamaGunPowerup(player, vehicle, powerup as LlamaGunPowerup);
            }
            else if (powerup is DamagePowerup)
            {
                Player.ApplyDamageBonusPowerup(vehicle, powerup as DamagePowerup);
            }
            else if (powerup is AccuracyPowerup)
            {
                Player.ApplyAccuracyPowerup(vehicle, powerup as AccuracyPowerup);
            }
            else if (powerup is BurstPowerup)
            {
                Player.ApplyBurstPowerup(vehicle, powerup as BurstPowerup);
            }
        }

        public static void ApplyDamageBonusPowerup(Vehicle vehicle, DamagePowerup powerup)
        {
            vehicle.DamageBonus = vehicle.DamageBonus + powerup.Value;
        }

        public static void ApplyAccuracyPowerup(Vehicle vehicle, AccuracyPowerup powerup)
        {
            vehicle.PercentError = vehicle.PercentError - (vehicle.PercentError * powerup.Value);
        }

        public static void ApplyRateOfFirePowerup(Vehicle vehicle, RateOfFirePowerup powerup)
        {
            vehicle.RateOfFireBonus = vehicle.RateOfFireBonus + powerup.Value;
        }

        public static void ApplyMaxHealthPowerup(Vehicle vehicle, MaxHealthPowerup powerup)
        {
            vehicle.MaxHitPoints = vehicle.MaxHitPoints + (int)((double)vehicle.MaxHitPoints * powerup.Value);
        }

        public static void ApplyHealthPowerup(Vehicle vehicle, HealthPowerup powerup)
        {
            vehicle.HitPoints = vehicle.HitPoints + (int)((double)vehicle.MaxHitPoints * powerup.Value);
        }

        public static void ApplyBurstPowerup(Vehicle vehicle, BurstPowerup powerup)
        {
            vehicle.Bursting = true;
        }

        public static void ApplyUziPowerup(Player player, Vehicle vehicle, UziPowerup powerup)
        {
            foreach (Weapon weapon in vehicle.Weapons)
            {
                if (weapon is Uzi)
                {
                    return;
                }
            }
            player.HasUzi = true;
            vehicle.Weapons.Add(new Uzi(player.simulator));
        }

        public static void ApplyLlamaGunPowerup(Player player, Vehicle vehicle, LlamaGunPowerup powerup)
        {
            foreach (Weapon weapon in vehicle.Weapons)
            {
                if (weapon is LlamaGun)
                {
                    return;
                }
            }
            player.HasLlama = true;
            vehicle.Weapons.Add(new LlamaGun(player.simulator));
        }
    
    }
}
