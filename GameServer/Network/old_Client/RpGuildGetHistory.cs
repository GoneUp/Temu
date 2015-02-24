namespace Tera.Network.old_Client
{
    public class RpGuildGetHistory : ARecvPacket
    {
        protected int Unk;

        public override void Read()
        {
            Unk = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.GuildService.SendGuildHistory(Connection.Player);
        }
    }
}