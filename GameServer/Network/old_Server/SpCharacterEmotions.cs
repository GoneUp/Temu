using System.IO;
using Tera.Data.Enums;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpCharacterEmotions : ASendPacket
    {
        protected Creature Creature;
        protected int EmotionId;
        protected int Duration;

        public SpCharacterEmotions(Creature creature, int emotionId, int duration = 0)
        {
            Creature = creature;
            EmotionId = emotionId;
            Duration = duration;
        }

        public SpCharacterEmotions(Creature creature, PlayerEmotion emotion, int duration = 0)
        {
            Creature = creature;
            EmotionId = emotion.GetHashCode();
            Duration = duration;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Creature);
            WriteDword(writer, EmotionId);
            WriteDword(writer, Duration);
            WriteByte(writer, 0); //unk
        }
    }
}