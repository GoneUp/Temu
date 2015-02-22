using System.IO;

namespace Tera.Network.old_Server
{
    public class SpRelogWindow : ASendPacket
    {
        protected int Time;

        public SpRelogWindow(int time)
        {
            Time = time;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, Time);
        }
    }
}