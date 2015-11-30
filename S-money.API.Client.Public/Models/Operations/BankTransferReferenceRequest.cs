using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Models.Operations
{
    public class BankTransferReferenceRequest
    {
        public AccountRef Beneficiary { get; set; }
        public bool IsMine { get; set; }
        
    }

    public class BankTransferReferenceResponse : BankTransferReferenceRequest
    {
        public long Id { get; set; }
        public string Reference { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}