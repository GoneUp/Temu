using System.IO;

namespace Tera.Network.old_Server
{
    public class SpDeathDialog : ASendPacket
    {
        protected int CurrentSection;

        public SpDeathDialog(int currentSection)
        {
            CurrentSection = currentSection;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, 30); //Timeout in minutes
            WriteDword(writer, CurrentSection);
            WriteDword(writer, 0);
            WriteWord(writer, 0);
        }
    }
}