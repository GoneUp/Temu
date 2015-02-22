using System.IO;
using Tera.Data.Enums;
using Tera.Data.Structures.Npc;

namespace Tera.Network.old_Server
{
    public class SpNpcIcon : ASendPacket //len 17
    {
        protected Npc Npc;
        protected QuestStatusIcon Icon;
        protected byte Status; // 0x00, 0x01 or something else %)

        public SpNpcIcon(Npc npc, QuestStatusIcon icon, byte status)
        {
            Npc = npc;
            Icon = icon;
            Status = status;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Npc);
            WriteDword(writer, Icon.GetHashCode());
            WriteByte(writer, Status);
        }
    }
}