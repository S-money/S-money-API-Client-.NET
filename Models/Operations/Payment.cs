using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Models.Operations
{
    public class Payment
    {
        public long Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public long? Amount { get; set; }
        public Fee Fee { get; set; }
        public AccountRef Beneficiary { get; set; }
        public AccountRef Sender { get; set; }
        public string Message { get; set; }
    }
}
