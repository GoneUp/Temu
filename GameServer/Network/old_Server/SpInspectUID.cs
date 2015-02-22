using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpInspectUid : ASendPacket
    {
        protected Player Player;

        public SpInspectUid(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, 0);
            WriteUid(writer, Player);
        }
    }
}
