using System.IO;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpDirectionChange : ASendPacket
    {
        protected Creature Creature;
        protected short NewHeading;
        protected short Time;

        public SpDirectionChange(Creature creature, short newheading, short time = (short) 0)
        {
            Creature = creature;
            NewHeading = newheading;
            Time = time;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Creature);
            WriteWord(writer, NewHeading);
            WriteWord(writer, Time);
            WriteWord(writer, 0); //unk
        }
    }
}