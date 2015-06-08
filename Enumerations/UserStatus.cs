using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoney.API.Client.Enumerations
{
    public enum UserStatus
    {
        Unconfirmed = 0,
        Ok = 1,
        Frozen = 2,
        OnTheFly = 3,
        PendingClosing = 4,
        Closed = 5,
        WaitingKYC = 6
    }
}