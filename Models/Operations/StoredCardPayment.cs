using System;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Models.Operations
{
    public class StoredCardPayment
    {
        public long? Id { get; set; }
        public string OrderId { get; set; }
        public AccountRef AccountId { get; set; }
        public CardRef Card {get;set;}
        public long? Amount { get; set; }
        public bool? IsMine { get; set; }
        public DateTime OperationDate { get; set; }
        public Fee Fee { get; set; }
    }
}
