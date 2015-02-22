using Tera.Data.Structures.Player;

namespace Tera.Data.Interfaces
{
    public interface IController
    {
        void Start(Player player);
        void Release();
        void Action();
    }
}