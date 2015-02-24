namespace Tera.Network.old_Client
{
    public class RpChatPrivate : ARecvPacket
    {
        protected string Name;
        protected string Message;

        public override void Read()
        {
            ReadWord(); //0x0800
            ReadWord(); //0x2800
            Name = ReadString();
            Message = ReadString();
        }

        public override void Process()
        {
            Communication.Global.ChatService.ProcessPrivateMessage(Connection, Name, Message);
        }
    }
}