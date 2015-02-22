using Tera.Data.Structures.Player;

namespace Tera.Communication.Interfaces
{
    public interface IMountService : IComponent
    {
        void UnkQuestion(Player player, int unk);
        void ProcessMount(Player player, int skillId);
        void PlayerEnterWorld(Player player);
    }
}
