using System.Collections.Generic;
using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpPartyList : ASendPacket
        //1FAD 0200 3000 3000 0000000B0000000C00B503B5030000 901E0C00 01000000030000000000010000000100000000 3000 6300 5300 B5030000 901E0C00 01000000 04000000 01 7E00B50300800000 00000000 64006D00680061006C00790061000000        6300 0000 8600 B5030000 F84B0A00 01000000 00000000 01 E900B50300800000 00000000 460069007200650046006F00780079000000
        //9CBD 0200 3000 3000 0000006416000007000B000B000000 FD8D0000 01000000030000000000010000000100000000 3000 6500 5300 0B000000 FD8D0000 04000000 06000000 014C6C0B0000800001000000004D00650074006100770069006E00640000006500000088000B000000A72500000C00000005000000016C6E0B00008000010000000045006C0061000000
    {
        protected List<Player> PartyPlayers;

        public SpPartyList(List<Player> partyPlayers)
        {
            PartyPlayers = partyPlayers;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) PartyPlayers.Count); //counter of party members
            WriteWord(writer, 0); //first name begin shift
            WriteWord(writer, 0); //first name begin shift
            WriteByte(writer, "0000006416000007000B000B000000");
            WriteDword(writer, PartyPlayers[0].PlayerId);
            WriteByte(writer, "01000000030000000000010000000100000000");


            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            writer.Seek(8, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            for (int i = 0; i < PartyPlayers.Count; i++)
            {
                WriteWord(writer, (short) writer.BaseStream.Length);
                short nextPlShift = (short) writer.BaseStream.Length;
                WriteWord(writer, 0); // next player begin shift
                WriteWord(writer, 0); // name begin shift
                WriteDword(writer, 11);// -_-
                WriteDword(writer, PartyPlayers[i].PlayerId);
                WriteDword(writer, PartyPlayers[i].GetLevel());
                WriteDword(writer, PartyPlayers[i].PlayerData.Class.GetHashCode());
                WriteByte(writer, (byte)Communication.Global.PlayerService.IsPlayerOnline(PartyPlayers[i]).GetHashCode());
                WriteUid(writer, PartyPlayers[i]);
                WriteDword(writer, 0);

                writer.Seek(nextPlShift + 2, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteString(writer, PartyPlayers[i].PlayerData.Name);

                if (PartyPlayers.Count > i + 1)
                {
                    writer.Seek(nextPlShift, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);
                }
            }
        }
    }
}