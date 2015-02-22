using System.IO;
using Tera.Data.Structures.Gather;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public enum GatherEndCode
    {
        Normal = 3,
        Failed = 2,
        Rupted = 1
    }

    public class SpGatherEnd : ASendPacket
    {
        protected Player Player;
        protected Gather Gather;
        protected GatherEndCode EndCode;

        public SpGatherEnd(Player player, Gather gather, GatherEndCode endCode)
        {
            Player = player;
            Gather = gather;
            EndCode = endCode;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteUid(writer, Gather);
            WriteDword(writer, EndCode.GetHashCode());
        }
    }
}