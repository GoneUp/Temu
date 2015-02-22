using System.IO;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpNpcHpMp : ASendPacket
    {
        protected Creature Creature;
        protected float Hp;
        protected bool IsFreandly;

        public SpNpcHpMp(Creature creature, bool isFreandly = false)
        {
            Creature = creature;
            IsFreandly = isFreandly;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Creature);
            WriteSingle(writer, (Creature.LifeStats.Hp/(Creature.MaxHp/100F))/100F);
            WriteByte(writer, (byte) (IsFreandly ? 0 : 1));

            WriteDword(writer, 0x00000000);
            WriteDword(writer, 0x00000000);
            WriteDword(writer, 0x401F0000);
            WriteDword(writer, 0x05000000);
        }
    }
}