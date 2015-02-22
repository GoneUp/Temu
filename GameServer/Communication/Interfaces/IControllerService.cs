using Tera.Data.Interfaces;
using Tera.Data.Structures.Player;

namespace Tera.Communication.Interfaces
{
    public interface IControllerService : IComponent
    {
        void PlayerEnterWorld(Player player);
        void PlayerEndGame(Player player);

        void TrySetDefaultController(Player player);
        void SetController(Player player, IController controller);
    }
}
