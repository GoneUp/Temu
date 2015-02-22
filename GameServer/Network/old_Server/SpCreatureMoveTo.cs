using System.IO;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpCreatureMoveTo : ASendPacket
    {
        protected Creature Creature;
        protected Creature Target;

        protected WorldPosition Position;

        public SpCreatureMoveTo(Creature creature, Creature target, WorldPosition position)
        {
            Creature = creature;
            Target = target;
            Position = position;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Creature);
            WriteUid(writer, Target);
            WriteSingle(writer, Position.X);
            WriteSingle(writer, Position.Y);
            WriteSingle(writer, Position.Z);
            WriteWord(writer, Position.Heading);
        }
    }
}