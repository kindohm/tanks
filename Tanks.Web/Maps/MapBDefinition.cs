using System.Windows.Controls;
using FarseerGames.FarseerPhysics;

namespace Tanks.Web.Maps
{
    public class MapBDefinition : MapDefinition
    {
        public MapBDefinition(PhysicsSimulator simulator) : base(simulator) { }

        protected override UserControl GetMapControl()
        {
            return new MapB();
        }

    }
}
