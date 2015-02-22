using System.Collections.Generic;
using System.IO;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.SkillEngine;

namespace Tera.Network.old_Server
{
    public class SpTraidSkillList : ASendPacket
    {
        protected Player Player;
        protected List<UserSkill> SkillList;

        public SpTraidSkillList(Player player, List<UserSkill> skillList)
        {
            Player = player;
            SkillList = skillList;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) SkillList.Count);
            short nextShift = (short)writer.BaseStream.Position;
            WriteWord(writer, 0);

            for (int i = 0; i < SkillList.Count; i++)
            {
                writer.Seek(nextShift, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteWord(writer, (short) writer.BaseStream.Length); //now shift
                nextShift = (short) writer.BaseStream.Position;
                WriteWord(writer, 0); //next shift

                WriteWord(writer, (short) (SkillList[i].PrevSkillId != 0 ? 1 : 0));
                short prevSkillShift = (short)writer.BaseStream.Position;
                WriteWord(writer, 0);

                WriteDword(writer, 0); //Unk

                WriteDword(writer, SkillList[i].SkillId);
                WriteByte(writer, 1); // IsActive
                WriteDword(writer, SkillList[i].Cost);
                WriteDword(writer, SkillList[i].Level);
                WriteByte(writer, (byte) (Player.GetLevel() >= SkillList[i].Level ? 1 : 0));

                if (SkillList[i].PrevSkillId != 0)
                {
                    writer.Seek(prevSkillShift, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);

                    WriteWord(writer, (short) writer.BaseStream.Length);
                    WriteWord(writer, 0); //next shift

                    WriteDword(writer, SkillList[i].PrevSkillId);
                    WriteByte(writer, 1); //IsActive
                }
            }
        }
    }
}