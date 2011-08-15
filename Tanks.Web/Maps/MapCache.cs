using System;
using System.Reflection;
using System.Collections.Generic;
using FarseerGames.FarseerPhysics;

namespace Tanks.Web.Maps
{
    public class MapCache
    {
        Dictionary<string, MapDefinition> maps;
        static MapCache instance;

        MapCache()
        {
            this.maps = new Dictionary<string, MapDefinition>();
        }

        public static MapCache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MapCache();
                }
                return instance;
            }
        }

        public MapDefinition GetMap(string name, PhysicsSimulator simulator)
        {
            if (this.maps.ContainsKey(name))
            {
                return this.maps[name];
            }

            string typeName = "Tanks.Web.Maps.{0}Definition";
            typeName = string.Format(typeName, name);

            Type mapType = Type.GetType(typeName);
            MapDefinition definition = Activator.CreateInstance(mapType, simulator) as MapDefinition;
            this.maps.Add(name, definition);
            return definition;


        }
    }
}
