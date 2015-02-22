﻿using Tera.Communication.Interfaces;
using Tera.Data.DAO;
using Tera.Data.Structures.Player;
using Tera.Network.old_Server;

namespace Tera.Services
{
    class MountService : IMountService
    {
        public void Action() { }

        public void UnkQuestion(Player player, int unk)
        {
            Communication.Global.VisibleService.Send(player, new SpMountUnkResponse(player, unk));
        }

        public void ProcessMount(Player player, int skillId)
        {
            if (!Data.Data.Mounts.ContainsKey(skillId))
                return;

            if (Data.Data.Mounts.ContainsKey(skillId) && player.PlayerMount == skillId)
            {
                Communication.Global.VisibleService.Send(player, new SpMountHide(player, player.PlayerMount));
                player.PlayerMount = 0;
                Communication.Logic.CreatureLogic.UpdateCreatureStats(player);
                DAOManager.playerDAO.UpdatePlayer(player);
            }
            else
            {
                if (Data.Data.Mounts.ContainsKey(skillId) && player.PlayerMount == 0)
                {
                    Communication.Global.VisibleService.Send(player, new SpMountShow(player, Data.Data.Mounts[skillId].MountId, skillId));
                    player.PlayerMount = skillId;
                    Communication.Logic.CreatureLogic.UpdateCreatureStats(player);
                    DAOManager.playerDAO.UpdatePlayer(player);
                }
                else
                {
                    if (Data.Data.Mounts.ContainsKey(skillId) && player.PlayerMount != 0)
                    {
                        Communication.Global.VisibleService.Send(player, new SpMountHide(player, player.PlayerMount));
                        player.PlayerMount = 0;
                        Communication.Logic.CreatureLogic.UpdateCreatureStats(player);
                        DAOManager.playerDAO.UpdatePlayer(player);

                        Communication.Global.VisibleService.Send(player, new SpMountShow(player, Data.Data.Mounts[skillId].MountId, skillId));
                        player.PlayerMount = skillId;
                        Communication.Logic.CreatureLogic.UpdateCreatureStats(player);
                        DAOManager.playerDAO.UpdatePlayer(player);
                    }
                }
            }

            /*
            //On Mount sit down
             * 
            if (player.PlayerMount == skillId)
            {
                Communication.Global.VisibleService.Send(player, new SpMountHide(player, player.PlayerMount));
                player.PlayerMount = 0;
                Communication.Logic.CreatureLogic.UpdateCreatureStats(player);
            }
            else
            {
                if (player.PlayerMount != 0)
                    Communication.Global.VisibleService.Send(player, new SpMountHide(player, player.PlayerMount));

                player.PlayerMount = skillId;
                Communication.Global.VisibleService.Send(player, new SpMountShow(player, Data.Data.Mounts[skillId].MountId, skillId));
                Communication.Logic.CreatureLogic.UpdateCreatureStats(player);
            }*/

        }

        public void PlayerEnterWorld(Player player)
        {
            if (player.PlayerMount != 0)
            {
                Communication.Global.VisibleService.Send(player, new SpMountShow(player, Data.Data.Mounts[player.PlayerMount].MountId, player.PlayerMount));
                Communication.Logic.CreatureLogic.UpdateCreatureStats(player);
                DAOManager.playerDAO.UpdatePlayer(player);
            }
        }
    }
}
