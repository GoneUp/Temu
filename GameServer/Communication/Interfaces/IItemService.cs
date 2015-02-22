using Tera.Data.Structures.Player;
using Tera.Data.Structures.World;

namespace Tera.Communication.Interfaces
{
    public interface IItemService : IComponent
    {
        void ItemUse(Player player, int itemId, WorldPosition position);
        void GetItemInfo(Player player, long itemUid);
    }
}
