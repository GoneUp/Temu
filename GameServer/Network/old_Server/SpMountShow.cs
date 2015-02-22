using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpMountShow : ASendPacket
    {
        protected Player Player;
        protected int Mount;
        protected int SkillId;

        public SpMountShow(Player player, int mount, int skillId)
        {
            Player = player;
            Mount = mount;
            SkillId = skillId;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteDword(writer, Mount);
            WriteDword(writer, SkillId); //MountSkillId
        }
    }
}