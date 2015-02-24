namespace Tera.Network.old_Client
{
    public class RpQuestRefuse : ARecvPacket
    {
        protected int QuestId;

        public override void Read()
        {
            ReadDword(); // Unk
            ReadDword(); // 8
            QuestId = ReadDword();
        }

        public override void Process()
        {
            lock (Connection.Player.QuestsLock)
            {
                if(!Connection.Player.Quests.ContainsKey(QuestId))
                    return;

                Connection.Player.Quests.Remove(QuestId);
            }
        }
    }
}
