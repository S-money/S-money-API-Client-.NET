using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smoney.API.Client.Models.Users
{
    public class Card
    {
        public long Id { get; set; }
        public string AppCardId { get; set; }
        public string Hint { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
