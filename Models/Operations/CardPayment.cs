using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Models.Operations
{
    // Note : On fait que la V2
    public class CardPayment
    {
        public long Id { get; set; }
        public long Amount { get; set; }
        public CardPaymentStatus Status { get; set; }
        public AccountRef Beneficiary { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Message { get; set; }
        public string OrderId { get; set; }
        // TODO : REFUNDS public List<PayInRefundRef> Refunds { get; set; }
        public int ErrorCode { get; set; }
        public SPExtraResultCodes ExtraResults { get; set; }
        public long? Fee { get; set; }
        public CardRef Card { get; set; }
        public bool IsMine { get; set; }
        public List<Schedule> Schedules { get; set; }
    }

    public class CardPaymentRequest : CardPayment
    {
        public string UrlReturn { get; set; }
        public string UrlCallback { get; set; }
        public string AvailableCards { get; set; }
        public IPaymentSchedule PaymentSchedule { get; set; }
    }

    public class CardPaymentCreated : CardPayment
    {
        public string Href { get; set; }
    }
}