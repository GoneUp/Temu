using Tera.Communication.Logic;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Client
{
    public class RpAttack : ARecvPacket
    {
        protected UseSkillArgs Args = new UseSkillArgs();

        public override void Read()
        {
            Args.SkillId = ReadDword() - 0x4000000;

            Args.StartPosition.Heading = (short)ReadWord();
            Args.StartPosition.X = Single();
            Args.StartPosition.Y = Single();
            Args.StartPosition.Z = Single();

            Args.TargetPosition.X = Single();
            Args.TargetPosition.Y = Single();
            Args.TargetPosition.Z = Single();

            ReadByte(); //mb target count
            ReadByte(); //mb target count
            ReadByte(); //???
            ReadLong(); //mb targetid
        }

        public override void Process()
        {
            PlayerLogic.UseSkill(Connection, Args);
        }
    }
}