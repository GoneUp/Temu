using System.IO;

namespace Tera.Network.old_Server
{
    public class SpSystemNotice : ASendPacket
    {
        protected string Text;
        protected int TimeoutSeconds;

        public SpSystemNotice(string text, int timeoutSeconds = 1)
        {
            Text = text;
            TimeoutSeconds = timeoutSeconds;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 15); //Text shift
            WriteWord(writer, 1);
            WriteByte(writer, 0x99);
            WriteByte(writer, 0xFF);
            WriteByte(writer, 0);
            WriteDword(writer, TimeoutSeconds);
            WriteString(writer, Text);
        }
    }
}