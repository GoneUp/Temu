using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpSkillList : ASendPacket
    {
        protected Player Player;

        public SpSkillList(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) Player.Skills.Count);
            WriteWord(writer, 8);

            short shift = 8;
            int counter = 1;
            foreach (var skill in Player.Skills)
            {
                WriteWord(writer, shift);
                shift += 9;
                WriteWord(writer, (short) (counter++ != Player.Skills.Count ? shift : 0));
                WriteDword(writer, skill);

         

                WriteByte(writer,
                       (byte)
                       (!Data.Data.UserSkills[Player.TemplateId].ContainsKey(skill) ||
                        Data.Data.UserSkills[Player.TemplateId][skill].IsActive
                            ? 1
                            : 0));
            }
            //B089 0700 0800 0800 1100 74270000 01 1100 1A00 C4EA0000 01 1A00 2300 C4A28900 01 2300 2C00 12270000002C003500644B00000035003E00654B0000003E000000664B000000
        }
    }
}