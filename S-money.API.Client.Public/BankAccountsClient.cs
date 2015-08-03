using System.Collections.Generic;
using System.Net.Http;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string bankaccounts = "bankaccounts";

        public BankAccount GetBankAccount(long id, string userId = null)
        {
            var uri = CreateUri(userId, bankaccounts);
            return GetAsync<BankAccount>(uri + id);
        }

        public List<BankAccount> GetBankAccounts(string userId = null)
        {
            var uri = CreateUri(userId, bankaccounts);
            return GetAsync<List<BankAccount>>(uri);
        }

        public BankAccount PostBankAccount(BankAccount bankAccount, string userId = null)
        {
            var uri = CreateUri(userId, bankaccounts);
            return PostAsync(uri, bankAccount);
        }

        public BankAccount PutBankAccount(BankAccount bankAccount, string userId = null)
        {
            var uri = CreateUri(userId, bankaccounts);
            return PostAsync(uri, bankAccount);
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