using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Tera.Data.Structures.Guild;
using Tera.Data.Structures.Player;
using Utils;
using Utils.Logger;

namespace Tera.Database.DAO
{
    public class GuildDAO : DAOManager
    {
        private MySqlConnection GuildDAOConnection;

        public GuildDAO(string conStr) : base(conStr)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            GuildDAOConnection = new MySqlConnection(conStr);
            GuildDAOConnection.Open();

            stopwatch.Stop();

            Logger.WriteLine(LogState.Info,"DAO: GuildDAO Initialized with {0} Guilds in {1}s"
            , LoadTotalGuilds()
            , (stopwatch.ElapsedMilliseconds / 1000.0).ToString("0.00"));
        }

        public bool SaveGuild(List<Player> players, string guildName)
        {
            string SQL = "INSERT INTO `guilds` "
                + "(guildname, guildlogo, level, creationdate) "
                + "VALUES(?name, ?logo, ?level, ?credate);";
            MySqlCommand cmd = new MySqlCommand(SQL, GuildDAOConnection);
            cmd.Parameters.AddWithValue("?name", guildName);
            cmd.Parameters.AddWithValue("?guildlogo", "");
            cmd.Parameters.AddWithValue("?level", "1");
            cmd.Parameters.AddWithValue("?credate", RandomUtilities.GetRoundedUtc());

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Exception ,"DAO: Guild Save Failed!" + ex.Message);
                return false;
            }

            return true;
        }

        public void AddCharacterToGuild(Player player, Guild guild)
        {
            string SQL = "UPDATE guilds SET guildmembers = ?player WHERE guildname = ?gname";
            MySqlCommand cmd = new MySqlCommand(SQL, GuildDAOConnection);
            cmd.Parameters.AddWithValue("?player", player);
            cmd.Parameters.AddWithValue("?gname", guild.GuildName);
        }

        public void GuildHistoryAdd(string historyEvent)
        {
        }

        public int LoadTotalGuilds()
        {
            string SQL = "SELECT COUNT(*) FROM guilds";
            MySqlCommand cmd = new MySqlCommand(SQL, GuildDAOConnection);
            MySqlDataReader reader = cmd.ExecuteReader();

            int count = 0;
            while (reader.Read())
            {
                count = reader.GetInt32(0);
            }

            reader.Close();
            return count;
        }

        public Dictionary<int, Guild> LoadGuilds()
        {
            string SQL = "SELECT * FROM `guilds`";
            MySqlCommand cmd = new MySqlCommand(SQL, GuildDAOConnection);
            MySqlDataReader LoadGuildsReader = cmd.ExecuteReader();

            Dictionary<int, Guild> guildlist = new Dictionary<int, Guild>();
            if (LoadGuildsReader.HasRows)
            {
                while (LoadGuildsReader.Read())
                {
                    Guild tmpGuild = new Guild();
                    {
                        //TODO: 
                    };
                    guildlist.Add(tmpGuild.GuildId, tmpGuild);
                }
            }
            LoadGuildsReader.Close();

            return guildlist;
        }

        /*
        public List<Player> LoadPlayerGuild(string accName)
        {
            string cmdString = "SELECT * FROM `guilds` WHERE AccountName=?username AND deleted = ?delete";
            MySqlCommand command = new MySqlCommand(cmdString, GuildDAOConnection);
            command.Parameters.AddWithValue("?username", accName);
            command.Parameters.AddWithValue("?delete", 0);
            MySqlDataReader reader = command.ExecuteReader();

            List<Player> players = new List<Player>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Player player = new Player()
                    {
                        Id = reader.GetInt32(0),
                        AccountName = reader.GetString(1),
                        Level = reader.GetInt32(2),
                        Exp = reader.GetInt64(3),
                        ExpRecoverable = reader.GetInt64(4),
                        Mount = reader.GetInt32(5),
                        UiSettings = (reader.GetString(6) != null) ? HexToBytes(reader.GetString(6)) : new byte[0],
                        GuildAccepted = (byte)reader.GetInt16(7),
                        PraiseGiven = (byte)reader.GetInt16(8),
                        LastPraise = reader.GetInt32(9),
                        CurrentBankSection = reader.GetInt32(10),
                        CreationDate = reader.GetInt32(11),
                        LastOnline = reader.GetInt32(12)
                    };
                    players.Add(player);
                }
            }
            reader.Close();

            foreach (var player in players)
            {
                cmdString = "SELECT * FROM character_data WHERE PlayerId=?id";
                command = new MySqlCommand(cmdString, PlayerDAOConnection);
                command.Parameters.AddWithValue("?id", player.Id);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        player.PlayerData = new PlayerData()
                        {
                            Name = reader.GetString(1),
                            Gender = (Gender)Enum.Parse(typeof(Gender), reader.GetString(2)),
                            Race = (Race)Enum.Parse(typeof(Race), reader.GetString(3)),
                            Class = (PlayerClass)Enum.Parse(typeof(PlayerClass), reader.GetString(4)),
                            Data = HexToBytes(reader.GetString(5)),
                            Details = HexToBytes(reader.GetString(6)),
                        };

                        player.Position = new Structures.World.WorldPosition()
                        {
                            MapId = reader.GetInt32(7),
                            X = reader.GetFloat(8),
                            Y = reader.GetFloat(9),
                            Z = reader.GetFloat(10),
                            Heading = reader.GetInt16(11)
                        };
                    }
                }
                reader.Close();
            }

            
            return players;
        }
         */

        public bool AddGuildRank(Guild g, GuildMemberRank gmr)
        {
            string SQL = "INSERT INTO guild_ranks "
            + "(gid, rankprivileges, rankname) "
            + "VALUES(?gid, ?rankpriv, ?rankname);";

            // Are we GM
            if (gmr.RankName == "GuildMaster")
                gmr.RankPrivileges = 7;
            else
                gmr.RankPrivileges = 0;

            MySqlCommand cmd = new MySqlCommand(SQL, GuildDAOConnection);
            cmd.Parameters.AddWithValue("?gid", g.GuildId);
            cmd.Parameters.AddWithValue("?rankpriv", gmr.RankPrivileges);
            cmd.Parameters.AddWithValue("?rankname", gmr.RankName);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Exception ,"DAO: Guild Rank Add Error!" + ex.Message);
                return false;
            }
            return true;
        }
    }
}
