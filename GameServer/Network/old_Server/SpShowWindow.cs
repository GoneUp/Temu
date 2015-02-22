using System.IO;
using Tera.Data.Enums;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.World.Requests;

namespace Tera.Network.old_Server
{
    public class SpShowWindow : ASendPacket
    {
        public static int TradeConfirm = 3;
        public static int PartyConfirm = 4;
        public static int PartyAcceptConfirm = 5;
        public static int Mailbox = 8;
        public static int Inventory = 9;
        public static int GuildConfirm = 10;
        public static int DuelConfirm = 14;
        public static int WorldTeleport = 17;
        public static int DeathmatchConfirm = 21;
        public static int Inventory2 = 23;
        public static int DeathmatchBet = 27;
        public static int Bank = 28;
        public static int LearnSkills = 29;
        public static int BattleGroupConfirm = 30;
        public static int Extraction = 31;
        public static int SoulbindStatusLine = 34;
        public static int CombiningStatusLine = 35;
        public static int Enchant = 36;
        public static int Negotiating = 38;
        public static int Remodel = 42;
        public static int RestoreAppearance = 43;
        public static int Dye = 44;
        public static int StartGvGBattle = 46;
        public static int GvGSurrender = 47;


        protected Player Player;
        protected int Type;

        /// <summary>
        /// A request, that will be sent by this packet
        /// </summary>
        protected Request Request;

        /// <summary>
        /// Show window
        /// </summary>
        /// <param name="request">Request that will be displayed</param>
        public SpShowWindow(Request request)
        {
            Request = request;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 0); //Player NameShift
            WriteWord(writer, 0); //Target NameShift
            WriteWord(writer, 0); //Packet Length
            WriteWord(writer, (short) (Request.Type == RequestType.Extraction ? 4 : 0));

            WriteUid(writer, Request.Owner);
            WriteUid(writer, Request.Target);

            WriteDword(writer, Request.Type.GetHashCode());
            WriteDword(writer, Request.UID);
            WriteDword(writer, 0);
            WriteDword(writer, Request.Timeout);

            writer.Seek(4, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, Request.Owner.PlayerData.Name);
            WriteWord(writer, 0);

            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            if (Request.Target != null)
                WriteString(writer, Request.Target.PlayerData.Name);
            else
                WriteWord(writer, 0);
            WriteWord(writer, 0);

            if (Request.Type == RequestType.Extraction)
                WriteDword(writer, 0); // ExtractSubtype

            writer.Seek(8, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}