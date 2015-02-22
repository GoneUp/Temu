using System.Collections.Generic;
using System.IO;
using Tera.Data.Structures.Npc;
using Tera.Data.Structures.Quest;
using Tera.Data.Structures.Quest.Enums;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpShowDialog : ASendPacket
    {
        protected int DialogUid;
        protected int Stage;
        protected Npc Npc;
        protected List<DialogButton> Buttons;
        protected int DialogId;
        protected int Special1;
        protected int Special2;
        protected int Page;
        protected Quest Quest;
        protected QuestReward Reward;

        public SpShowDialog(Npc npc, int stage, List<DialogButton> buttons, int dialogId, int special1, int special2, int page, int dialogUid, Quest quest = null, QuestReward reward = null)
        {
            Npc = npc;
            Stage = stage;
            Buttons = buttons;
            DialogUid = dialogUid;
            DialogId = dialogId;
            Special1 = special1;
            Special2 = special2;
            Page = page;
            Quest = quest;
            Reward = reward;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) Buttons.Count); //Buttons count
            int buttonsShift = (int) writer.BaseStream.Position;
            WriteWord(writer, 0); //First button shift

            WriteWord(writer, (short) (Quest == null ? 0 : 1));
            int rewardShift = (int) writer.BaseStream.Position;
            WriteWord(writer, 0);

            WriteUid(writer, Npc);
            WriteDword(writer, Stage); //Stage?
            WriteDword(writer, DialogId); //DialogId
            WriteDword(writer, Special1);
            WriteDword(writer, Special2);
            WriteDword(writer, Page); //Page?
            WriteDword(writer, DialogUid); //DialogUid
            WriteByte(writer, new byte[5]);
            WriteDword(writer, 1);
            WriteByte(writer, new byte[7]);
            WriteByte(writer, "FFFFFFFF");

            int i = 1;
            foreach (DialogButton dialogButton in Buttons)
            {
                writer.Seek(buttonsShift, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteWord(writer, (short) writer.BaseStream.Position);
                buttonsShift = (int) writer.BaseStream.Position;
                WriteWord(writer, 0);

                WriteWord(writer, (short) (writer.BaseStream.Position + 10));
                WriteDword(writer, i++);
                WriteDword(writer, dialogButton.Icon.GetHashCode());
                WriteString(writer, dialogButton.Text);
            }

            if (Quest != null)
            {
                int itemsShift = 0;

                writer.Seek(rewardShift, SeekOrigin.Begin);
                WriteWord(writer, (short)writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteWord(writer, (short)writer.BaseStream.Position);
                WriteWord(writer, 0);

                if (Reward != null && Reward.Items != null)
                {
                    WriteWord(writer, (short) Reward.Items.Count);
                    itemsShift = (int) writer.BaseStream.Position;
                    WriteWord(writer, 0);
                }
                else
                    WriteDword(writer, 0);

                WriteDword(writer, 0);
                WriteDword(writer, Quest.QuestRewardType == QuestRewardType.Choice ? 1 : 3); //1 Selectable reward //2 Unspecified reward //3 All
                WriteDword(writer, Quest.RewardExp);
                WriteDword(writer, Quest.RewardMoney);
                WriteDword(writer, 0);
                WriteDword(writer, 0); //Polici points
                WriteDword(writer, 0);
                WriteDword(writer, 0); //Reputation levels [exp]
                WriteDword(writer, 0); //Reputation

                if (Reward == null || Reward.Items == null)
                    return;

                for (int x = 0; x < Reward.Items.Count; x++)
                {
                    writer.Seek(itemsShift, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);

                    WriteWord(writer, (short)writer.BaseStream.Position);
                    itemsShift = (int) writer.BaseStream.Position;
                    WriteWord(writer, 0);

                    WriteDword(writer, Reward.Items[x].Key);
                    WriteDword(writer, Reward.Items[x].Value);
                }
            }
        }
    }
}