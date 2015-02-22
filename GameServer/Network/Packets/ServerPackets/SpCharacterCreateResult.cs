using System.IO;

namespace Tera.Network.Packets.ServerPackets
{
    public class SpCharacterCreateResult : ASendPacket
    {
        protected byte IsValid;

        public SpCharacterCreateResult(byte isValid)
        {
            IsValid = isValid;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteByte(writer, IsValid);
        }
    }
}