using System.IO;

namespace Tera.Network.old_Server
{
    public class SpGatherProgress : ASendPacket
    {
        protected int Progress;

        public SpGatherProgress(int progress)
        {
            Progress = progress;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, Progress);
        }
    }
}