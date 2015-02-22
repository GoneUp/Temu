using System.IO;

namespace Tera.Network.old_Server
{
    public class SpItemInfo : ASendPacket
    {
        protected int ItemId;
        protected long ItemUniqueId;
        protected string CreatorName;
        protected string SoulboundName;

        public SpItemInfo(int itemId, long itemUniqueId, string creatorName = "", string soulboundName = "")
        {
            ItemId = itemId;
            ItemUniqueId = itemUniqueId;
            CreatorName = creatorName;
            SoulboundName = soulboundName;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, 0);
            int creatorShift = (int) writer.BaseStream.Position;
            WriteWord(writer, 0); //Creator name shift?
            int soulboundShift = (int) writer.BaseStream.Position;
            WriteWord(writer, 0); //Soulbound name shift
            WriteWord(writer, 19); //19
            WriteWord(writer, 0);

            WriteLong(writer, ItemUniqueId); //ItemUniqueId
            WriteDword(writer, ItemId); //ItemId
            WriteLong(writer, ItemUniqueId); //ItemUniqueId

            WriteDword(writer, 0); //???
            WriteDword(writer, 0);
            WriteDword(writer, 0); //???

            WriteDword(writer, 0);
            WriteDword(writer, 1);
            WriteDword(writer, 1); //Count
            WriteDword(writer, 0); //Enchant level
            WriteDword(writer, 0);
            WriteByte(writer, 1); //Can't trade
            WriteDword(writer, 0);
            WriteDword(writer, 0); //EffectId
            WriteDword(writer, 0); //EffectId on enchant +3
            WriteDword(writer, 0); //EffectId on enchant +6
            WriteDword(writer, 0); //EffectId on enchant +9
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteWord(writer, 0);
            WriteByte(writer, 0);
            WriteByte(writer, 0); //Requires Identification Scroll to remove
            WriteByte(writer, 0); //Masterwork
            WriteDword(writer, 0);


            WriteByte(writer, 0); //Show "Stats Totals when Equipped"

            //"Stats Totals when Equipped"
            WriteDword(writer, 0); //Attack
            WriteDword(writer, 0); //Defence
            WriteDword(writer, 0); //Knockdown
            WriteDword(writer, 0); //Resist to knockdown
            WriteSingle(writer, 0); //Crit chanse (float)
            WriteSingle(writer, 0); //Crit resist (float)
            WriteSingle(writer, 0); //Crit power (float)
            WriteDword(writer, 0); //Damage
            WriteDword(writer, 0); //Balance
            WriteDword(writer, 0); //Attack speed
            WriteDword(writer, 0); //Movement
            WriteDword(writer, 0); //Binding (float)
            WriteDword(writer, 0); //Poison (float)
            WriteDword(writer, 0); //Stun (float)

            WriteDword(writer, 0); //Add attack
            WriteDword(writer, 0); //Add defence
            WriteDword(writer, 0); //Add knockdown
            WriteDword(writer, 0); //Add resist to knockdown
            WriteDword(writer, 0); //Add crit chanse (float)
            WriteDword(writer, 0); //Add crit resist (float)
            WriteDword(writer, 0); //Add crit power (float)
            WriteDword(writer, 0); //Add damage
            WriteDword(writer, 0); //Add Balance
            WriteDword(writer, 0); //Add attack speed
            WriteDword(writer, 0); //Add movement
            WriteDword(writer, 0); //Add Binding (float)
            WriteDword(writer, 0); //Add poison (float)
            WriteDword(writer, 0); //Add stun (float)

            WriteDword(writer, 0); //Min attack
            WriteDword(writer, 0); //Add min attack

            WriteDword(writer, 3);
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteDword(writer, 0);
            WriteDword(writer, 1); //Item level
            WriteDword(writer, 0);

            writer.Seek(creatorShift, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, CreatorName); //Creator name

            writer.Seek(soulboundShift, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, SoulboundName); //Soulbound name
        }
    }
}