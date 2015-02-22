using System.IO;

namespace Tera.Network.old_Server
{
    public class SpUISettings : ASendPacket
    {
        protected byte[] UISettings;

        public SpUISettings(byte[] settings)
        {
            UISettings = settings;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteByte(writer, UISettings);
        }
    }
}
