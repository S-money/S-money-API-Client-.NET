using System.Collections.Generic;
using System.Net.Http;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        public StoredCardPayment PostStoredCardPayment(StoredCardPayment moneyIn, string userIdentifier = null)
        {
            var uri = userIdentifier == null ? BaseURL + "payins/storedcardpayments" : string.Format(BaseURL + "users/{0}/payins/storedcardpayments", userIdentifier);

            var response = this.PostAsJsonAsync<StoredCardPayment>(uri, moneyIn).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var responseMoneyIn = response.Content.ReadAsAsync<StoredCardPayment>().Result;
            return responseMoneyIn;
        }

        public IEnumerable<StoredCardPayment> GetStoredCardPayments(string userIdentifier = null)
        {
            var uri = userIdentifier == null ? BaseURL + "payins/storedcardpayments" : string.Format(BaseURL + "users/{0}/payins/storedcardpayments", userIdentifier);

            var response = this.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var moneyIns = response.Content.ReadAsAsync<IEnumerable<StoredCardPayment>>().Result;
            return moneyIns;
        }

        public StoredCardPayment GetStoredCardPayment(string orderid, string userIdentifier = null)
        {
            var uri = userIdentifier == null ? BaseURL + string.Format("payins/storedcardpayments/{0}", orderid) : string.Format(BaseURL + "users/{0}/payins/storedcardpayments/{1}", userIdentifier, orderid);

            var response = this.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var moneyIn = response.Content.ReadAsAsync<StoredCardPayment>().Result;
            return moneyIn;
        }
    }
}
