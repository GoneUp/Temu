using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    public class RpReleaseAttack : ARecvPacket
    {
        protected int AttackUid;
        protected int Type;

        public override void Read()
        {
            AttackUid = ReadDword();
            Type = ReadDword();
        }

        public override void Process()
        {
            PlayerLogic.ReleaseAttack(Connection, AttackUid, Type);
        }
    }
}