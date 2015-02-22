namespace Tera.Network.old_Client
{
    public class RpPartyLeave : ARecvPacket
    {
        public override void Read()
        {
            //nothing
        }

        public override void Process()
        {
            Communication.Global.PartyService.LeaveParty(Connection.Player);
        }
    }
}