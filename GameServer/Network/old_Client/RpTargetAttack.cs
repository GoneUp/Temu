using Tera.Communication.Logic;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Client
{
    public class RpTargetAttack : ARecvPacket
    {
        protected UseSkillArgs Args = new UseSkillArgs {IsTargetAttack = true};

        public override void Read()
        {
            short targetCount = (short) ReadWord();
            ReadWord(); //Shifts
            ReadDword(); //Shifts

            Args.SkillId = ReadDword() - 0x4000000;

            Args.StartPosition.X = Single();
            Args.StartPosition.Y = Single();
            Args.StartPosition.Z = Single();
            Args.StartPosition.Heading = (short)ReadWord();
            ReadByte(); //unk

            if (targetCount-- > 0)
            {
                ReadDword(); //shifts
                ReadDword();
                Args.AddTarget(ReadLong());
            }

            ReadDword(); //shifts
            Args.TargetPosition.X = Single();
            Args.TargetPosition.Y = Single();
            Args.TargetPosition.Z = Single();

            //Other Targets
            while (targetCount-- > 0)
            {
                ReadDword(); //shifts
                ReadDword();
                ReadLong(); //TargetUid
            }
        }

        public override void Process()
        {
            PlayerLogic.UseSkill(Connection, Args);
        }
    }
}