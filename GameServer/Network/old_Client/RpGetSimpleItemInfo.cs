using Tera.Network.old_Server;

namespace Tera.Network.old_Client
{
    public class RpGetSimpleItemInfo : ARecvPacket
    {
        protected int ItemId;

        public override void Read()
        {
            ItemId = ReadD();
        }

        public override void Process()
        {
            new SpSimpleItemInfo(ItemId).Send(Connection);
        }
    }
}