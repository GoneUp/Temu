namespace Tera.Network.old_Client
{
    public class RpSellItem : ARecvPacket
    {
        protected int ItemId;
        protected int Counter;
        protected long PlayerUid;
        protected int Slot;

        public override void Read()
        {
            PlayerUid = ReadLong();
            ReadDword(); // dialog uid?
            ItemId = ReadDword();
            Counter = ReadDword();
            Slot = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.PlayerService.AddItemsToNpcSell(Connection.Player, ItemId, Counter, Slot);
        }
    }
}