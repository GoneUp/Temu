using Tera.Data.Enums;
using Tera.Data.Interfaces;

namespace Tera.Communication.Interfaces
{
    public interface IChatService : IComponent
    {
        void ProcessMessage(IConnection connection, string message, ChatType type);
        void ProcessPrivateMessage(IConnection connection, string playerName, string message);
        void SendChatInfo(IConnection connection, int type, string name);
    }
}