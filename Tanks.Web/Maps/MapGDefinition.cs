using System.Windows.Controls;
using FarseerGames.FarseerPhysics;

namespace Tanks.Web.Maps
{
    public class MapGDefinition : MapDefinition
    {
        public MapGDefinition(PhysicsSimulator simulator) : base(simulator) { }

        protected override UserControl GetMapControl()
        {
            return new MapG();
        }

    }
}
