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
    public class Round6Ground : Ground
    {
        Round6GroundBrush brush;

        public Round6Ground(PhysicsSimulator simulator)
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

           
                                    //<LineSegment Point="1000,300"/>
                                    //<LineSegment Point="850,290"/>
                                    //<LineSegment Point="770,300"/>
                                    //<LineSegment Point="700,650"/>
                                    //<LineSegment Point="620,650"/>
                                    //<LineSegment Point="580,650"/>
                                    //<LineSegment Point="420,640"/>
                                    //<LineSegment Point="300,650"/>
                                    //<LineSegment Point="0,660"/>
                                    //<LineSegment Point="0,750"/>


            vertices.Add(new Vector2(999, 300));
            vertices.Add(new Vector2(900, 310));
            vertices.Add(new Vector2(770, 300));
            vertices.Add(new Vector2(700, 650));
            vertices.Add(new Vector2(620, 650));
            vertices.Add(new Vector2(580, 650));
            vertices.Add(new Vector2(420, 640));
            vertices.Add(new Vector2(300, 650));
            vertices.Add(new Vector2(1, 660));


            this.Body = BodyFactory.Instance.CreatePolygonBody(this.simulator, vertices, 10);
            this.Body.IsStatic = true;

            Geom geometry;
            geometry = GeomFactory.Instance.CreatePolygonGeom(this.simulator, this.Body, vertices, 1f);
            geometry.RestitutionCoefficient = .1f;
            geometry.FrictionCoefficient = 1f;
            geometry.Tag = this;

            this.brush = new Round6GroundBrush();
            this.brush.Extender.Body = this.Body;
        }

        public override IDrawingBrush Brush
        {
            get { return this.brush; }
        }
    }
}
