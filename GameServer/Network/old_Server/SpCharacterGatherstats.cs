using System.IO;
using Tera.Data.Enums.Gather;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterGatherstats : ASendPacket
    {
        protected PlayerCraftStats GatherStats;

        public SpCharacterGatherstats(PlayerCraftStats gatherStats)
        {
            GatherStats = gatherStats;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, GatherStats.GetGatherStat(TypeName.Energy));
            WriteWord(writer, GatherStats.GetGatherStat(TypeName.Herb));
            WriteWord(writer, 0); //unk, mb bughunting
            WriteWord(writer, GatherStats.GetGatherStat(TypeName.Mine));
            WriteLong(writer, 0);
        }
    }
}