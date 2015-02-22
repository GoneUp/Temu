using Tera.Data.Enums.Craft;
using Tera.Data.Enums.Gather;
using Tera.Data.Structures.Player;

namespace Tera.Communication.Interfaces
{
    public interface ICraftLearnService
    {
        void ProcessCraftStat(Player player, CraftStat craftStat);
        void ProcessGatherStat(Player player, TypeName typeName);
    }
}
