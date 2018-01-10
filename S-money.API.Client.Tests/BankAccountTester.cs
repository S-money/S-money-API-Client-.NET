using System.Linq;
using System.Threading.Tasks;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Tests
{
    public class BankAccountTester
    {
        private const string BIC = "CMCIFR2A";
        private const string IBAN = "FR7613106005002000743520962";

        public static async Task<BankAccountRef> CreateAccount(APIClient client, string userId)
        {
            var accounts = await client.GetBankAccounts(userId);
            var result = accounts.FirstOrDefault(e => e.Iban == IBAN);
            if (result == null)
            {
                var account = new BankAccount
                              {
                                  Bic = BIC,
                                  Iban = IBAN,
                                  DisplayName = "Smoney default account",
                                  IsMine = true
                              };
                result = await client.PostBankAccount(account, userId);
            }
            return new BankAccountRef
                   {
                       Id = result.Id.Value
                   };
        }
    }
}