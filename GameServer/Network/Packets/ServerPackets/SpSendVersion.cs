using System.IO;
using System.Linq;
using Tera.Data.Enums;

namespace Tera.Network.Packets.ServerPackets
{
    public class SpSendVersion : ASendPacket
    {
        protected int[] Version;

        public SpSendVersion(int[] version)
        {
            Version = version;
        }

        public override void Write(BinaryWriter writer)
        {
            if (GameServer.gameserverConfig.ServerMode == eServerMode.Release)
            {
                 WriteByte(writer, (byte) (Version == OpCodes.Version ? 1 : 0));
            }
            else
            {
                WriteByte(writer, 1);
            }
           
        }
    }
}