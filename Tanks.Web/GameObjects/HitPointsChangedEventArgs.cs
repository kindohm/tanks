using System;

namespace Tanks.Web.GameObjects
{
    public class HitPointsChangedEventArgs : EventArgs
    {
        public int Change { get; set; }
        public Vehicle Vehicle { get; set; }

        public HitPointsChangedEventArgs(Vehicle vehicle, int change)
        {
            this.Change = change;
            this.Vehicle = vehicle;
        }
    }
}
