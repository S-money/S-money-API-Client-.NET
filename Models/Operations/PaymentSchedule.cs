using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smoney.API.Client.Enumerations;

namespace Smoney.API.Client.Models.Operations
{
    public interface IPaymentSchedule
    {
    }

    public class AutomaticPaymentSchedule : IPaymentSchedule
    {
        public long FirstAmount { get; set; }
        public long? FirstFee { get; set; }
        public int Count { get; set; }
        public int Period { get; set; }
    }

    public class DetailedPaymentSchedule : List<Schedule>, IPaymentSchedule
    {
    }

    public class Schedule
    {
        public int SequenceNumber { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public CardPaymentStatus Status { get; set; } // TODO : enum
        public long? Fee { get; set; }
        // TODO : REFUND public List<IRefund> Refunds { get; set; }
    }
}