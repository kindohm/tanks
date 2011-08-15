using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using System.Windows.Media;
using Tanks.Web.UI;
using Tanks.Web.GameObjects;

namespace Tanks.Web.Rounds
{
    public class Round5Ground : Ground
    {
        Round5GroundBrush brush;

        public Round5Ground(PhysicsSimulator simulator)
            : base(simulator)
        {
            this.simulator = simulator;
            this.Initialize();
        }

        void Initialize()
        {

            Vertices vertices = new Vertices();

            vertices.Add(new Vector2(1, 749));
            vertices.Add(new Vector2(999, 749));


            vertices.Add(new Vector2(999, 600));
            vertices.Add(new Vector2(900, 630));
            vertices.Add(new Vector2(800, 640));
            vertices.Add(new Vector2(700, 650));
            vertices.Add(new Vector2(620, 650));
            vertices.Add(new Vector2(580, 640));
            vertices.Add(new Vector2(500, 650));
            vertices.Add(new Vector2(470, 650));
            vertices.Add(new Vector2(400, 550));
            vertices.Add(new Vector2(370, 540));
            vertices.Add(new Vector2(300, 550));
            vertices.Add(new Vector2(1, 510));


            this.Body = BodyFactory.Instance.CreatePolygonBody(this.simulator, vertices, 10);
            this.Body.IsStatic = true;

            Geom geometry;
            geometry = GeomFactory.Instance.CreatePolygonGeom(this.simulator, this.Body, vertices, 1f);
            geometry.RestitutionCoefficient = .1f;
            geometry.FrictionCoefficient = 1f;
            geometry.Tag = this;

            this.brush = new Round5GroundBrush();
            this.brush.Extender.Body = this.Body;
        }

        public override IDrawingBrush Brush
        {
            get { return this.brush; }
        }
    }
}
