using System.IO;
using Tera.Data.Structures.Npc;

namespace Tera.Network.old_Server
{
    public class SpNpcInfo : ASendPacket
    {
        protected Npc Npc;
        protected string TestNpcHex;

        public SpNpcInfo(Npc npc)
        {
            Npc = npc;
        }

        public SpNpcInfo(string hex)
        {
            TestNpcHex = hex;
        }

        public override void Write(BinaryWriter writer)
        {
            if (TestNpcHex != null)
            {
                WriteByte(writer, TestNpcHex);
                return;
            }

            WriteDword(writer, 0); //???
            WriteWord(writer, 0); //Shit shift

            WriteUid(writer, Npc);
            WriteUid(writer, Npc.Target);
            WriteSingle(writer, Npc.Position.X);
            WriteSingle(writer, Npc.Position.Y);
            WriteSingle(writer, Npc.Position.Z);
            WriteWord(writer, Npc.Position.Heading);
            WriteDword(writer, 12); //???
            WriteDword(writer, Npc.SpawnTemplate.NpcId);
            WriteWord(writer, Npc.SpawnTemplate.Type);
            WriteDword(writer, 0); //ModelId
            
            WriteByte(writer, "000000000500000001010100000000000000000000000000000000000000");

            WriteUid(writer, Npc.Target); //If 1 agressiv

            WriteByte(writer, "000000000000000000");

            writer.Seek(8, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length); //Shit shift
            writer.Seek(0, SeekOrigin.End);

            WriteByte(writer, "45C5C8B958C72000E4C2D8D5B4CC0000"); //Shit
        }
    }
}