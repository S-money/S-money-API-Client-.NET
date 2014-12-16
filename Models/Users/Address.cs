using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smoney.API.Client.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Smoney.API.Client.Models.Users
{
    public class Address
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Country Country { get; set; }
    }
}
