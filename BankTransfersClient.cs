using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Configuration;
using System.Web.Http;
using Smoney.API.Client.Models;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        public BankTransfer PostOneShotMoneyOut(BankTransfer moneyOut, string userIdentifier = null)
        {
            var uri = userIdentifier == null ? BaseURL + "moneyouts/oneshot" : string.Format(BaseURL + "users/{0}/moneyouts/oneshot", userIdentifier);
            var response = this.PostAsJsonAsync(uri, moneyOut).Result;
            if (response.IsSuccessStatusCode)
            {
                var createdMoneyOut = response.Content.ReadAsAsync<BankTransfer>().Result;
                return createdMoneyOut;
            }
            else
                throw new APIException(response);
        }

        public BankTransfer GetMoneyOut(long id, string userIdentifier = null)
        {
            var uri = userIdentifier == null ? string.Format(BaseURL + "moneyouts/{0}", id) : string.Format(BaseURL + "users/{0}/moneyouts/{1}", userIdentifier, id);
            var response = this.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var moneyOut = response.Content.ReadAsAsync<BankTransfer>().Result;
                return moneyOut;
            }
            else
                throw new APIException(response);
        }

        public IEnumerable<BankTransfer> GetMoneyOuts(string userIdentifier = null)
        {
            var uri = userIdentifier == null ? BaseURL + "moneyouts" : string.Format(BaseURL + "users/{0}/moneyouts", userIdentifier);
            var response = this.GetAsync(BaseURL + "moneyouts").Result;
            if (response.IsSuccessStatusCode)
            {
                var moneyouts = response.Content.ReadAsAsync<IEnumerable<BankTransfer>>().Result;
                return moneyouts;
            }
            else
                throw new APIException(response);
        }
    }
}
