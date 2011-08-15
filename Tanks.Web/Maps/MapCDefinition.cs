using System.Windows.Controls;
using FarseerGames.FarseerPhysics;

namespace Tanks.Web.Maps
{
    public class MapCDefinition : MapDefinition
    {
        public MapCDefinition(PhysicsSimulator simulator) : base(simulator) { }

        protected override UserControl GetMapControl()
        {
            return new MapC();
        }

    }
}
