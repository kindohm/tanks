using System;

namespace Tanks.Web.GameObjects
{
    public class PowerupEventArgs : EventArgs
    {
        public Powerup Powerup { get; set; }

        public PowerupEventArgs(Powerup powerup)
        {
            this.Powerup = powerup;
        }
    }
}
