using System;
using FarseerGames.FarseerPhysics.Mathematics;

namespace Tanks.Web.GameObjects
{
    public class FiredShotEventArgs : EventArgs
    {
        public Vehicle Vehicle { get; set; }
        public Vector2 Trajectory { get; set; }

        public FiredShotEventArgs(Vehicle vehicle, Vector2 trajectory)
        {
            this.Vehicle = vehicle;
            this.Trajectory = trajectory;
        }
    }
}
