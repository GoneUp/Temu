using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpMountHide : ASendPacket
    {
        protected Player Player;
        protected int Mount;

        public SpMountHide(Player player, int mount)
        {
            Player = player;
            Mount = mount;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteDword(writer, Mount + 111110); //MountSkillId
        }
    }
}