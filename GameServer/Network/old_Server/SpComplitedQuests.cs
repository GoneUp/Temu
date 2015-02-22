using System.Collections.Generic;
using System.IO;

namespace Tera.Network.old_Server
{
    public class SpComplitedQuests : ASendPacket
    {
        protected List<int> QuestsIds;

        public SpComplitedQuests(List<int> questsIds)
        {
            QuestsIds = questsIds;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) QuestsIds.Count);
            int shift = (int) writer.BaseStream.Position;
            WriteWord(writer, 0);

            for (int i = 0; i < QuestsIds.Count; i++)
            {
                writer.Seek(shift, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteWord(writer, (short) writer.BaseStream.Length);
                shift = (int) writer.BaseStream.Position;
                WriteWord(writer, 0);

                WriteDword(writer, QuestsIds[i]);
            }
        }
    }
}