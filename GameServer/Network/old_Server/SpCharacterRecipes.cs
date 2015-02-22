using System.Collections.Generic;
using System.IO;
using Tera.Data.Structures.Craft;
using Utils;

namespace Tera.Network.old_Server
{
    public class SpCharacterRecipes : ASendPacket
    {
        protected List<Recipe> Recipes = new List<Recipe>();
        protected bool IsForCraftReset;

        /// <summary>
        /// Send when need to update recipes or reset craft process
        /// </summary>
        /// <param name="recipes">Can be null, when send for craft reset</param>
        /// <param name="isForCraftReset"> </param>
        public SpCharacterRecipes(List<Recipe> recipes, bool isForCraftReset = false)
        {
            Recipes = recipes;
            IsForCraftReset = isForCraftReset;
        }

        public override void Write(BinaryWriter writer)
        {
            if(IsForCraftReset)
            {
                WriteDword(writer, 0);
                WriteByte(writer, 1);
                WriteByte(writer, 1);
                return;
            }

            WriteWord(writer, (short) Recipes.Count);
            WriteDword(writer, 0); // first recipe shift

            writer.Seek(6, SeekOrigin.Begin);
            WriteDword(writer, (int) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            for (int i = 0; i < Recipes.Count; i++)
            {
                WriteWord(writer, (short)writer.BaseStream.Length);
                short recShift = (short) writer.BaseStream.Length;
                WriteWord(writer, 0);

                WriteWord(writer, (short) Recipes[i].NeededItems.Count);
                WriteWord(writer, 0); //items start shift

                WriteDword(writer, Recipes[i].RecipeId);
                WriteDword(writer, Recipes[i].CraftStat.GetHashCode());
                WriteDword(writer, 0); // O_o
                WriteDword(writer, Recipes[i].ResultItem.Key); //itemid
                WriteDword(writer, Recipes[i].ResultItem.Value); //counter
                WriteDword(writer, Recipes[i].Level);
                WriteLong(writer, RandomUtilities.GetRoundedUtc());
                WriteByte(writer, 0);
                WriteDword(writer, Recipes[i].NeededItems.Count);

                writer.Seek(recShift + 4, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                int counter = 1;
                foreach (KeyValuePair<int, int> itm in Recipes[i].NeededItems)
                {
                    WriteWord(writer, (short) writer.BaseStream.Length);

                    short sh1 = (short) writer.BaseStream.Length;

                    WriteWord(writer, 0);

                    WriteDword(writer, itm.Key); //itemid
                    WriteDword(writer, itm.Value); //counter

                    if (counter < Recipes[i].NeededItems.Count)
                    {
                        writer.Seek(sh1, SeekOrigin.Begin);
                        WriteWord(writer, (short) writer.BaseStream.Length);
                        writer.Seek(0, SeekOrigin.End);
                    }
                    counter++;
                }

                if (i + 1 < Recipes.Count)
                {
                    writer.Seek(recShift, SeekOrigin.Begin);
                    WriteWord(writer, (short)writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);
                }
            }
        }
    }
}
