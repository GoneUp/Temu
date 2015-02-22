using Tera.Communication.Logic;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Client
{
    public class RpUseDelaySkill : ARecvPacket
    {
        protected UseSkillArgs Args = new UseSkillArgs {IsDelaySkill = true};

        public override void Read()
        {
            Args.SkillId = ReadD() - 0x4000000;
            Args.IsDelayStart = ReadC() == 1;
            Args.StartPosition = new WorldPosition
                                     {
                                         X = ReadF(),
                                         Y = ReadF(),
                                         Z = ReadF(),
                                         Heading = (short) ReadH()
                                     };
        }

        public override void Process()
        {
            PlayerLogic.UseSkill(Connection, Args);
        }
    }
}