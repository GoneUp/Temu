using System.IO;

namespace Tera.Network.old_Server
{
    public class SpForFun : ASendPacket
    {
        protected string Text;

        public SpForFun(string text)
        {
            Text = text;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteString(writer, Text);
        }
    }
}