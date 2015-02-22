using System.IO;
using Tera.Data.Enums;

namespace Tera.Network.old_Server
{
    public class SpSystemWindow : ASendPacket
    {
        protected SystemWindow Window;

        public SpSystemWindow(SystemWindow window)
        {
            Window = window;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, Window.GetHashCode());
        }
    }
}