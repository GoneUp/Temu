using System.IO;
using Tera.Data.Structures.Account;
using Tera.Data.Structures.Player;

namespace Tera.Network.Packets.ServerPackets
{
    public class SpSendCharacterList : ASendPacket
    {
        protected GameAccount GameAccount;

        public SpSendCharacterList(GameAccount gameAccount)
        {
            GameAccount = gameAccount;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteByte(writer, "02002700010000000000000000090000000100000000002800000000000000180000002700C7010000000057016701200087014000D02B000001000000000000000700000041000000B8710100A21800000100000001000000020000006395EF5400000000008F5F01000000000076020EAB137501003A7501003A7501002D7501002E7501002F75010000000000397501003975010055BB020092C300000000000065040505010E03000000000000000000000000000000E7A20CAB000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000397202000000000000000000000000006807BD046807BD0400000000000001000000006D100000010000004B00610074006C0079006E006E000000130912071307000F170C0D0012000E000B0F0F0A140E0C1414141D101511110800000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000C701000000000000F7020903200029034000ACB4130001000000040000000800000032000000A8720000840700000F2700003D000000AF1B0000A869E65400000000008F5F01000000000076020EAB0C3A010000000000000000000D3A01000E3A01000F3A0100000000000000000000000000000000000000000000000000651B0100071E04000000000000000000000000000000E7A20CAB000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000084EF3B0001000000000001640000008A020000000000004B00610072006F006C0079006E006E0000000006080C000000001A1017000C15050010000C0D0000000F1517100C19100E090113101313101313130F0F0F0F0F0F0F10130A00050B100000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            return;

            WriteWord(writer, (short) GameAccount.Players.Count); //Characters count
            WriteWord(writer, (short) (GameAccount.Players.Count == 0 ? 0 : 27));
            WriteByte(writer, new byte[9]);
            WriteDword(writer, 8); //Max character count
            WriteDword(writer, 1);
            WriteWord(writer, 0);

            for (int i = 0; i < GameAccount.Players.Count; i++)
            {
                Player player = GameAccount.Players[i];
                while ((player.PlayerLevel + 1) != Data.Data.PlayerExperience.Count - 1
                && player.PlayerExp >= Data.Data.PlayerExperience[player.PlayerLevel])
                    player.PlayerLevel++;
           

                short check1 = (short) writer.BaseStream.Position;
                WriteWord(writer, check1); //Check1
                WriteWord(writer, 0); //Check2
                WriteWord(writer, 0); //Name shift
                WriteWord(writer, 0); //Details shift
                WriteWord(writer, (short) player.PlayerData.Details.Length); //Details length

                WriteDword(writer, player.PlayerId); //PlayerId
                WriteDword(writer, player.PlayerData.Gender.GetHashCode()); //Gender
                WriteDword(writer, player.PlayerData.Race.GetHashCode()); //Race
                WriteDword(writer, player.PlayerData.Class.GetHashCode()); //Class
                WriteDword(writer, player.GetLevel()); //Level

                WriteByte(writer, "260B000087050000"); //A0860100A0860100
                WriteByte(writer, player.ZoneDatas ?? new byte[12]);
                WriteDword(writer, player.LastOnlineUtc);
                WriteByte(writer, "00000000008F480900000000006AD376B0"); //Unk
                //WriteByte(writer, "000000A0860100A0860100000000000000 00000000 00008F7E 00000000 0000000F B9090000 00000001 91DDB1"); //New character, play start video
                WriteDword(writer, player.Inventory.GetItemId(1) ?? 0); //Item (hands)
                WriteDword(writer, 0); //Item (earing1?)
                WriteDword(writer, 0); //Item (earing2?)
                WriteDword(writer, player.Inventory.GetItemId(3) ?? 0); //Item (body)
                WriteDword(writer, player.Inventory.GetItemId(4) ?? 0); //Item (gloves)
                WriteDword(writer, player.Inventory.GetItemId(5) ?? 0); //Item (boots)
                WriteDword(writer, 0); //Item (ring1)
                WriteDword(writer, 0); //Item (ring2)
                WriteDword(writer, 0); //Item ?
                WriteDword(writer, 0); //Item ?
                WriteDword(writer, 0); //Item ?

                WriteByte(writer, player.PlayerData.Data);
                WriteByte(writer, 0); //Offline?
                WriteByte(writer, "0000000000000000000000000089E66EB0"); //???
                WriteByte(writer, new byte[48]);

                WriteDword(writer, player.Inventory.GetItem(1) != null ? player.Inventory.GetItem(1).Color : 0);
                WriteDword(writer, player.Inventory.GetItem(3) != null ? player.Inventory.GetItem(3).Color : 0);
                WriteDword(writer, player.Inventory.GetItem(4) != null ? player.Inventory.GetItem(4).Color : 0);
                WriteDword(writer, player.Inventory.GetItem(5) != null ? player.Inventory.GetItem(5).Color : 0);

                WriteByte(writer, new byte[28]); //16 bytes possible colors

                WriteDword(writer, 0); //Rested (current)
                WriteDword(writer, 10000); //Rested (max)
                                
                WriteByte(writer, 1);
                WriteByte(writer, (byte) (player.PlayerExp == 0 ? 1 : 0)); //Intro video flag

                WriteDword(writer, 0); //Now start only in Island of Dawn
                //WriteDword(writer, player.Exp == 0 ? 1 : 0); //Prolog or IslandOfDawn dialog window

                writer.Seek(check1 + 4, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length); //Name shift
                writer.Seek(0, SeekOrigin.End);

                WriteString(writer, player.PlayerData.Name);

                writer.Seek(check1 + 6, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length); //Details shift
                writer.Seek(0, SeekOrigin.End);

                WriteByte(writer, player.PlayerData.Details);

                if (i != GameAccount.Players.Count - 1)
                {
                    writer.Seek(check1 + 2, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length); //Check2
                    writer.Seek(0, SeekOrigin.End);
                }
            }
        }
    }
}