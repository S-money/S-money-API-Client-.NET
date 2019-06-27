namespace Smoney.API.Client.Models.Users
{
    public class AccountRef
    {
        public long? Id { get; set; }
        public string AppAccountId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Alias { get; set; }
        public string Href { get; set; }
    }
}