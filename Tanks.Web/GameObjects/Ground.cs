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
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using Tanks.Web.UI;

namespace Tanks.Web.GameObjects
{
    public class Ground : BaseGameObject
    {
        RectangleBrush brush;

        public Ground(PhysicsSimulator simulator):base(simulator)
        {
            this.simulator = simulator;
            this.Initialize();
        }
       
        void Initialize()
        {
            Vector2 size = new Vector2(Screen.Width - 20, 20);
            this.Body = BodyFactory.Instance.CreateRectangleBody(this.simulator, size.X, size.Y, 1);
            this.Body.IsStatic = true;
            this.Position = new Vector2(Screen.Width / 2, Screen.Height - 10);

            Geom geometry;
            geometry = GeomFactory.Instance.CreateRectangleGeom(this.simulator, this.Body, size.X, size.Y);
            geometry.RestitutionCoefficient = .1f;
            geometry.FrictionCoefficient = 1f;
            geometry.CollisionGridCellSize = .1f;
            geometry.Tag = this;
            this.brush = new RectangleBrush();
            this.brush.Extender.Body = this.Body;
            this.brush.Size = size;
            this.brush.rectangle.Fill = new SolidColorBrush(Colors.Brown);

        }

        public override IDrawingBrush Brush
        {
            get { return this.brush; }
        }

    }
}
