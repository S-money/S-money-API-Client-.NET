using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Smoney.API.Client.Models.Attachments
{
    public class FileAttachment
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public Stream Content { get; set; }
    }
}
