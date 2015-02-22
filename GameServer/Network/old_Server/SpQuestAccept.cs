using System.IO;

namespace Tera.Network.old_Server
{
    public class SpQuestAccept : ASendPacket
    {
        protected string Options;

        public SpQuestAccept(string options)
        {
            Options = options;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 0x0006); //shift??
            WriteWord(writer, 0x0040); //shift??
            WriteByte(writer, "3600320034000B00510075006500730074004E0061006D0065000B00");
            WriteString(writer, Options);
        }
    }
}