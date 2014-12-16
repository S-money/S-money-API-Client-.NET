using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Configuration;
using System.Web.Http;
using Smoney.API.Client.Models;
using Smoney.API.Client.Models.Operations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        public BankAccount PostBankAccount(BankAccount bankAccount, string userid = null)
        {
            var uri = userid == null ? BaseURL + "bankaccounts" : string.Format(BaseURL + "users/{0}/bankaccounts", userid);

            var response = this.PostAsJsonAsync(uri, bankAccount).Result;
            if (response.IsSuccessStatusCode)
            {
                var createdBankAccount = response.Content.ReadAsAsync<BankAccount>().Result;
                return createdBankAccount;
            }
            else
                throw new APIException(response);
        }

        public BankAccount PutBankAccount(BankAccount bankAccount, string userid = null)
        {
            var uri = userid == null ? BaseURL + "bankaccounts" : string.Format(BaseURL + "users/{0}/bankaccounts", userid);

            var response = this.PutAsJsonAsync(uri, bankAccount).Result;
            if (response.IsSuccessStatusCode)
            {
                var updatedBankAccount = response.Content.ReadAsAsync<BankAccount>().Result;
                return updatedBankAccount;
            }
            else
                throw new APIException(response);
        }

        public void DeleteBankAccount(long id, string userid = null)
        {
            var uri = userid == null ? string.Format(BaseURL + "bankaccounts/{0}", id) : string.Format(BaseURL + "users/{0}/bankaccounts/{1}", userid, id);

            var response = this.DeleteAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
                throw new APIException(response);
        }

        public BankAccount GetBankAccount(long id, string userid = null)
        {
            var uri = userid == null ? string.Format(BaseURL + "bankaccounts/{0}", id) : string.Format(BaseURL + "users/{0}/bankaccounts/{1}", userid, id);

            var response = this.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var ba = response.Content.ReadAsAsync<BankAccount>().Result;
                return ba;
            }
            else
                throw new APIException(response);
        }

        public List<BankAccount> GetBankAccounts(string userid = null)
        {
            var uri = userid == null ? BaseURL + "bankaccounts" : string.Format(BaseURL + "users/{0}/bankaccounts", userid);

            var response = this.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var baList = response.Content.ReadAsAsync<List<BankAccount>>().Result;
                return baList;
            }
            else
                throw new APIException(response);
        }
    }
}
