using System;
using Data.Interfaces;
using Network.Server;
using Utils;
using Utils.Logger;

namespace Tera.AdminEngine.AdminCommands
{
    class Speed : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                connection.Player.MovementByAdminCommand = short.Parse(msg);
                new SpCharacterStats(connection.Player).Send(connection);
            }
            catch(Exception ex)
            {
                Logger.WriteLine(LogState.Exception,"AdminEngine: Speed: " + ex);
            }
        }
    }
}
