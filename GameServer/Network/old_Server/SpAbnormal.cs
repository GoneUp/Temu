using System.IO;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpAbnormal : ASendPacket
    {
        protected Abnormal Abnormal;

        public SpAbnormal(Abnormal abnormal)
        {
            Abnormal = abnormal;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Abnormal.Creature);
            WriteUid(writer, Abnormal.Caster);

            WriteDword(writer, Abnormal.Abnormality.Id);
            WriteDword(writer, Abnormal.Abnormality.Time);
            WriteDword(writer, 1); //Type?
        }
    }
}