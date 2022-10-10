using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class Era
    {
        public int id;
        public string name;
        public List<GrapableObject> grapableObjects;
        public List<Npc> npcs;
        public bool isFree = true;

        public Era(int id, string name, List<GrapableObject> grapableObjects, List<Npc> npcs)
        {
            this.id = id;
            this.name = name;
            this.grapableObjects = grapableObjects;
            this.npcs = npcs;
        }
    }
}