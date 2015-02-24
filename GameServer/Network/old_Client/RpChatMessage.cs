using Tera.Communication.Logic;
using Tera.Data.Enums;

namespace Tera.Network.old_Client
{
    public class RpChatMessage : ARecvPacket
    {
        protected short Length;
        protected ChatType Type;
        protected string Message;

        public override void Read()
        {
            Length = (short) ReadWord();
            Type = (ChatType) ReadDword();
            Message = ReadString();
        }

        public override void Process()
        {
            PlayerLogic.ProcessChatMessage(Connection, Message, Type);
        }
    }
}