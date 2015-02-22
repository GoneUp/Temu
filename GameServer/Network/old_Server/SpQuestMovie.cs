using System.IO;

namespace Tera.Network.old_Server
{
    public class SpQuestMovie : ASendPacket
    {
        protected int MovieId;

        public SpQuestMovie(int movieId)
        {
            MovieId = movieId;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, MovieId);
        }
    }
}