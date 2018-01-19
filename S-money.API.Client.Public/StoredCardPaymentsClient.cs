using System.Collections.Generic;
using System.Net.Http;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string storedcardpayments = "payins/storedcardpayments";

        public StoredCardPayment PostStoredCardPayment(StoredCardPayment moneyIn, string userId = null)
        {
            var uri = CreateUri(userId, storedcardpayments);
            return PostAsync(uri, moneyIn);
        }

        public IEnumerable<StoredCardPayment> GetStoredCardPayments(string userId = null)
        {
            var uri = CreateUri(userId, storedcardpayments);
            return GetAsync<IEnumerable<StoredCardPayment>>(uri);
        }

        public StoredCardPayment GetStoredCardPayment(string orderId, string userId = null)
        {
            var uri = CreateUri(userId, storedcardpayments);
            return GetAsync<StoredCardPayment>(uri + orderId);
        }
    }
}