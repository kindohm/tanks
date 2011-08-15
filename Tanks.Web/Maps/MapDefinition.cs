using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Factories;

namespace Tanks.Web.Maps
{
    public abstract class MapDefinition
    {
        PhysicsSimulator simulator;
        UserControl mapControl;
        List<MapUnit> units;

        public List<MapUnit> Units
        {
            get { return this.units; }
        }

        public IDrawingBrush Brush
        {
            get { return this.MapControl as IDrawingBrush; }
        }

        public UserControl MapControl
        {
            get
            {
                if (this.mapControl == null)
                {
                    this.mapControl = this.GetMapControl();
                }
                return this.mapControl;
            }
        } 

        public MapDefinition(PhysicsSimulator simulator)
        {
            this.simulator = simulator;
            this.units = new List<MapUnit>();
            this.Initialize();
        }

        PathGeometry GetMapGeometry()
        {
            IMap map = this.MapControl as IMap;
            if (map == null)
            {
                throw new Exception("Map was not an IMap.");
            }
            return map.PathGeometry;
        }

        void Initialize()
        {
            this.LoadPath();
        }

        void LoadPath()
        {
            PathGeometry pathGeometry = this.GetMapGeometry();
            foreach (PathFigure figure in pathGeometry.Figures)
            {
                this.LoadFigure(figure);
            }
        }

        void LoadFigure(PathFigure figure)
        {
            MapUnit unit = new MapUnit();
            Vertices vertices = MapDefinition.GetVerticesFromFigure(figure);
            Body body = BodyFactory.Instance.CreatePolygonBody(this.simulator, vertices, 10);
            body.IsStatic = true;

            Geom geometry;
            geometry = GeomFactory.Instance.CreatePolygonGeom(this.simulator, body, vertices, 1f);
            geometry.RestitutionCoefficient = .1f;
            geometry.FrictionCoefficient = 1f;
            geometry.Tag = unit;




            unit.Geometry = geometry;
            unit.Body = body;

            this.units.Add(unit);
        }

        static Vertices GetVerticesFromFigure(PathFigure figure)
        {
            Vertices vertices = new Vertices();
            vertices.Add(new Vector2((float)figure.StartPoint.X, (float)figure.StartPoint.Y));
            foreach (LineSegment segment in figure.Segments)
            {
                vertices.Add(new Vector2((float)segment.Point.X, (float)segment.Point.Y));
            }

            return vertices;
        }

        protected abstract UserControl GetMapControl();
        
    }
}
