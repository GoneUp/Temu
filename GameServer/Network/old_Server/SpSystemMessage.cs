using System.IO;
using System.Text;

namespace Tera.Network.old_Server
{
    public class SpSystemMessage : ASendPacket
    {
        protected string[] Args;

        public SpSystemMessage(string[] args)
        {
            Args = args;
        }

        public override void Write(BinaryWriter writer)
        {
            for (int i = 0; i < Args.Length; i++)
            {
                WriteWord(writer, (short) (i == 0 ? 6 : 11));
                writer.Write(Encoding.Unicode.GetBytes(Args[i]));
            }

            WriteWord(writer, 0);
        }
    }
}