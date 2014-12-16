using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoney.API.Client.Models.Users
{
    public class CardRef
    {
        public long? Id { get; set; }
        public string AppCardId { get; set; }
        public string href { get; set; }
    }
}
