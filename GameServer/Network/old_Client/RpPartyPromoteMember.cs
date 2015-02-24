namespace Tera.Network.old_Client
{
    class RpPartyPromoteMember : ARecvPacket
    {
        protected int PlayerId;

        public override void Read()
        {
            ReadDword(); //11
            PlayerId = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.PartyService.PromotePlayer(Connection.Player, PlayerId);
        }
    }
}
