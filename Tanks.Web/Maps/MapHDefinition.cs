using System.Windows.Controls;
using FarseerGames.FarseerPhysics;

namespace Tanks.Web.Maps
{
    public class MapHDefinition : MapDefinition
    {
        public MapHDefinition(PhysicsSimulator simulator) : base(simulator) { }

        protected override UserControl GetMapControl()
        {
            return new MapH();
        }

    }
}
