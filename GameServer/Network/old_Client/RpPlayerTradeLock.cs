namespace Tera.Network.old_Client
{
    public class RpPlayerTradeLock : ARecvPacket
    {
        public override void Read()
        {
            ReadLong(); // my uid
            ReadLong(); //other uid
            ReadDword(); //trade Uid
            ReadLong(); //my uid too
        }

        public override void Process()
        {
            Communication.Global.TradeService.Lock(Connection.Player);
        }
    }
}