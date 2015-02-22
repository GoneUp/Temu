using System.Collections.Generic;
using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpFriendUpdate : ASendPacket
    {
        protected List<Player> Friends = new List<Player>();

        public SpFriendUpdate(List<Player> friends)
        {
            for (int i = 0; i < friends.Count; i++)
            {
                if (Communication.Global.PlayerService.IsPlayerOnline(friends[i]) && friends[i].Connection != null)
                    Friends.Add(friends[i]);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) Friends.Count);

            var shift = (int) writer.BaseStream.Position;
            WriteWord(writer, 0);

            for (int i = 0; i < Friends.Count; i++)
            {
                writer.Seek(shift, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteWord(writer, (short) writer.BaseStream.Position);
                shift = (int) writer.BaseStream.Position;
                WriteWord(writer, 0); //next friend shift
                WriteDword(writer, Friends[i].PlayerId);
                WriteDword(writer, Friends[i].GetLevel());
                WriteByte(writer, "00000000010000000200000007000000");
                WriteWord(writer, 0);
            }
        }
    }
}