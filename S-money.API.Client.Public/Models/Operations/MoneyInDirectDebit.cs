using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Models.Operations
{
    public class MoneyInDirectDebit
    {
        public string OrderId { get; set; }

        [XmlIgnore]
        public DateTime? PaymentDate { get; set; }

        [XmlElement(ElementName = "PaymentDate")]
        public string XmlTime
        {
            get => PaymentDate.HasValue ? XmlConvert.ToString(PaymentDate.Value, XmlDateTimeSerializationMode.RoundtripKind) : null;
            set => PaymentDate = DateTimeOffset.Parse(value).DateTime;
        }

        public long Amount { get; set; }
        public long? Fee { get; set; }

        public AccountRef Beneficiary { get; set; }
        public bool IsMine { get; set; }

        public string Message { get; set; }

        public MandateRef Mandate { get; set; }

    }

    public class MoneyInDirectDebitRequest : MoneyInDirectDebit
    {

    }
    public class MoneyInDirectDebitResponse : MoneyInDirectDebit
    {
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }
        public PaymentStatus Status { get; set; }
        public List<Schedule> Schedules { get; set; }
    }

    public class CancelRequest
    {
        public PaymentStatus Status { get; set; }
    }
}