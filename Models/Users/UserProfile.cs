using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smoney.API.Client.Enumerations;

namespace Smoney.API.Client.Models.Users
{
    public class UserProfile
    {
        public Civility? Civility { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Alias { get; set; }
        public PhotoRef Photo { get; set; }
    }
}
