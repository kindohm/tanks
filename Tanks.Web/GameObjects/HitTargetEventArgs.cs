using System;

namespace Tanks.Web.GameObjects
{
    public class HitTargetEventArgs : EventArgs
    {
        public Vehicle Source { get; set; }
        public Vehicle Target { get; set; }

        public HitTargetEventArgs(Vehicle source, Vehicle target)
        {
            this.Source = source;
            this.Target = target;
        }
    }
}
