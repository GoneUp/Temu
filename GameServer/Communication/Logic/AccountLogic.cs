using System.Linq;
using Tera.Data;
using Tera.Data.Interfaces;
using Tera.Data.Structures.Player;
using Utils;

namespace Tera.Communication.Logic
{
    public class AccountLogic : Global
    {
        public const int LogoutTimeout = 1; //TODO: 10

        public static void TryAuthorize(IConnection connection, string accountName, string session)
        {
            //TODO: Session check
            AccountService.Authorized(connection, accountName);
            FeedbackService.OnAuthorized(connection);
        }

        public static void GetPlayerList(IConnection connection)
        {
            FeedbackService.SendPlayerList(connection);
        }

        public static void ClientDisconnected(IConnection connection)
        {
            if (connection.GameAccount != null && connection.Player != null)
            {
                Player player = connection.Player;
                new DelayedAction(() => PlayerLogic.PlayerEndGame(player), LogoutTimeout*1000);
            }
        }

        public static void RelogPlayer(IConnection connection)
        {
            AccountService.AbortExitAction(connection);
            FeedbackService.ShowRelogWindow(connection, LogoutTimeout);

            connection.GameAccount.ExitAction = new DelayedAction(
                () =>
                    {
                        FeedbackService.Relog(connection);
                        PlayerLogic.PlayerEndGame(connection.Player);
                    }, LogoutTimeout * 1000);
        }
        //ToDo
        public static void RemovePlayer(IConnection connection, int playerId)
        {
            Player p = connection.GameAccount.Players.FirstOrDefault(player => player.PlayerId == playerId);

            if (p == null)
            { return; }

            if (Cache.UsedNames.Contains(p.PlayerData.Name.ToLower()))
                Cache.UsedNames.Remove(p.PlayerData.Name.ToLower());

            PartyService.LeaveParty(p);
            GuildService.LeaveGuild(p, p.Guild);
            connection.GameAccount.Players.Remove(connection.GameAccount.Players.FirstOrDefault(player => player.PlayerId == playerId));
            FeedbackService.SendCharRemove(connection);
        }

        public static void ExitPlayer(IConnection connection)
        {
            AccountService.AbortExitAction(connection);
            FeedbackService.ShowExitWindow(connection, LogoutTimeout);

            connection.GameAccount.ExitAction = new DelayedAction(
                () =>
                {
                    FeedbackService.Exit(connection);
                    PlayerLogic.PlayerEndGame(connection.Player);
                }, LogoutTimeout * 1000);
        }

        public static void AbortExitAction(IConnection connection)
        {
            if (connection.GameAccount.ExitAction != null)
                connection.GameAccount.ExitAction.Abort();
        }
    }
}