using System.Collections.Generic;
using System.Net.Http;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string subaccounts = "subaccounts";

        public Account GetSubAccount(string identifier, string userid = null)
        {
            var uri = CreateUri(userid, subaccounts);
            return GetAsync<Account>(uri + identifier);
        }

        public Account GetSubAccount(long id, string userid = null)
        {
            return GetSubAccount(id.ToString(), userid);
        }

        public IEnumerable<Account> GetSubAccounts(string userid = null)
        {
            var uri = CreateUri(userid, subaccounts);
            return GetAsync<IEnumerable<Account>>(uri);
        }

        public Account PostSubAccount(Account subaccount, string userid = null)
        {
            var uri = CreateUri(userid, subaccounts);
            return PostAsync(uri, subaccount);
        }

        public Account PutSubAccount(string identifier, Account subaccount, string userid = null)
        {
            var uri = CreateUri(userid, subaccounts);

            var response = this.PutAsJsonAsync(uri + identifier, subaccount).Result;
            return HandleResult<Account>(response);
        }

        public Account PutSubAccount(long id, Account subaccount, string userid = null)
        {
            return PutSubAccount(id.ToString(), subaccount, userid);
        }

        public void DeleteSubAccount(string identifier, string userid = null)
        {
            var uri = CreateUri(userid, subaccounts);

            var response = this.DeleteAsync(uri + identifier).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new APIException(response);
            }
        }

        public void DeleteSubAccount(long id, string userid = null)
        {
            DeleteSubAccount(id.ToString(), userid);
        }
    }
}