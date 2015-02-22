using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Player;

namespace Tera.Communication.Interfaces
{
    public interface IObserverService : IComponent
    {
        void AddObserved(Player player, Creature creature);
        void RemoveObserved(Player player, Creature creature);
        void NotifyHpChanged(Creature creature);
        void NotifyMpChanged(Creature creature);
    }
}