namespace Tera.Network.old_Client
{
    public class RpGetItemInfo : ARecvPacket
    {
        protected long ItemUid;
        protected int ViewMode;

        public override void Read()
        {
            ReadWord(); // 38
            ViewMode = ReadWord(); // 20 - inventory, 24 - inspect
            ReadWord(); // 0
            ItemUid = ReadLong();
        }

        public override void Process()
        {
            Communication.Global.ItemService.GetItemInfo(Connection.Player, ItemUid);
        }
    }
}