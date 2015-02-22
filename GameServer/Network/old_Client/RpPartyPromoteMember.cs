namespace Tera.Network.old_Client
{
    class RpPartyPromoteMember : ARecvPacket
    {
        protected int PlayerId;

        public override void Read()
        {
            ReadD(); //11
            PlayerId = ReadD();
        }

        public override void Process()
        {
            Communication.Global.PartyService.PromotePlayer(Connection.Player, PlayerId);
        }
    }
}
