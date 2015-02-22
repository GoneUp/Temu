using System.IO;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpNpcEmotion : ASendPacket //len 32
    {
        protected Creature Creature;
        protected Creature Target;
        protected int Emotion;

        public SpNpcEmotion(Creature creature, Creature target, int emotion)
        {
            Creature = creature;
            Target = target;
            Emotion = emotion;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Creature);
            WriteUid(writer, Target);

            WriteDword(writer, 0);
            WriteDword(writer, Emotion);
            WriteDword(writer, 0);
        }
    }
}