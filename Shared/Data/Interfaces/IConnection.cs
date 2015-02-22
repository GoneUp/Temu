using Tera.Data.Structures.Account;
using Tera.Data.Structures.Player;

namespace Tera.Data.Interfaces
{
    public interface IConnection
    {
        GameAccount GameAccount { get; set; }
        Player Player { get; set; }
        bool IsValid { get; }

        void Close();
        void PushPacket(byte[] data);
        long Ping();
    }
}
