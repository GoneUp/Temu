using Tera.Communication.Interfaces;
using Tera.Data.Interfaces;
using Tera.Database.DAO;
using Utils;

namespace Tera.Services
{
    class AccountService : IAccountService
    {
        public void Authorized(IConnection connection, string accountName)
        {

            connection.GameAccount = DAOManager.accountDAO.LoadAccount(accountName);
            connection.GameAccount.AccountWarehouse = DAOManager.inventoryDAO.LoadAccountStorage(connection.GameAccount);
            connection.GameAccount.Players = Communication.Global.PlayerService.OnAuthorized(connection.GameAccount);
        }

        public void AbortExitAction(IConnection connection)
        {
            if (connection.GameAccount.ExitAction != null)
            {
                connection.GameAccount.ExitAction.Abort();
                connection.GameAccount.ExitAction = null;
            }
        }

        public void Action()
        {
            
        }
    }
}
