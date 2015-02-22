using System.IO;

namespace Tera.Network.Packets.ServerPackets
{
    public class SpCharacterDelete : ASendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteByte(writer, 1);
        }
    }
}