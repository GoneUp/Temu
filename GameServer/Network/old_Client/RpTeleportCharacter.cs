namespace Tera.Network.old_Client
{
    public class RpTeleportCharacter : ARecvPacket
    {
        protected int TeleportId;

        public override void Read()
        {
            TeleportId = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.TeleportService.StartPegasusFlight(Connection.Player, TeleportId);
        }
    }
}