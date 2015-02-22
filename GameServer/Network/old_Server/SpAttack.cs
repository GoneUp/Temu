using System.IO;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Npc;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.SkillEngine;

namespace Tera.Network.old_Server
{
    public class SpAttack : ASendPacket
    {
        protected Creature Creature;
        protected Attack Attack;

        public SpAttack(Creature creature, Attack attack)
        {
            Creature = creature;
            Attack = attack;
        }

        public override void Write(BinaryWriter writer)
        {
            int model = 0;
            if (Creature is Player)
                model = ((Player) Creature).PlayerData.SexRaceClass;
            else if (Creature is Npc)
                model = ((Npc) Creature).SpawnTemplate.NpcId;

            if (Attack.Stage == 0)
            {
                WriteWord(writer, 1); //Unk count
                WriteWord(writer, 50); //Shift
            }
            else
                WriteDword(writer, 0);

            WriteUid(writer, Creature);
            WriteSingle(writer, Attack.Args.StartPosition.X);
            WriteSingle(writer, Attack.Args.StartPosition.Y);
            WriteSingle(writer, Attack.Args.StartPosition.Z);
            WriteWord(writer, Attack.Args.StartPosition.Heading);
            WriteDword(writer, model);
            WriteDword(writer, Attack.Args.SkillId + 0x4000000);
            WriteDword(writer, Attack.Stage);
            WriteSingle(writer, Creature.Attack.Speed);

            if (Creature is Player)
                WriteDword(writer, Attack.UID);
            else
                WriteDword(writer, 0);

            if (Attack.Stage == 0)
            {
                WriteByte(writer, "000032000000000000000000803F0000803F");
                WriteDword(writer, Attack.UID);
            }
        }
    }
}