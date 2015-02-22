using System;
using System.IO;
using System.Text;
using Tera.Data.Enums.Gather;
using Tera.Data.Interfaces;
using Tera.Data.Structures;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Player;
using Utils;
using Utils.Logger;

namespace Tera.Network
{
    public abstract class ASendPacket : ISendPacket
    {
        protected byte[] Datas;
        protected object WriteLock = new object();

        public void Send(Player player)
        {
            Send(player.Connection);
        }

        public void Send(params Player[] players)
        {
            for (int i = 0; i < players.Length; i++)
                Send(players[i].Connection);
        }

        public void Send(params IConnection[] states)
        {
            for (int i = 0; i < states.Length; i++)
                Send(states[i]);
        }

        public void Send(IConnection state)
        {
            if (state == null || !state.IsValid)
            { return; }

            if (!OpCodes.Send.ContainsKey(GetType()))
            {
                Logger.WriteLine(LogState.Warn,"UNKNOWN packet opcode: {0}", GetType().Name);
                return;
            }


            lock (WriteLock)
            {
                if (Datas == null)
                {
                    try
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream, new UTF8Encoding()))
                            {
                                WriteWord(writer, 0); //Reserved for length
                                WriteWord(writer, OpCodes.Send[GetType()]);
                                Write(writer);
                            }

                            Datas = stream.ToArray();
                            BitConverter.GetBytes((short) Datas.Length).CopyTo(Datas, 0);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine(LogState.Warn,"Can't write packet: {0}", GetType().Name);
                        Logger.WriteLine(LogState.Warn, "ASendPacket" + ex);
                        return;
                    }
                }
            }

            state.PushPacket(Datas);
        }

        public abstract void Write(BinaryWriter writer);

        protected void WriteDword(BinaryWriter writer, int val)
        {
            writer.Write(val);
        }

        protected void WriteWord(BinaryWriter writer, short val)
        {
            writer.Write(val);
        }

        protected void WriteByte(BinaryWriter writer, byte val)
        {
            writer.Write(val);
        }

        protected void WriteDouble(BinaryWriter writer, double val)
        {
            writer.Write(val);
        }

        protected void WriteSingle(BinaryWriter writer, float val)
        {
            writer.Write(val);
        }

        protected void WriteLong(BinaryWriter writer, long val)
        {
            writer.Write(val);
        }

        protected void WriteString(BinaryWriter writer, String text)
        {
            if (text == null)
            {
                writer.Write((short) 0);
            }
            else
            {
                Encoding encoding = Encoding.Unicode;
                writer.Write(encoding.GetBytes(text));
                writer.Write((short) 0);
            }
        }

        protected void WriteByte(BinaryWriter writer, string hex)
        {
            writer.Write(hex.HexSringToBytes());
        }

        protected void WriteByte(BinaryWriter writer, byte[] data)
        {
            writer.Write(data);
        }

        protected void WriteUid(BinaryWriter writer, Uid uid)
        {
            if (uid == null)
            {
                writer.Write(0L);
                return;
            }

            writer.Write(uid.UID);
            writer.Write(UidFactory.GetFamily(uid).GetHashCode());
        }

        protected void WriteStats(BinaryWriter writer, Player player)
        {
            CreatureBaseStats baseStats = player.GameStats; // Communication.Global.StatsService.InitStats(Player)

            #region Base stats
            WriteDword(writer, baseStats.Power - player.EffectsImpact.ChangeOfPower);
            WriteDword(writer, baseStats.Endurance - player.EffectsImpact.ChangeOfEndurance);
            WriteWord(writer, (short) (baseStats.ImpactFactor - player.EffectsImpact.ChangeOfImpactFactor));
            WriteWord(writer, (short) (baseStats.BalanceFactor - player.EffectsImpact.ChangeOfBalanceFactor));
            WriteWord(writer, (short) (baseStats.Movement - player.EffectsImpact.ChangeOfMovement));
            WriteWord(writer, (short) (baseStats.AttackSpeed - player.EffectsImpact.ChangeOfAttackSpeed));

            // Crit. stats
            WriteSingle(writer, baseStats.CritChanse);
            WriteSingle(writer, baseStats.CritResist);
            WriteWord(writer, 0);
            WriteByte(writer, 0);
            WriteByte(writer, 0x40); // Crit how much times?

            WriteDword(writer, baseStats.Attack); //min attack
            WriteDword(writer, baseStats.Attack); //max attack
            WriteDword(writer, baseStats.Defense);
            WriteWord(writer, (short)baseStats.Impact);
            WriteWord(writer, (short)baseStats.Balance);

            WriteSingle(writer, baseStats.WeakeningResist); // Weakening
            WriteSingle(writer, baseStats.PeriodicResist); // Periodic
            WriteSingle(writer, baseStats.StunResist); // Stun
            #endregion

            #region Additional stats
            WriteDword(writer, player.EffectsImpact.ChangeOfPower);
            WriteDword(writer, player.EffectsImpact.ChangeOfEndurance);
            WriteWord(writer, player.EffectsImpact.ChangeOfImpactFactor);
            WriteWord(writer, player.EffectsImpact.ChangeOfBalanceFactor);

            WriteWord(writer, player.EffectsImpact.ChangeOfMovement);
            WriteWord(writer, player.EffectsImpact.ChangeOfAttackSpeed);

            WriteSingle(writer, player.GameStats.CritChanse - baseStats.CritChanse);
            WriteSingle(writer, player.GameStats.CritResist - baseStats.CritResist);
            WriteDword(writer, 0);

            WriteDword(writer, player.GameStats.Attack - baseStats.Attack); //min attack
            WriteDword(writer, player.GameStats.Attack - baseStats.Attack); //max attack
            WriteDword(writer, player.GameStats.Defense - baseStats.Defense);

            WriteWord(writer, (short)(player.GameStats.Impact - baseStats.Impact));
            WriteWord(writer, (short)(player.GameStats.Balance - baseStats.Balance));

            WriteSingle(writer, player.GameStats.WeakeningResist - baseStats.WeakeningResist); // Weakening
            WriteSingle(writer, player.GameStats.PeriodicResist - baseStats.PeriodicResist); // Periodic
            WriteSingle(writer, player.GameStats.StunResist - baseStats.StunResist); // Stun
            #endregion
        }

        protected void WriteGatherStats(BinaryWriter writer, Player player)
        {
            WriteWord(writer, player.PlayerCraftStats.GetGatherStat(TypeName.Energy));
            WriteWord(writer, player.PlayerCraftStats.GetGatherStat(TypeName.Herb));
            WriteWord(writer, 0); //unk, mb bughunting
            WriteWord(writer, player.PlayerCraftStats.GetGatherStat(TypeName.Mine));
        }
    }
}