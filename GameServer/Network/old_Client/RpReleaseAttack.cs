using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    public class RpReleaseAttack : ARecvPacket
    {
        protected int AttackUid;
        protected int Type;

        public override void Read()
        {
            AttackUid = ReadD();
            Type = ReadD();
        }

        public override void Process()
        {
            PlayerLogic.ReleaseAttack(Connection, AttackUid, Type);
        }
    }
}