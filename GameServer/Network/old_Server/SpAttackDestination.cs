using System.IO;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Npc;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.SkillEngine;

namespace Tera.Network.old_Server
{
    public class SpAttackDestination : ASendPacket
    {
        protected Creature Creature;
        protected Attack Attack;
        protected Creature Target;

        public SpAttackDestination(Creature creature, Attack attack, Creature target = null)
        {
            Creature = creature;
            Attack = attack;
            Target = target;
        }

        public override void Write(BinaryWriter writer)
        {
            int model = 0;

            if (Creature is Player)
                model = ((Player) Creature).PlayerData.SexRaceClass;
            else if (Creature is Npc)
                model = ((Npc) Creature).SpawnTemplate.NpcId;

            if (Target == null)
            {
                WriteDword(writer, 0); //shifts
                WriteByte(writer, "01002000"); //shifts
                WriteUid(writer, Creature);
                WriteDword(writer, model);
                WriteDword(writer, Attack.Args.SkillId + 0x4000000);
                WriteDword(writer, Attack.UID);
                WriteByte(writer, "20000000"); //shifts
            }
            else
            {
                WriteByte(writer, "01002000"); //shifts
                WriteByte(writer, "01003000"); //shifts
                WriteUid(writer, Creature);
                WriteDword(writer, model);
                WriteDword(writer, Attack.Args.SkillId + 0x4000000);
                WriteDword(writer, Attack.UID);
                WriteByte(writer, "20000000"); //shifts
                WriteByte(writer, "00000000"); //shifts
                WriteUid(writer, Target);
                WriteByte(writer, "30000000"); //shifts
            }

            WriteSingle(writer, Attack.Args.TargetPosition.X);
            WriteSingle(writer, Attack.Args.TargetPosition.Y);
            WriteSingle(writer, Attack.Args.TargetPosition.Z);
        }
    }
}