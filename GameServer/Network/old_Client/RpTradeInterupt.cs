namespace Tera.Network.old_Client
{
    public class RpTradeInterupt : ARecvPacket
    {
        protected int Uid;

        public override void Read()
        {
            ReadLong(); //my uid
            ReadLong(); //other uid
            Uid = ReadDword(); //TradeUid
        }

        public override void Process()
        {
            Communication.Global.PlayerService.InterruptNpcTraid(Connection.Player);
        }
    }
}