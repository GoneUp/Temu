using System.IO;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpRemoveAbnormal : ASendPacket
    {
        protected Abnormal Abnormal;

        public SpRemoveAbnormal(Abnormal abnormal)
        {
            Abnormal = abnormal;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Abnormal.Creature);
            WriteDword(writer, Abnormal.Abnormality.Id);
        }
    }
}