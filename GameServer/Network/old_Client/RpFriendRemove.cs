﻿namespace Tera.Network.old_Client
{
    public class RpFriendRemove : ARecvPacket
    {
        protected int FriendId;

        public override void Read()
        {
            FriendId = ReadDword();
        }

        public override void Process()
        {
            
        }
    }
}