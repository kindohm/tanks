using System;

namespace Tanks.Web.GameObjects
{
    public class PowerupAcquiredEventArgs : EventArgs
    {
        public Vehicle Vehicle { get; set; }
        public Powerup Powerup { get; set; }

        public PowerupAcquiredEventArgs(Vehicle vehicle, Powerup powerup)
        {
            this.Vehicle = vehicle;
            this.Powerup = powerup;
        }

    }
}
