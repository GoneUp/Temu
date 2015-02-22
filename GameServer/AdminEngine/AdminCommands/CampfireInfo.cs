using System;
using Tera.Data.Enums;
using Tera.Data.Interfaces;
using Tera.Data.Structures.World;
using Tera.Network.old_Server;
using Utils;
using Utils.Logger;

namespace Tera.AdminEngine.AdminCommands
{
    class CampfireInfo : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                Campfire campfire = null;
                double distance = double.MaxValue;

                foreach (var camp in connection.Player.VisibleCampfires)
                {
                    double dist = camp.Position.DistanceTo(connection.Player.Position);
                    if (dist < distance)
                    {
                        campfire = camp;
                        distance = dist;
                    }
                }

                if (campfire == null)
                {
                    new SpChatMessage("Campfire in visible not found!", ChatType.System).Send(connection);
                    return;
                }

                new SpChatMessage("Unk1: " + campfire.Type, ChatType.System).Send(connection);
                new SpChatMessage("Unk2: " + campfire.Status, ChatType.System).Send(connection);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Exception,"AdminEngine: CampfireInfo: " + ex);
            }
        }
    }
}
