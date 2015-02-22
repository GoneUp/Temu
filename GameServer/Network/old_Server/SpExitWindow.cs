using System.IO;

namespace Tera.Network.old_Server
{
    public class SpExitWindow : ASendPacket
    {
        protected int Timeout;

        public SpExitWindow(int timeout)
        {
            Timeout = timeout;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, Timeout);
        }
    }
}
