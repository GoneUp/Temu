using Tera.Communication.Interfaces;
using Tera.Data.Enums.Craft;
using Tera.Data.Enums.Gather;
using Tera.Data.Structures.Player;
using Tera.Network.old_Server;

namespace Tera.Services
{
    class CraftLearnService : ICraftLearnService
    {
        public void ProcessCraftStat(Player player, CraftStat craftStat)
        {
            player.PlayerCraftStats.ProgressCraftStat(craftStat);
            new SpCharacterCraftStats(player).Send(player.Connection);
        }

        public void ProcessGatherStat(Player player, TypeName typeName)
        {
            player.PlayerCraftStats.ProgressGatherStat(typeName);
            new SpCharacterGatherstats(player.PlayerCraftStats).Send(player.Connection);
        }
    }
}
