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
        private const string cardpayments = "payins/cardpayments";

        public IEnumerable<CardPayment> GetCardPayments(string userIdentifier = null)
        {
            ReplaceRequestHeader("Accept", "application/vnd.s-money.v2+json");

            var uri = CreateUri(userIdentifier, cardpayments);
            return GetAsync<IEnumerable<CardPayment>>(uri);
        }

        public CardPayment GetCardPayment(string id, string userIdentifier = null)
        {
            ReplaceRequestHeader("Accept", "application/vnd.s-money.v2+json");

            var uri = CreateUri(userIdentifier, cardpayments);
            return GetAsync<CardPayment>(uri + id);
        }

        public CardPaymentCreated PostCardPayment(CardPaymentRequest cardPayment, string userIdentifier = null)
        {
            ReplaceRequestHeader("Accept", "application/vnd.s-money.v2+json");

            var uri = CreateUri(userIdentifier, cardpayments);

            var req = new HttpRequestMessage(HttpMethod.Post, uri);
            req.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(cardPayment));
            req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.s-money.v2+json");

            var response = this.SendAsync(req, new CancellationToken()).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new APIException(response);
            }
            return response.Content.ReadAsAsync<CardPaymentCreated>().Result;
        }
    }
}