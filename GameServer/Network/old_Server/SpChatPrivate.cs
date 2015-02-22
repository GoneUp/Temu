using System.IO;

namespace Tera.Network.old_Server
{
    public class SpChatPrivate : ASendPacket //send only 4 self
    {
        protected string Sender;
        protected string Sended;
        protected string Message;

        public SpChatPrivate(string sender, string targetName, string message)
        {
            Sender = sender;
            Sended = targetName;
            Message = message;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 0); //first name shift
            WriteWord(writer, 0); //second name shift
            WriteWord(writer, 0); //message shift
            WriteByte(writer, 0);

            writer.Seek(4, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, Sender);

            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, Sended);

            writer.Seek(8, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, Message);
        }
    }
}