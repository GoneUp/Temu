using System.IO;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Npc;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.SkillEngine;

namespace Tera.Network.old_Server
{
    public class SpAttackEnd : ASendPacket //len 42
    {
        protected Creature Creature;
        protected Attack Attack;

        public SpAttackEnd(Creature creature, Attack attack)
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

            WriteUid(writer, Creature);
            WriteSingle(writer, Creature.Position.X);
            WriteSingle(writer, Creature.Position.Y);
            WriteSingle(writer, Creature.Position.Z);
            WriteWord(writer, Creature.Position.Heading);
            WriteDword(writer, model);
            WriteDword(writer, Attack.Args.SkillId + 0x4000000);
            WriteDword(writer, 0);
            WriteDword(writer, Attack.UID);
        }
    }
}