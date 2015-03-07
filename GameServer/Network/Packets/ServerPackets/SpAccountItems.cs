using System;
using System.IO;
using System.Linq;
using Tera.Data.Structures.Account;

namespace Tera.Network.Packets.ServerPackets
{
    public class SpAccountItems : ASendPacket
    {
        protected GameAccount GameAccount;

        public SpAccountItems(GameAccount gameAccount)
        {
            GameAccount = gameAccount;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, Convert.ToInt16(GameAccount.AccountItems.Count));
            WriteWord(writer, 8); //BeginnListPos

            int i = 0;
            foreach (var tmpItem in GameAccount.AccountItems)
            {    
                if (i > 0) {SetPointer(writer, i-1);}

                WriteWord(writer, Convert.ToInt16(writer.BaseStream.Position));

                //NexPos Pointer. We create a "marker" at a given pos first, then we set it on the next run. On the last the pointer will be 0 as expected.
                CreatePointer(writer, i);
       

                WriteDword(writer, tmpItem.ItemId);
                WriteDword(writer, tmpItem.Options);
                WriteDword(writer, 0);
                i++;
            }
        }
    }
}