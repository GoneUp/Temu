namespace Tera.Network.old_Client
{
    public class RpCompleteTraid : ARecvPacket
    {
        protected int DialogUid;

        public override void Read()
        {
            ReadDword(); //04070000
            ReadDword(); //0
            DialogUid = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.PlayerService.CompleteNpcTraid(Connection.Player, DialogUid);
        }
    }
}