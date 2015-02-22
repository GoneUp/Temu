using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Player;

namespace Tera.Communication.Interfaces
{
    public interface IStatsService : IComponent
    {
        void Init();
        CreatureBaseStats InitStats(Creature creature);
        CreatureBaseStats GetBaseStats(Player player);
        void UpdateStats(Creature creature);
    }
}