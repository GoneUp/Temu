using System.IO;
using System.Linq;

namespace Tera.Network.Packets.ServerPackets
{
    public class SpPremiumInfo : ASendPacket
    {
        protected int[] Version;

        public SpPremiumInfo(int[] version)
        {
            Version = version;
        }

        public override void Write(BinaryWriter writer)
        {
            for (int i = 0; i < Version.Count(); i++)
            {
                
            }

            WriteByte(writer, (byte) (Version == OpCodes.Version ? 1 : 0));
        }
    }
}