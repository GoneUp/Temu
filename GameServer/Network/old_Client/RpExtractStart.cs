namespace Tera.Network.old_Client
{
    public class RpExtractStart : ARecvPacket
    {
        protected int Type;

        public override void Read()
        {
            Type = ReadDword();
        }

        public override void Process()
        {
            
        }
    }
}