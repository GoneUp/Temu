﻿using System;
using System.Collections.Generic;
using Tera.Communication;
using Tera.Data.Enums;
using Tera.Data.Interfaces;
using Tera.Data.Structures.Player;
using Tera.Network.old_Server;
using Utils;
using Utils.Logger;

namespace Tera.AdminEngine.AdminCommands
{
    class AddItem : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            try
            {
                if (msg.Split(' ')[0].Equals("money"))
                {
                    Global.StorageService.AddMoneys(connection.Player, connection.Player.Inventory,
                                                                  int.Parse(msg.Split(' ')[1]));
                    Global.GuildService.AddNewGuild(new List<Player>{connection.Player, connection.Player.Party.PartyMembers[1]}, "OnTera");
                    return;
                }

                Global.StorageService.AddItem(connection.Player, connection.Player.Inventory,
                                              new StorageItem
                                                  {
                                                      ItemId = Convert.ToInt32(msg.Split(' ')[0]),
                                                      Amount = (msg.Split(' ').Length < 2
                                                                   ? 1
                                                                   : Convert.ToInt32(msg.Split(' ')[1]))
                                                  });
            }
            catch(Exception e)
            {
                new SpChatMessage("Wrong syntax!\nType: !additem {item_id} {counter}", ChatType.Notice).Send(connection);
                Logger.WriteLine(LogState.Warn,e.ToString());
            }
        }
    }
}
