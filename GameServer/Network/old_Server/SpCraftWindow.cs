using System.IO;
using Tera.Data.Enums.Craft;

namespace Tera.Network.old_Server
{
    public class SpCraftWindow : ASendPacket
    {
        protected CraftStat CraftStat;

        public SpCraftWindow(CraftStat craftStat)
        {
            CraftStat = craftStat;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, CraftStat.GetHashCode());
        }
    }
}
