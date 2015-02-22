using System.IO;
using Tera.Data.Structures.Gather;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpGatherStart : ASendPacket
    {
        protected Player Player;
        protected Gather Gather;

        public SpGatherStart(Player player, Gather gather)
        {
            Player = player;
            Gather = gather;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteUid(writer, Gather);
        }
    }
}