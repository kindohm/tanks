using System;

namespace Tanks.Web.GameObjects
{
    public class RoundEventArgs : EventArgs
    {
        public Round Round { get; set; }

        public RoundEventArgs(Round round)
        {
            this.Round = round;
        }
    }
}
