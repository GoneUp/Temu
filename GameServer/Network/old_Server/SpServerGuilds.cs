using System.Collections.Generic;
using System.IO;
using Tera.Data.Structures.Guild;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpServerGuilds : ASendPacket
    {
        protected List<Guild> Guilds;
        protected int TabId;
        protected int TotalGuilds;
        protected int TabsCounter;
        //protected Dictionary<int, List<Guild>> Guilds;
        public SpServerGuilds(List<Guild> guilds, int tabId, int totalGuilds, int tabsCounter)
        {
            Guilds = guilds;
            TabId = tabId;
            TotalGuilds = totalGuilds;
            TabsCounter = tabsCounter;
        }
        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 0x11);
            WriteWord(writer, 0); //First shift
            WriteDword(writer, TabId);
            WriteDword(writer, TabsCounter);
            WriteDword(writer, TotalGuilds);
            WriteDword(writer, 1);

            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            for (int i = 0; i < Guilds.Count; i++)
            {
                Guild g = Guilds[i];

                short shift = (short) writer.BaseStream.Length;

                WriteWord(writer, shift); //Now shift
                WriteWord(writer, 0); //Next shift

                int namesShift = (int) writer.BaseStream.Position;
                WriteWord(writer, 0); //Fuild Name shift
                WriteWord(writer, 0); //Guildmaster name shift
                WriteWord(writer, 0); //??? shift
                WriteWord(writer, 0); //Guild ad shift
                WriteWord(writer, 0); //Logo shift

                WriteDword(writer, g.Level); //Guild Level
                WriteDword(writer, g.CreationDate); //A913E4F //Founded time
                WriteDword(writer, 0); //???
                WriteDword(writer, g.GuildMembers.Count); //Members
                WriteDword(writer, 0); //Connect rate
                WriteDword(writer, g.Praises); //Total prise
                WriteByte(writer, 0); //Can apply to guild

                writer.Seek(namesShift, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteString(writer, g.GuildName); //Name

                writer.Seek(namesShift + 2, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                Player leader = Communication.Global.GuildService.GetLeader(g);
                WriteString(writer, leader != null ? leader.PlayerData.Name : "No leader"); //Owner

                writer.Seek(namesShift + 4, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteString(writer, "Owners"); //???

                writer.Seek(namesShift + 6, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteString(writer, g.GuildAd);

                writer.Seek(namesShift + 8, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteString(writer, "");
                //WriteString(writer, "guildlogo_11_45_1"); //Logo

                if (Guilds.Count > i + 1)
                {
                    writer.Seek(shift + 2, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);
                }
            }
        }
    }
}