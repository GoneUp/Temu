namespace Tera.Network.old_Client
{
    public class RpCharacterEmotion : ARecvPacket
    {
        protected int EmotionId;

        public override void Read()
        {
            EmotionId = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.EmotionService.StartEmotion(Connection.Player, EmotionId);
        }
    }
}