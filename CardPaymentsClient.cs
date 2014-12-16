using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using Smoney.API.Client.Models.Operations;
using System.Net;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        public IEnumerable<CardPayment> GetCardPayments(string userIdentifier = null)
        {
            ReplaceRequestHeader("Accept", "application/vnd.s-money.v2+json");

            var uri = userIdentifier == null ? BaseURL + "payins" : string.Format(BaseURL + "users/{0}/payins", userIdentifier);
            var response = this.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            return response.Content.ReadAsAsync<IEnumerable<CardPayment>>().Result;
        }

        public CardPayment GetCardPayment(string id, string userIdentifier = null)
        {
            ReplaceRequestHeader("Accept", "application/vnd.s-money.v2+json");

            var uri = userIdentifier == null ? string.Format(BaseURL + "payins/cardpayments/{0}", id) : string.Format(BaseURL + "users/{0}/payins/cardpayments/{1}", userIdentifier, id);
            var response = this.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            return response.Content.ReadAsAsync<CardPayment>().Result;
        }

        public CardPaymentCreated PostCardPayment(CardPaymentRequest cardPayment, string userIdentifier = null)
        {
            ReplaceRequestHeader("Accept", "application/vnd.s-money.v2+json");

            var uri = userIdentifier == null ? string.Format(BaseURL + "payins/cardpayments") : string.Format(BaseURL + "users/{0}/payins/cardpayments", userIdentifier);

            var req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(cardPayment));
            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.s-money.v2+json");

            var response = this.SendAsync(req, new CancellationToken()).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            return response.Content.ReadAsAsync<CardPaymentCreated>().Result;
        }
    }
}
