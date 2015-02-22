namespace Tera.Network.old_Client
{
    public class RpGuildRemoveMember : ARecvPacket
    {
        protected string Name;

        public override void Read()
        {
            ReadH();
            Name = ReadS();
        }

        public override void Process()
        {
            Communication.Global.GuildService.KickMember(Connection.Player, Name);
        }
    }
}