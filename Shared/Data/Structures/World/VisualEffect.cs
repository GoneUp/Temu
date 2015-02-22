using System.Collections.Generic;
using Tera.Data.Enums;

namespace Tera.Data.Structures.World
{
    public class VisualEffect
    {
        public VisualEffectType Type;

        public List<int> Times = new List<int>();

        public WorldPosition Position;

        public void Release()
        {
            Position = null;
        }
    }
}