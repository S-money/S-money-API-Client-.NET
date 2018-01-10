using System.Collections.Generic;
using Smoney.API.Client.Models.Operations;
using System.Threading.Tasks;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string cardpayments = "payins/cardpayments";

        public async Task<IEnumerable<CardPayment>> GetCardPayments(string userIdentifier = null, int? pageNumber = null)
        {
            UseV2();

            var uri = CreateUri(userIdentifier, cardpayments, pageNumber);
            return await GetAsync<IEnumerable<CardPayment>>(uri);
        }

        public async Task<int> GetCardPaymentsCount(string userIdentifier = null)
        {
            UseV2();

            var uri = CreateUri(userIdentifier, cardpayments);
            return await GetCount(uri);
        }

        public async Task<CardPayment> GetCardPayment(string id, string userIdentifier = null)
        {
            UseV2();

            var uri = CreateUri(userIdentifier, cardpayments);
            return await GetAsync<CardPayment>(uri + id);
        }

        public async Task<CardPaymentCreated> PostCardPayment(CardPaymentRequest cardPayment, string userIdentifier = null)
        {
            UseV2();

            var uri = CreateUri(userIdentifier, cardpayments);
            return await PostAsync<CardPaymentRequest, CardPaymentCreated>(uri, cardPayment);
        }
    }
}