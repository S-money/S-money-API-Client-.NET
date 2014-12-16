using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoney.API.Client.Models.Users
{
    public class CardRegistration
    {
        public Card Card { get; set; }
        public int Status { get; set; } // TODO : enum (0 = En attente, 1 = Réalisé, 2 = Echoué)
        public string UrlReturn { get; set; }
        public string AvailableCards { get; set; }
        public int ErrorCode { get; set; }
        public SPExtraResultCodes ExtraResults { get; set; }
        public string Href { get; set; } // TODO Created DTO
    }

    public class SPExtraResultCodes
    {
        public int? RiskControlResult { get; set; }
        public int? BankAuthResult { get; set; }
        public int? ThreedsResult { get; set; }
        public bool WarrantyResult { get; set; }
    }
}
