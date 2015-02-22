using System.IO;
using Tera.Data.Structures.Creature;
using Utils;

namespace Tera.Network.old_Server
{
    public class SpNpcHpWindow : ASendPacket
    {
        protected Creature Creature;

        public SpNpcHpWindow(Creature creature)
        {
            Creature = creature;
        }

        public override void Write(BinaryWriter writer)
        {
            //B2C0 0000000000000000 0ECE020000800B00 734F633F 00000000 09000000 00000000000000000000000000000000401F000005000000
            //B2C0 0100380000000000 F042030000800B00 F734423E 00000000 09000000 00000000000000000000000000000000401F000005000000 38000000E02A0900E803000001000000

            lock (Creature.EffectsLock)
            {
                WriteWord(writer, (short) Creature.Effects.Count);
                int effectShift = (int) writer.BaseStream.Position;
                WriteWord(writer, 0); //first Abnormal shift
                WriteDword(writer, 0); //unk
                WriteUid(writer, Creature);
                WriteSingle(writer, (Creature.LifeStats.Hp/(Creature.MaxHp/100F))/100F);
                WriteDword(writer, 0);
                WriteDword(writer, Creature.GetLevel()); //npc level
                WriteByte(writer, "00000000000000000000000000000000401F000005000000".HexSringToBytes()); // ololo o_O

                for (int i = 0; i < Creature.Effects.Count; i++)
                {
                    writer.Seek(effectShift, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);

                    WriteWord(writer, (short) writer.BaseStream.Length);
                    effectShift = (int) writer.BaseStream.Position;
                    WriteWord(writer, 0); //posible next Abnormal shift
                    WriteDword(writer, Creature.Effects[i].Abnormality.Id);
                    WriteDword(writer, Creature.Effects[i].TimeRemain);
                    WriteDword(writer, 1);
                }
            }
        }
    }
}