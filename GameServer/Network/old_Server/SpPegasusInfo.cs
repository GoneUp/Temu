using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpPegasusInfo : ASendPacket
    {
        protected Player Player;
        protected int Flag;

        public SpPegasusInfo(Player player, int flag = 1)
        {
            Flag = flag;
            Player = player;
        }
        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteDword(writer, Flag);
        }
    }
}
