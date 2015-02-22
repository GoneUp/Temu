using System.IO;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpShowIcon : ASendPacket
    {
        protected Creature Creature;
        protected int Icon;

        public SpShowIcon(Creature creature, int icon)
        {
            Creature = creature;
            Icon = icon;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Creature);
            WriteDword(writer, Icon);
        }
    }
}