using System.Collections.Generic;
using Tera.Data.Enums.Item;
using Tera.Data.Interfaces;
using Tera.Data.Structures.Player;
using Utils;

namespace Tera.Data.Structures.Account
{
    [ProtoBuf.ProtoContract]
    public class GameAccount : TeraAccount
    {
        public IConnection Connection;

        public bool IsOnline
        {
            get { return Connection != null; }
        }


        [ProtoBuf.ProtoMember(3)]
        public List<Player.Player> Players = new List<Player.Player>();

        [ProtoBuf.ProtoMember(4)]
        public string UiSettings = null;

        [ProtoBuf.ProtoMember(10)]
        public Storage AccountWarehouse = new Storage{StorageType = StorageType.AccountWarehouse};

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