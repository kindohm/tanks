using System.Windows.Controls;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Mathematics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Factories;


namespace Tanks.Web.Maps
{
    public class MapUnit
    {
        public Geom Geometry { get; set; }
        public Body Body { get; set; }
    }
}
