using System;
using System.Collections.Generic;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Models.Operations
{
    public class DirectDebit
    {
        public string OrderId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public long Amount { get; set; }
        public long? Fee { get; set; }
        
        public AccountRef Beneficiary { get; set; }
        public bool IsMine { get; set; }

        public string Message { get; set; }

        public MandateRef Mandate { get; set; }
        
    }

    public class DirectDebitRequest : DirectDebit
    {
        
    }
    public class DirectDebitResponse : DirectDebit
    {
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }
        public CardPaymentStatus Status { get; set; }
        public List<Schedule> Schedules { get; set; }
    }

    public class CancelRequest
    {
        public CardPaymentStatus Status { get; set; }
    }
}