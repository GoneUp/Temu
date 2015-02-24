namespace Tera.Network.old_Client
{
    public class RpRemoveBuyTrade : ARecvPacket
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
            Communication.Global.PlayerService.RemoveBuyItemsFromNpcTrade(Connection.Player, ItemId, ItemCounter, DialogUid);
        }
    }
}