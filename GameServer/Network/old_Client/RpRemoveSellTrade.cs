namespace Tera.Network.old_Client
{
    public class RpRemoveSellTrade : ARecvPacket
    {
        protected long PlayerUid;
        protected int DialogUid; //not sure
        protected int ItemId;
        protected int ItemCounter;

        public override void Read()
        {
            PlayerUid = ReadLong();
            DialogUid = ReadDword();
            ItemId = ReadDword();
            ItemCounter = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.PlayerService.RemoveSellItemsFromNpcTrade(Connection.Player, ItemId, ItemCounter, DialogUid);
        }
    }
}