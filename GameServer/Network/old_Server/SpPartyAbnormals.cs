using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpPartyAbnormals : ASendPacket
    {
        protected Player Player;

        //public SpPartyAbnormals(Player player) // WRONG
        //{
        //    Player = player;
        //}

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, 0);
            WriteWord(writer, (short) Player.Effects.Count);
            WriteWord(writer, 0); //first effect shift
            WriteDword(writer, 11); //hello AGAIN, fucking 0x0b shit...how are you? -_-
            WriteDword(writer, Player.PlayerId);

            writer.Seek(10, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            for (int i = 0; i < Player.Effects.Count; i++)
            {
                short shPos = (short) writer.BaseStream.Position;

                WriteWord(writer, shPos);
                WriteWord(writer, 0); //next abnormal
                WriteDword(writer, 0);
                WriteDword(writer, 0);
                WriteByte(writer, 1); //possible IsBuff

                if (Player.Effects.Count > i + 1)
                {
                    writer.Seek(shPos + 2, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);
                }
            }
        }
    }
}