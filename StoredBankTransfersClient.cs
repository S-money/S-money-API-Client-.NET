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
        public BankTransfer PostRecurringMoneyOut(BankTransfer moneyOut, string userIdentifier = null)
        {
            var uri = userIdentifier == null ? BaseURL + "moneyouts/recurring" : string.Format(BaseURL + "users/{0}/moneyouts/recurring", userIdentifier);
            var response = this.PostAsJsonAsync(uri, moneyOut).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var createdMoneyOut = response.Content.ReadAsAsync<BankTransfer>().Result;
            return createdMoneyOut;
        }
    }
}
