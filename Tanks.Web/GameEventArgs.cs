using System;

namespace Tanks.Web
{
    public class GameEventArgs : EventArgs
    {
        public Game Game { get; set; }

        public GameEventArgs(Game game)
        {
            this.Game = game;
        }
    }
}
