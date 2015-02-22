using System.IO;

namespace Tera.Network.old_Server
{
    public class SpDuelCounter : ASendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 0x1388);
        }
    }
}