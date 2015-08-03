using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoney.API.Client.Enumerations
{
    public enum CardPaymentStatus
    {
        Pending = 0,
        Succeeded = 1,
        Refund = 2,
        Failed = 3,
        WaitingValidation = 4,
        Canceled = 5,
        InProgress = 6
    }
}