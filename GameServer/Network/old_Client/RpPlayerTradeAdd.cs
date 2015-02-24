namespace Tera.Network.old_Client
{
    public class RpPlayerTradeAdd : ARecvPacket
    {
        protected int Slot;
        protected int Counter;
        protected long Money;

        public override void Read()
        {
            ReadLong(); // SellerUid 
            ReadLong(); // TargetUid 
            ReadDword(); // Trade id
            ReadLong(); //seller uid too
            Slot = ReadDword(); // omg... Slot - ??
            Counter = ReadDword();
            Money = ReadLong(); // Money
        }

        public override void Process()
        {
            if(Slot > 0)
                Communication.Global.TradeService.AddItem(Connection.Player, Slot - 20, Counter);

            if(Money > 0)
                Communication.Global.TradeService.ChangeMoney(Connection.Player, Money);
        }
    }
}