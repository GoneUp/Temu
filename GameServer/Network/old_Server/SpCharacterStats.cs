using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterStats : ASendPacket
    {
        protected Player Player;

        public SpCharacterStats(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, Player.LifeStats.Hp); // current hp
            WriteDword(writer, Player.LifeStats.Mp); // current mp
            WriteDword(writer, 0); //unk
            WriteDword(writer, Player.MaxHp);
            WriteDword(writer, Player.MaxMp);

            WriteStats(writer, Player);

            WriteByte(writer, (byte) Player.GetLevel()); //Level
            WriteByte(writer, 0x00);
            WriteByte(writer, (byte) Player.PlayerMode.GetHashCode());

            WriteByte(writer, 0);
            if(Player.LifeStats.Stamina > 100)
                WriteByte(writer, 4); // Adundunt stamina
            else if(Player.LifeStats.Stamina > 20)
                WriteByte(writer, 3); // Normal stamina
            else
                WriteByte(writer, 2); // Poor stamina
            WriteByte(writer, "0001");
            WriteDword(writer, (int)(Player.GameStats.HpStamina * (Player.LifeStats.Stamina / 120.0))); //Player.GameStats.MaxHp - Player.GameStats.MaxHp); //TODO: WTF?
            WriteDword(writer, (int)(Player.GameStats.MpStamina * (Player.LifeStats.Stamina / 120.0))); //Player.GameStats.MaxMp - Player.GameStats.MaxMp); //TODO: WTF?
            WriteDword(writer, Player.LifeStats.Stamina);
            WriteDword(writer, 120); //stamina max val
            WriteByte(writer, "0000000000000000000000000000000002000000020000000000000000000000401F000005000000");
        }

        //EBF0 8B0500002B010000000000008B0500002B01000032000000230000002800460069006400000048420000964200000040B4000000B40000003200000006000B00000034420000344200003442CFFFFFFFDEFFFFFFD8FFBBFF97FF9DFF000048C27DFF95C2000080BFC6FFFFFFC6FFFFFFECFFFFFFFAFFF5FFFAFE33C2FAFE33C2FAFE33C2140000000400018B0500002B01000078000000780000000000000000000000000000000000000002000000020000000000000000000000401F000005000000
    }
}