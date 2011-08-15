using System;

namespace Tanks.Web
{
    public class SceneLoopEventArgs : EventArgs
    {
        public SceneLoop SceneLoop { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        public SceneLoopEventArgs(SceneLoop game, TimeSpan elapsed)
        {
            this.SceneLoop = game;
            this.ElapsedTime = elapsed;
        }
    }
}
