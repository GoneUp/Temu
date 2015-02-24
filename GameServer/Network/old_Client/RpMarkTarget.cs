using Tera.Communication.Logic;
using Tera.Data.Structures;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Client
{
    public class RpMarkTarget : ARecvPacket
    {
        protected long TargetUid;
        protected int SkillId;

        public override void Read()
        {
            TargetUid = ReadLong();
            SkillId = ReadDword() - 0x4000000;
        }

        public override void Process()
        {
            Creature target = TeraObject.GetObject(TargetUid) as Creature;

            if (target != null)
                PlayerLogic.MarkTarget(Connection, target, SkillId);
        }
    }
}