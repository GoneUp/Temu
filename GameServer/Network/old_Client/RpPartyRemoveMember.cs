namespace Tera.Network.old_Client
{
    public class RpPartyRemoveMember : ARecvPacket
    {
        protected int PlayerId;

        public override void Read()
        {
            ReadDword();
            PlayerId = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.PartyService.KickPlayerFromParty(Connection.Player, PlayerId);
        }
    }
}