using System.IO;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpUpdateMp : ASendPacket
    {
        protected Creature Creature;
        protected int Diff;
        protected Creature Attacker;

        public SpUpdateMp(Creature creature, int diff, Creature attacker)
        {
            Creature = creature;
            Diff = diff;
            Attacker = attacker;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, Creature.LifeStats.Mp);
            WriteDword(writer, Creature.MaxMp);
            WriteDword(writer, Diff); //added Mp

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