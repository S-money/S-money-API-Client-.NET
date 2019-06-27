using System;

namespace Smoney.API.Client.Models.Users
{
    public class ChatMessage
    {
        public string Message { get; set; }
        public AccountRef Sender { get; set; }
        public DateTime Date { get; set; }
    }
}
