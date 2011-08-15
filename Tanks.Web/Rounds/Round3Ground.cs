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
    public class Round3Ground : Ground
    {
        Round3GroundBrush brush;

        public Round3Ground(PhysicsSimulator simulator)
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
            vertices.Add(new Vector2(999, 430));
            vertices.Add(new Vector2(800, 470));
            vertices.Add(new Vector2(600, 530));
            vertices.Add(new Vector2(500, 690));
            vertices.Add(new Vector2(420, 650));
            vertices.Add(new Vector2(370, 550));
            vertices.Add(new Vector2(300, 510));
            vertices.Add(new Vector2(200, 490));
            vertices.Add(new Vector2(100, 490));
            vertices.Add(new Vector2(1, 470));
                        
            
            vertices.Add(new Vector2(1, 710));
            this.Body = BodyFactory.Instance.CreatePolygonBody(this.simulator, vertices, 10);
            this.Body.IsStatic = true;

            Geom geometry;
            geometry = GeomFactory.Instance.CreatePolygonGeom(this.simulator, this.Body, vertices, 1f);
            geometry.RestitutionCoefficient = .1f;
            geometry.FrictionCoefficient = 1f;
            geometry.Tag = this;

            this.brush = new Round3GroundBrush();
            this.brush.Extender.Body = this.Body;
        }

        public override IDrawingBrush Brush
        {
            get { return this.brush; }
        }
    }
}
