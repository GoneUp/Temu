namespace Tera.Network.old_Client
{
    public class RpAddToTrade : ARecvPacket
    {
        protected int DialogUid; // not shure...
        protected int ItemId;
        protected int ItemsCounter;
        protected long PlayerUid;

        public override void Read()
        {
            PlayerUid = ReadLong();
            DialogUid = ReadDword();
            ItemId = ReadDword();
            ItemsCounter = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.PlayerService.AddItemsToNpcBuy(Connection.Player, ItemId, ItemsCounter);
        }
    }
}