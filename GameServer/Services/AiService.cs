using Tera.AiEngine;
using Tera.Communication.Interfaces;
using Tera.Data.Interfaces;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Gather;
using Tera.Data.Structures.Npc;
using Tera.Data.Structures.Objects;
using Tera.Data.Structures.Player;

namespace Tera.Services
{
    class AiService : IAiService
    {
        public IAi CreateAi(Creature creature)
        {
            if (creature is Player)
                return new PlayerAi();

            if (creature is Npc)
                return new NpcAi();

            if (creature is Projectile)
                return new ProjectileAi();

            if (creature is Gather)
                return new GatherAi();

            return new DefaultAi();
        }

        public void Action()
        {
            
        }
    }
}
