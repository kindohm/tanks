using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Dynamics;

namespace Tanks.Web.GameObjects
{
    public abstract class BaseGameObject : IGameObject
    {
        Body body;
        protected PhysicsSimulator simulator;

        public BaseGameObject(PhysicsSimulator simulator)
        {
            this.simulator = simulator;
        }

        public Vector2 Position
        {
            get { return this.Body.Position; }
            set { this.Body.Position = value; }
        }

        public Body Body
        {
            get { return this.body; }
            set { this.body = value; }
        }

        public abstract IDrawingBrush Brush{ get; }
    }
}
