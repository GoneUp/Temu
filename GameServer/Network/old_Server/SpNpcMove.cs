using System.IO;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpNpcMove : ASendPacket //len 44
    {
        protected Creature Creature;
        protected short Speed;
        protected float X2;
        protected float Y2;
        protected float Z2;

        public SpNpcMove(Creature creature, short speed, float x2, float y2, float z2)
        {
            Creature = creature;
            Speed = speed;
            X2 = x2;
            Y2 = y2;
            Z2 = z2;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Creature);
            WriteSingle(writer, Creature.Position.X);
            WriteSingle(writer, Creature.Position.Y);
            WriteSingle(writer, Creature.Position.Z);
            WriteWord(writer, Creature.Position.Heading);
            WriteWord(writer, Speed);
            WriteSingle(writer, X2);
            WriteSingle(writer, Y2);
            WriteSingle(writer, Z2);
            WriteDword(writer, 0);
        }
    }
}