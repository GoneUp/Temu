using System.Collections.Generic;
using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpFriendList : ASendPacket
    {
        public List<Player> Friends;

        public SpFriendList(List<Player> friends)
        {
            Friends = friends;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) Friends.Count);

            if (Friends.Count > 0)
            {
                WriteWord(writer, 8);
                for (int i = 0; i < Friends.Count; i++)
                {
                    WriteWord(writer, (short) writer.BaseStream.Position);
                    int shift = (int) writer.BaseStream.Position;
                    WriteWord(writer, 0); //end friend shift
                    WriteWord(writer, 0); //start name shift
                    WriteWord(writer, 0); //end name shift
                    WriteDword(writer, Friends[i].PlayerId);
                    WriteDword(writer, Friends[i].GetLevel());
                    WriteDword(writer, Friends[i].PlayerData.Race.GetHashCode());
                    WriteDword(writer, Friends[i].PlayerData.Class.GetHashCode());
                    WriteByte(writer, "01000000010000000200000007000000");

                    writer.Seek(shift + 2, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);

                    WriteString(writer, Friends[i].PlayerData.Name);

                    writer.Seek(shift + 4, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);

                    WriteWord(writer, 0);

                    if (Friends.Count - 1 > i)
                    {
                        writer.Seek(shift, SeekOrigin.Begin);
                        WriteWord(writer, (short) writer.BaseStream.Length);
                        writer.Seek(0, SeekOrigin.End);
                    }
                }
            }
        }
    }
}