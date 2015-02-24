namespace Tera.Network.old_Client
{
    public class RpChatBlock : ARecvPacket
    {
        protected string NameOfFlooder;

        public override void Read()
        {
            ReadWord();
            NameOfFlooder = ReadString();
        }

        public override void Process()
        {
            
        }
    }
}