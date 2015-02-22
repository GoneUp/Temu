using Tera.Data.Interfaces;

namespace Tera.Communication.Interfaces
{
    public interface IAdminEngine : IComponent
    {
        bool ProcessChatMessage(IConnection connection, string message);
    }
}