using System.IO;

namespace Tera.Network.old_Server
{
    public class SpInspectUnk : ASendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, 0x0e);
            WriteDword(writer, 0x0e);
        }
    }
}
