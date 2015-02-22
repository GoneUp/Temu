using System.IO;
using Tera.Data.Structures.Npc;

namespace Tera.Network.old_Server
{
    public class SpNpcTalk : ASendPacket
    {
        protected Npc Npc;
        protected string Options; // Like '@quest:1301001'

        public SpNpcTalk(Npc npc, string options)
        {
            Npc = npc;
            Options = options;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 0); // shift
            WriteUid(writer, Npc);

            writer.Seek(4, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, Options);
        }

        //65D4 0E00 5152040000800B00 4000 7100 7500 6500 7300 7400 3A00 3100 3300 3000 3100 3000 3000 3200 0000
    }
}