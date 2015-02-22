using System.IO;

namespace Tera.Network.old_Server
{
    public class SpExtractProgressbar : ASendPacket
    {
        protected int Counter;
        protected bool IsForStop;

        public SpExtractProgressbar(int counter, bool isForStop = false)
        {
            Counter = counter;
            IsForStop = isForStop;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) (IsForStop ? 0 : 257)); //oO
            WriteDword(writer, Counter);
        }
    }
}