using System;

namespace Tanks.Web.GameObjects
{
    public class VehicleEventArgs : EventArgs
    {
        public Vehicle Vehicle { get; set; }

        public VehicleEventArgs(Vehicle vehicle)
        {
            this.Vehicle = vehicle;
        }
    }
}
