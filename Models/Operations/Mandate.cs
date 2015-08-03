using System;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Models.Operations
{
    public class Mandate
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public BankAccountRef BankAccount { get; set; }
        public ManadateStatus Status { get; set; }
        
    }

    public class MandateRequest : Mandate
    {
        public string UrlReturn { get; set; }
    }

    public class MandateResponse : Mandate
    {
        public string Href { get; set; }
    }
}