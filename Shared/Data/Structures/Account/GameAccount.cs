using System.Collections.Generic;
using Tera.Data.Enums.Item;
using Tera.Data.Interfaces;
using Tera.Data.Structures.Player;
using Utils;

namespace Tera.Data.Structures.Account
{
    public class GameAccount : TeraAccount
    {
        public IConnection Connection;

        public bool IsOnline
        {
            get { return Connection != null; }
        }


        public List<Player.Player> Players = new List<Player.Player>();

        public string UiSettings = null;

        public Storage AccountWarehouse = new Storage{StorageType = StorageType.AccountWarehouse};

        public List<AccountItem> AccountItems = new List<AccountItem>();

        public DelayedAction ExitAction;

        public override void Release()
        {
            base.Release();

            for (int i = 0; i < Players.Count; i++)
                Players[i].Release();

            Players = null;
        }
    }
}