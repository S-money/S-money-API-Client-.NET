namespace Smoney.API.Client.Models.Attachments
{
    public class Attachment
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public int Size { get; set; }
        public string Href { get; set; }
    }
}
