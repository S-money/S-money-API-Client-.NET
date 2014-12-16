using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoney.API.Client.Models.Users
{
    public class AccountRef
    {
        public long? Id { get; set; }
        public string AppAccountId { get; set; }
        public string DisplayName { get; set; }
        public string Href { get; set; }
    }
}
