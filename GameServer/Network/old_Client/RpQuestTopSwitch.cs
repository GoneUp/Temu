namespace Tera.Network.old_Client
{
    public class RpQuestTopSwitch : ARecvPacket
    {
        protected int QuestId;
        protected byte VisiblitySwitch;
        protected int Unk1; // 0x01

        public override void Read()
        {
            QuestId = ReadDword();
            VisiblitySwitch = (byte) ReadByte();
            Unk1 = ReadDword();
        }

        public override void Process()
        {
            
        }
    }
}