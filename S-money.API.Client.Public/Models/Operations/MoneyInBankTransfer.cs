using System;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Models.Operations
{
    public class MoneyInBankTransfer
    {
        public long Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public long Amount { get; set; }
        public PaymentStatus Status { get; set; }

        public AccountRef Beneficiary { get; set; }

        public bool IsMine { get; set; }

        public string Reference { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}