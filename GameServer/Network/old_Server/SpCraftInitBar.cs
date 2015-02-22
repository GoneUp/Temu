using System.IO;

namespace Tera.Network.old_Server
{
    public class SpCraftInitBar : ASendPacket
    {
        protected byte Unk;
        protected int MaxChance;

        public SpCraftInitBar(byte unk = 1, int maxChance = 100)
        {
            Unk = unk;
            MaxChance = maxChance;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteByte(writer, Unk);
            WriteDword(writer, MaxChance);
        }
    }
}
