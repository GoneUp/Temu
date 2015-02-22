using System.IO;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpUpdateHp : ASendPacket
    {
        protected Creature Creature;
        protected int Diff;
        protected Creature Attacker;

        public SpUpdateHp(Creature creature, int diff, Creature attacker)
        {
            Creature = creature;
            Diff = diff;
            Attacker = attacker;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, Creature.LifeStats.Hp);
            WriteDword(writer, Creature.MaxHp);
            WriteDword(writer, Diff);

            if (Attacker != null)
            {
                WriteDword(writer, 1);
                WriteUid(writer, Attacker);
                WriteUid(writer, Creature);
            }
            else
            {
                WriteDword(writer, 0);
                WriteUid(writer, Creature);
                WriteLong(writer, 0);
            }
        }
    }
}