namespace Tera.Network.old_Client
{
    public class RpAddToExtract : ARecvPacket
    {
        protected int Type;
        protected long ItemUid;

        public override void Read()
        {
            Type = ReadDword();
            ItemUid = ReadLong();
        }

        public override void Process()
        {
            
        }
    }
}