using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterBuffs : ASendPacket
    {
        protected Player Player;

        public SpCharacterBuffs(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
       //0773 0500 1600 0100000000000000 3C000000 0000 1600 2200 00000000 00000000 2200 2E00 FFFFFFFF 00000000 2E00 3A00 FFFFFFFF 00000000 3A00 4600 FFFFFFFF 00000000 4600 0000 FFFFFFFF 00000000
            //WriteWord(writer, (short)Player.Effects.Count); //effects counter?
            //WriteWord(writer, 0); //first abnormal shift
            //WriteLong(writer, 1);//???
            //WriteDword(writer, 60); //???
            //WriteWord(writer, 0);
            ////WriteDword(writer, Skill.Id);
            //WriteByte(writer, "FFFFFF7F"); //???
            //WriteByte(writer, 0x01);
        }
    }
}