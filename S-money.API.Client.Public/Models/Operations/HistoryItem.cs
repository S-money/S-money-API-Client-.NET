using System;
using Smoney.API.Client.Models.Attachments;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Models.Operations
{
    public class HistoryItem
    {
        public long Id { get; set; }
        public DateTime OperationDate { get; set; }
        public long Amount { get; set; }
        public long Fee { get; set; }
        public string Label { get; set; }
        public Attachment Attachment { get; set; }
        public bool IsNew { get; set; }
        public int Type { get; set; }
        public int Direction { get; set; }
        public int Status { get; set; }
        public string OrderId { get; set; }
        public ChatMessage[] ChatMessages { get; set; }
        public object PaymentRequest { get; set; }// check type
        public DateTime? PaymentDate { get; set; }
        public AccountRef Sender { get; set; }
        public AccountRef Beneficiary { get; set; }
        public AccountRef Account { get; set; }
        public MandateRef Mandate { get; set; }
    }
}
