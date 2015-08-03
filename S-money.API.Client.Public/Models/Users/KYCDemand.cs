using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Attachments;

namespace Smoney.API.Client.Models.Users
{
    public class KYCDemand
    {
        public long? Id { get; set; }
        public DateTime RequestDate { get; set; }
        public UserDemandStatus Status { get; set; }
        public List<FileAttachmentRef> VoucherCopies { get; set; }
    }
}