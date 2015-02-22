using System.IO;

namespace Tera.Network.old_Server
{
    public class SpItemCooldown : ASendPacket
    {
        protected int ItemId;
        protected int Cooldown;

        public SpItemCooldown(int itemid, int cooldown)
        {
            ItemId = itemid;
            Cooldown = cooldown;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, ItemId);
            WriteDword(writer, Cooldown);
        }
    }
}