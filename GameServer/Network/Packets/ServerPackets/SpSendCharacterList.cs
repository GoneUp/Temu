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