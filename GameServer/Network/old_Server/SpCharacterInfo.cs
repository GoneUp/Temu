using System.IO;
using Tera.Data.Enums.Player;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterInfo : ASendPacket
    {
        protected Player Player;
        protected PlayerRelation Relation;

        public SpCharacterInfo(Player player, PlayerRelation relation)
        {
            Player = player;
            Relation = relation;
        }

        public override void Write(BinaryWriter writer)
        {

            WriteWord(writer, 0); //Name shift
            WriteWord(writer, 0); //Legion shift
            WriteWord(writer, 0); //Unk1 shift
            WriteWord(writer, 0); //Details shift
            WriteWord(writer, (short)Player.PlayerData.Details.Length); //2000
            WriteWord(writer, 0); //Unk2 shift
            WriteWord(writer, 0); //Unk3 shift

            WriteDword(writer, 11);
            WriteDword(writer, Player.PlayerId);

            WriteUid(writer, Player);

            WriteSingle(writer, Player.Position.X);
            WriteSingle(writer, Player.Position.Y);
            WriteSingle(writer, Player.Position.Z);
            WriteWord(writer, Player.Position.Heading);

            WriteDword(writer, Relation.GetHashCode());
            WriteDword(writer, Player.PlayerData.SexRaceClass);

            WriteByte(writer, "00004600AA00000000000101"); //???

            WriteByte(writer, Player.PlayerData.Data);

            WriteDword(writer, Player.Inventory.GetItemId(1) ?? 0); //Item (hands)
            WriteDword(writer, Player.Inventory.GetItemId(3) ?? 0); //Item (body)
            WriteDword(writer, Player.Inventory.GetItemId(4) ?? 0); //Item (gloves)
            WriteDword(writer, Player.Inventory.GetItemId(5) ?? 0); //Item (shoes)

            WriteByte(writer, "000000000000000001000000000000000600000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            WriteDword(writer, Player.GetLevel());
            WriteByte(writer, "000000000000000001000000000000000000000000");

            writer.Seek(4, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length); //Name shift
            writer.Seek(0, SeekOrigin.End);
            WriteString(writer, Player.PlayerData.Name); //Name

            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length); //Legion shift
            writer.Seek(0, SeekOrigin.End);
            WriteString(writer, Player.Guild != null ? Player.Guild.GuildName : "");

            writer.Seek(8, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length); //Unk1 shift
            writer.Seek(0, SeekOrigin.End);
            WriteString(writer, ""); //Unk1

            writer.Seek(10, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length); //Details shift
            writer.Seek(0, SeekOrigin.End);
            WriteByte(writer, Player.PlayerData.Details);

            writer.Seek(12, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length); //Unk2 shift
            writer.Seek(0, SeekOrigin.End);
            WriteString(writer, ""); //Unk2

            writer.Seek(14, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length); //Unk3 shift
            writer.Seek(0, SeekOrigin.End);
            WriteString(writer, Player.Guild == null ? "" : Player.Guild.GuildTitle); //on top of nick, mb status?

            //return;

            //1CDE BD00 D300 D500 D700 2000 F700 F900    0B00 0000    292A0000     460B0B0000800000 34119C47 3099A0C7 00108CC5 541D 01000000 F92A0000 00004600BE00000000000101 650E000200190400 112700009C3A00009D3A00009E3A0000 00000000000000000100000000000000060000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004000000000000000000000001000000000000000000000000 4D006100720079006400610069006C006C0065000000 00000000 09130810000000001E1716001001150010000C0D0000000F1517101517100C09 00000000
            WriteWord(writer, 0); //Name shift
            WriteWord(writer, 0); //Legion shift
            WriteWord(writer, 0); //Unk1 shift
            WriteWord(writer, 0); //Details shift
            WriteWord(writer, (short) Player.PlayerData.Details.Length); //2000
            WriteWord(writer, 0); //Unk2 shift
            WriteWord(writer, 0); //Unk3 shift

            WriteDword(writer, 11);
            WriteDword(writer, Player.PlayerId);

            WriteUid(writer, Player);
            WriteSingle(writer, Player.Position.X);
            WriteSingle(writer, Player.Position.Y);
            WriteSingle(writer, Player.Position.Z);
            WriteWord(writer, Player.Position.Heading); //ED60
            WriteDword(writer, Relation.GetHashCode());
            WriteDword(writer, Player.PlayerData.SexRaceClass);

            WriteByte(writer, "00004600AA00000000000101"); //???

            WriteByte(writer, Player.PlayerData.Data);
            //WriteByte(writer, RandomUtilities.HexToBytes("651D040301000500"));

            WriteDword(writer, Player.Inventory.GetItemId(1) ?? 0); //Item (hands)
            WriteDword(writer, Player.Inventory.GetItemId(3) ?? 0); //Item (body)
            WriteDword(writer, Player.Inventory.GetItemId(4) ?? 0); //Item (gloves)
            WriteDword(writer, Player.Inventory.GetItemId(5) ?? 0); //Item (shoes)

            WriteByte(writer, "00000000000000000100000000000000060000000000000000000000000000000000000000000000000000000000");

            WriteDword(writer, Player.Inventory.GetItem(1) != null ? Player.Inventory.GetItem(1).Color : 0);
            WriteDword(writer, Player.Inventory.GetItem(3) != null ? Player.Inventory.GetItem(3).Color : 0);
            WriteDword(writer, Player.Inventory.GetItem(4) != null ? Player.Inventory.GetItem(4).Color : 0);
            WriteDword(writer, Player.Inventory.GetItem(5) != null ? Player.Inventory.GetItem(5).Color : 0);

            WriteByte(writer, "00000000000004000000000000000000000001000000000000000000000000");

            writer.Seek(4, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length); //Name shift
            writer.Seek(0, SeekOrigin.End);
            WriteString(writer, Player.PlayerData.Name); //Name

            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length); //Legion shift
            writer.Seek(0, SeekOrigin.End);
            WriteString(writer, ""); //Legion name //TODO:

            writer.Seek(8, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length); //Unk1 shift
            writer.Seek(0, SeekOrigin.End);
            WriteString(writer, ""); //Unk1

            writer.Seek(10, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length); //Details shift
            writer.Seek(0, SeekOrigin.End);
            WriteByte(writer, Player.PlayerData.Details);

            writer.Seek(12, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length); //Unk2 shift
            writer.Seek(0, SeekOrigin.End);
            WriteString(writer, ""); //Unk2

            writer.Seek(14, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length); //Unk3 shift
            writer.Seek(0, SeekOrigin.End);
            WriteString(writer, ""); //Unk3
        }
    }
}