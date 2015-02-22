using System.Collections.Generic;
using System.IO;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpFlightPoints : ASendPacket
    {
        protected List<FlyTeleport> FlyTeleports;

        public SpFlightPoints(List<FlyTeleport> flyTeleports)
        {
            FlyTeleports = flyTeleports;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) FlyTeleports.Count);
            WriteWord(writer, 8);

            for (int i = 0; i < FlyTeleports.Count; i++)
            {
                WriteWord(writer, (short)writer.BaseStream.Length);

                short shift = (short) writer.BaseStream.Length;
                WriteWord(writer, 0);

                WriteDword(writer, FlyTeleports[i].Id);
                WriteDword(writer, FlyTeleports[i].Cost);
                WriteDword(writer, FlyTeleports[i].FromNameId);
                WriteDword(writer, FlyTeleports[i].ToNameId);
                WriteDword(writer, FlyTeleports[i].Level);

                if(FlyTeleports.Count >= i+1)
                {
                    writer.Seek(shift, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);
                }
            }
        }
    }
}