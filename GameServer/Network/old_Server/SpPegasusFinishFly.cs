using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpPegasusFinishFly : ASendPacket
    {
        protected Player Player;

        public SpPegasusFinishFly(Player player)
        {
            Player = player;
        }
        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
        }
    }
}
