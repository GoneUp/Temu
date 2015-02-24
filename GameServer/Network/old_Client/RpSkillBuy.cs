using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    public class RpSkillBuy : ARecvPacket
    {
        protected int SkillId;
        protected bool IsActive;

        public override void Read()
        {
            ReadDword(); //DialogId
            SkillId = ReadDword();
            IsActive = ReadByte() > 0;
        }

        public override void Process()
        {
            PlayerLogic.BuySkill(Connection.Player, SkillId, IsActive);
        }
    }
}