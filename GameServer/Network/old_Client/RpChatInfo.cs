namespace Tera.Network.old_Client
{
    public class RpChatInfo : ARecvPacket
    {
        protected int Type;
        protected string Name;

        public override void Read()
        {
            ReadWord(); //shift
            Type = ReadDword();
            ReadDword();
            Name = ReadString();
        }

        public override void Process()
        {
            Communication.Global.ChatService.SendChatInfo(Connection, Type, Name);
        }
    }
}