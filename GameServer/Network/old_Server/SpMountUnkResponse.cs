using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpMountUnkResponse : ASendPacket
    {
        protected Player Player;
        protected int Unk1;

        public SpMountUnkResponse(Player player, int unk1)
        {
            Player = player;
            Unk1 = unk1;
        }
        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteDword(writer, Unk1);
        }
    }
}
