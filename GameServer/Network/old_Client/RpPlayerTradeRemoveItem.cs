namespace Tera.Network.old_Client
{
    public class RpPlayerTradeRemoveItem : ARecvPacket
    {
        protected int Slot;
        protected int Count;

        public override void Read()
        {
            ReadLong(); // My uid
            ReadLong(); // Other uid
            ReadDword(); // Trade id
            ReadLong(); // My uid
            Slot = ReadDword();
            Count = ReadDword(); // count
            //Money = ReadLong();
        }

        public override void Process()
        {
            if (Slot >= 0 && Count > 0)
                Communication.Global.TradeService.RemoveItem(Connection.Player, Slot, Count);
        }
    }
}
