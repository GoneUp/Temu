using System;
using System.Collections.Generic;
using Tera.Data.Structures.Objects;
using Utils.Logger;

namespace Tera.Data.Structures.World
{
    public class MapInstance : Statistical
    {
        public int MapId;

        public bool IsEditingMode = false;

        public List<Player.Player> Players = new List<Player.Player>();
        public List<Npc.Npc> Npcs = new List<Npc.Npc>();
        public List<Gather.Gather> Gathers = new List<Gather.Gather>();
        public List<Item> Items = new List<Item>();
        public List<Climb> Climbs = new List<Climb>();
        public List<Projectile> Projectiles = new List<Projectile>();
        public List<Campfire> Campfires = new List<Campfire>();

        public object CreaturesLock = new object();

        public virtual void OnMove(Player.Player player)
        {
            
        }

        public virtual void OnNpcKill(Player.Player killer, Npc.Npc killed)
        {
            
        }

        public virtual void Release()
        {
            try
            {
                for (int i = 0; i < Npcs.Count; i++)
                    Npcs[i].Release();

                Npcs.Clear();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Exception, "MapInstance: Dispose " + ex.Message);
            }

            try
            {
                for (int i = 0; i < Gathers.Count; i++)
                    Gathers[i].Release();

                Gathers.Clear();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Exception, "MapInstance: Dispose " + ex.Message);
            }

            try
            {
                for (int i = 0; i < Items.Count; i++)
                    Items[i].Release();

                Items.Clear();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Exception, "MapInstance: Dispose " + ex.Message);
            }

            try
            {
                for (int i = 0; i < Campfires.Count; i++)
                    Campfires[i].Release();

                Campfires.Clear();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Exception, "MapInstance: Dispose " + ex.Message);
            }

            Climbs = null;
        }
    }
}