using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Models.Operations
{
    public class BankTransfer
    {
        public long Id { get; set; }
        public AccountRef AccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        public long Amount { get; set; }
        public Fee Fee { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
