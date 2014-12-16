using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Configuration;
using System.Web.Http;
using Smoney.API.Client.Models;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        public Account GetSubAccount(string identifier, string userid = null)
        {
            var uri = userid == null ? BaseURL + "subaccounts/" : string.Format(BaseURL + "users/{0}/subaccounts/", userid);

            var response = this.GetAsync(uri + identifier).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var subaccount = response.Content.ReadAsAsync<Account>().Result;
            return subaccount;
        }

        public Account GetSubAccount(long id, string userid = null)
        {
            return GetSubAccount(id.ToString(), userid);
        }

        public IEnumerable<Account> GetSubAccounts(string userid = null)
        {
            var uri = userid == null ? BaseURL + "subaccounts" : string.Format(BaseURL + "users/{0}/subaccounts", userid);

            var response = this.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var subaccounts = response.Content.ReadAsAsync<IEnumerable<Account>>().Result;
            return subaccounts;
        }

        public Account PostSubAccount(Account subaccount, string userid = null)
        {
            var uri = userid == null ? BaseURL + "subaccounts" : string.Format(BaseURL + "users/{0}/subaccounts", userid);

            var response = this.PostAsJsonAsync(uri, subaccount).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var createdsubaccount = response.Content.ReadAsAsync<Account>().Result;
            return createdsubaccount;
        }

        public Account PutSubAccount(string identifier, Account subaccount, string userid = null)
        {
            var uri = userid == null ? BaseURL + "subaccounts/" : string.Format(BaseURL + "users/{0}/subaccounts/", userid);

            var response = this.PutAsJsonAsync(uri + identifier, subaccount).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var createdsubaccount = response.Content.ReadAsAsync<Account>().Result;
            return createdsubaccount;
        }

        public Account PutSubAccount(long id, Account subaccount, string userid = null)
        {
            return PutSubAccount(id.ToString(), subaccount, userid);
        }

        public void DeleteSubAccount(string identifier, string userid = null)
        {
            var uri = userid == null ? BaseURL + "subaccounts/" : string.Format(BaseURL + "users/{0}/subaccounts/", userid);

            var response = this.DeleteAsync(uri + identifier).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
        }

        public void DeleteSubAccount(long id, string userid = null)
        {
            DeleteSubAccount(id.ToString(), userid);
        }
    }
}
