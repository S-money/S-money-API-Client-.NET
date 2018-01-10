using System.Collections.Generic;
using System.Threading.Tasks;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string bankaccounts = "bankaccounts";

        public async Task<BankAccount> GetBankAccount(long id, string userId = null)
        {
            var uri = CreateUri(userId, bankaccounts);
            return await GetAsync<BankAccount>(uri + id);
        }

        public async Task<List<BankAccount>> GetBankAccounts(string userId = null)
        {
            var uri = CreateUri(userId, bankaccounts);
            return await GetAsync<List<BankAccount>>(uri);
        }

        public async Task<BankAccount> PostBankAccount(BankAccount bankAccount, string userId = null)
        {
            var uri = CreateUri(userId, bankaccounts);
            return await PostAsync(uri, bankAccount);
        }

        public async Task<BankAccount> PutBankAccount(BankAccount bankAccount, string userId = null)
        {
            var uri = CreateUri(userId, bankaccounts);
            return await PostAsync(uri, bankAccount);
        }

        public void DeleteBankAccount(long id, string userId = null)
        {
            var uri = CreateUri(userId, bankaccounts);

            var response = this.DeleteAsync(uri + id).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new APIException(response);
            }
        }
    }
}