using System;
using System.Collections.Generic;
using System.IO;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpTradeList : ASendPacket
    {
        protected Tradelist Tradelist;

        public SpTradeList(Tradelist tradelist)
        {
            Tradelist = tradelist;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) Tradelist.ItemsByTabs.Count);
            WriteByte(writer, "2000B008B8030080000E"); //2000 mb shift?
            WriteDword(writer, (int) DateTime.Now.Ticks);
            WriteWord(writer, 0x3EC0); // ???
            WriteWord(writer, 0);

            WriteByte(writer, "7B14AE47E17A843F");

            foreach (KeyValuePair<int, List<int>> tab in Tradelist.ItemsByTabs)
            {
                WriteWord(writer, (short) writer.BaseStream.Length);
                WriteWord(writer, 0); //next tab shift

                WriteWord(writer, (short) tab.Value.Count);
                WriteWord(writer, (short) (writer.BaseStream.Length + 6)); //first item shift
                WriteDword(writer, tab.Key); // name Id

                writer.Seek((int) writer.BaseStream.Length - 10, SeekOrigin.Begin);
                WriteWord(writer, (short) (writer.BaseStream.Length + tab.Value.Count*8));
                writer.Seek(0, SeekOrigin.End);

                for (int i = 0; i < tab.Value.Count; i++)
                {
                    short nowItemShift = (short) writer.BaseStream.Length;
                    WriteWord(writer, nowItemShift); //now item shift
                    WriteWord(writer, (short) (i == tab.Value.Count - 1 ? 0 : nowItemShift + 8)); //next item shift
                    WriteDword(writer, tab.Value[i]);
                }
            }
        }
    }
}