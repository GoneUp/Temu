using Tera.Data.Structures;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.World.Requests;

namespace Tera.Network.old_Client
{
    public class RpGuildAddMemberRequest : ARecvPacket
    {
        protected long TargetUid;

        public override void Read()
        {
            TargetUid = ReadLong();
        }

        public override void Process()
        {
            Player target = (Player)Uid.GetObject(TargetUid);
            if(target != null)
                Communication.Global.ActionEngine.AddRequest(new GuildInvite(target) { Owner = Connection.Player });
        }
    }
}