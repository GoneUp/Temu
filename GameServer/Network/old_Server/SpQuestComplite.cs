using System.IO;

namespace Tera.Network.old_Server
{
    public class SpQuestComplite : ASendPacket
    {
        protected int QuestId;

        public SpQuestComplite(int questId)
        {
            QuestId = questId;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, QuestId);
            WriteDword(writer, QuestId); //QuestUId???
            WriteWord(writer, 0);
        }
    }
}