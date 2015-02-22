using System.IO;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpAttackResult : ASendPacket
    {
        protected Creature Creature;
        protected Creature Target;

        protected int SkillId;

        protected int AttackUid;
        protected int AttackType;
        protected int Damage;

        protected VisualEffect VisualEffect;

        public SpAttackResult(Creature creature, int skillId, AttackResult atk)
        {
            Creature = creature;
            Target = atk.Target;

            SkillId = skillId;

            AttackUid = atk.AttackUid;
            AttackType = atk.AttackType.GetHashCode();
            Damage = atk.Damage;

            VisualEffect = atk.VisualEffect;

            if (VisualEffect != null)
                AttackType |= 1 << 24;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) (VisualEffect != null ? VisualEffect.Times.Count : 0)); //count of timesteps
            int timesShift = (int) writer.BaseStream.Position;
            WriteWord(writer, 0); //shift

            WriteUid(writer, Creature); //attacker uniqueid
            WriteUid(writer, Target); //target unique id

            WriteDword(writer, Creature.GetModelId());
            WriteDword(writer, SkillId + 0x4000000);
            WriteLong(writer, 0);
            WriteDword(writer, AttackUid);

            WriteDword(writer, 0);

            WriteDword(writer, Damage); //damage ;)
            WriteDword(writer, AttackType); // 1 - Normal, 65537 - Critical
            WriteByte(writer, 0);

            if (VisualEffect != null)
            {
                WriteSingle(writer, VisualEffect.Position.X);
                WriteSingle(writer, VisualEffect.Position.Y);
                WriteSingle(writer, VisualEffect.Position.Z);
                WriteWord(writer, VisualEffect.Position.Heading);

                WriteDword(writer, VisualEffect.Type.GetHashCode());
                WriteDword(writer, 0);
                WriteDword(writer, AttackUid);

                foreach (int time in VisualEffect.Times)
                {
                    writer.Seek(timesShift, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);

                    WriteWord(writer, (short) writer.BaseStream.Position);
                    timesShift = (int) writer.BaseStream.Position;
                    WriteWord(writer, 0);

                    WriteDword(writer, time);
                    WriteByte(writer, "0000803F0000803F000080BF");
                }
            }
            else
                WriteByte(writer, new byte[27]); //unk
        }
    }
}