using System;

namespace Tanks.Web.GameObjects
{
    public class WindEventArgs : EventArgs
    {
        public Wind Wind { get; set; }

        public WindEventArgs(Wind wind)
        {
            this.Wind = wind;
        }
    }
}
