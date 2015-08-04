using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoney.API.Client.Models.Users
{
    public class Account
    {
        public long Id { get; set; }
        public string AppAccountId { get; set; }
        public int Amount { get; set; }
        public string DisplayName { get; set; }
        public bool IsDefault { get; set; }
    }
}