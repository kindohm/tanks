﻿using System.Windows.Controls;
using FarseerGames.FarseerPhysics;

namespace Tanks.Web.Maps
{
    public class MapADefinition : MapDefinition
    {
        public MapADefinition(PhysicsSimulator simulator) : base(simulator) { }

        protected override UserControl GetMapControl()
        {
            return new MapA();
        }

    }
}
