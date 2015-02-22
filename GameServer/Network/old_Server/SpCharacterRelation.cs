using System.IO;
using Tera.Data.Enums.Player;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpCharacterRelation : ASendPacket
    {
        protected Creature Creature;
        protected PlayerRelation Relation;

        public SpCharacterRelation(Creature creature, PlayerRelation relation)
        {
            Creature = creature;
            Relation = relation;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Creature);
            WriteDword(writer, Relation.GetHashCode());
        }
    }
}