using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string subaccounts = "subaccounts";

        public async Task<Account> GetSubAccount(string identifier, string userid = null)
        {
            var uri = CreateUri(userid, subaccounts);
            return await GetAsync<Account>(uri + identifier);
        }

        public async Task<Account> GetSubAccount(long id, string userid = null)
        {
            return await GetSubAccount(id.ToString(), userid);
        }

        public async Task<IEnumerable<Account>> GetSubAccounts(string userid = null)
        {
            var uri = CreateUri(userid, subaccounts);
            return await GetAsync<IEnumerable<Account>>(uri);
        }

        public async Task<Account> PostSubAccount(Account subaccount, string userid = null)
        {
            var uri = CreateUri(userid, subaccounts);
            return await PostAsync(uri, subaccount);
        }

        public async Task<Account> PutSubAccount(string identifier, Account subaccount, string userid = null)
        {
            var uri = CreateUri(userid, subaccounts);

            var response = this.PutAsJsonAsync(uri + identifier, subaccount).Result;
            return await HandleResult<Account>(response);
        }

        public async Task<Account> PutSubAccount(long id, Account subaccount, string userid = null)
        {
            return await PutSubAccount(id.ToString(), subaccount, userid);
        }

        public async Task DeleteSubAccount(string identifier, string userid = null)
        {
            var uri = CreateUri(userid, subaccounts);

            var response = await this.DeleteAsync(uri + identifier);
            if (!response.IsSuccessStatusCode)
            {
                throw new APIException(response);
            }
        }

        public async Task DeleteSubAccount(long id, string userid = null)
        {
            await DeleteSubAccount(id.ToString(), userid);
        }
    }
}