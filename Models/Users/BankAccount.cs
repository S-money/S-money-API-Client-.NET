using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoney.API.Client.Models.Users
{
    public class BankAccount
    {
        public long? Id { get; set; }
        public string DisplayName { get; set; }
        public string Bic { get; set; }
        public string Iban { get; set; }
        public bool? IsMine { get; set; }
    }
}
