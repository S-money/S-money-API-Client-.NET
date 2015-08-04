using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoney.API.Client.Models.Operations
{
    public class Fee
    {
        public long? Amount { get; set; }
        public decimal? VAT { get; set; }
        public long? AmountWithVAT { get; set; }
        public int Status { get; set; }
    }
}