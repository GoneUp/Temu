using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace AccountService
{
    public class Account
    {
        public Account()
        {
            Username = this.Username;
        }
        public uint AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public uint AccessLevel { get; set; }
        public string Membership { get; set; }
        public bool IsGM { get; set; }
        public long LastOnlineUtc { get; set; }
        public uint Coins { get; set; }
        public string Ip { get; set; }
        public string Settings { get; set; }
        public bool IsOnline { get; set; }
        public bool IsBanned { get; set; }
        public long UnBanDate { get; set; }//BanTime as Ticks this is better to save into db :)
        public long RegisterDate { get; set; }
    }

}