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
            Args.SkillId = ReadDword() - 0x4000000;
            Args.IsDelayStart = ReadByte() == 1;
            Args.StartPosition = new WorldPosition
                                     {
                                         X = Single(),
                                         Y = Single(),
                                         Z = Single(),
                                         Heading = (short) ReadWord()
                                     };
        }

        public override void Process()
        {
            PlayerLogic.UseSkill(Connection, Args);
        }
    }
}