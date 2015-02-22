using System.IO;

namespace Tera.Network.Packets.ServerPackets
{
    public class SpSendVersion : ASendPacket
    {
        protected int Version;

        public SpSendVersion(int version)
        {
            Version = version;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteByte(writer, (byte) (Version == OpCodes.Version ? 1 : 0));
        }
    }
}