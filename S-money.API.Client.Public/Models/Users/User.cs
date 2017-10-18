using System.Collections.Generic;
using System.Linq;
using Smoney.API.Client.Enumerations;

namespace Smoney.API.Client.Models.Users
{
    public class User
    {
        public long Id { get; set; }
        public string AppUserId { get; set; }
        public ClientType? Type { get; set; }
        public ClientRole? Role { get; set; }
        public UserProfile Profile { get; set; }
        public decimal Amount { get; set; }
        public List<Account> SubAccounts { get; set; }
        public List<BankAccountRef> BankAccounts { get; set; }
        public List<CardRef> CBCards { get; set; }
        public UserStatus? Status { get; set; }
        public Company Company { get; set; }

        public Account DefaultAccount
        {
            get { return SubAccounts != null ? SubAccounts.FirstOrDefault(x => x.IsDefault) : null; }
        }
    }
}