using Tera.Data.Structures.Player;
using Tera.Data.Structures.World.Requests;

namespace Tera.Communication.Interfaces
{
    public interface IDuelService : IComponent
    {
        void StartDuel(Player initiator, Player initiated, Request request);
        void ProcessDamage(Player player);
        void FinishDuel(Player winner);
        void PlayerLeaveWorld(Player player);
    }
}
