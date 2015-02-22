using System.IO;

namespace Tera.Network.old_Server
{
    public class SpHelp : ASendPacket
    {
        protected int MsgId;

        public SpHelp(int msgId)
        {
            MsgId = msgId;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, MsgId);
        }
    }
}