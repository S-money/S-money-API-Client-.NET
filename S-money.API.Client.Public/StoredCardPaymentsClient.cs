using System.Collections.Generic;
using System.Threading.Tasks;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string storedcardpayments = "payins/storedcardpayments";

        public async Task<StoredCardPayment> PostStoredCardPayment(StoredCardPayment moneyIn, string userId = null)
        {
            var uri = CreateUri(userId, storedcardpayments);
            return await PostAsync(uri, moneyIn);
        }

        public async Task<IEnumerable<StoredCardPayment>> GetStoredCardPayments(string userId = null)
        {
            var uri = CreateUri(userId, storedcardpayments);
            return await GetAsync<IEnumerable<StoredCardPayment>>(uri);
        }

        public async Task<StoredCardPayment> GetStoredCardPayment(string orderId, string userId = null)
        {
            var uri = CreateUri(userId, storedcardpayments);
            return await GetAsync<StoredCardPayment>(uri + orderId);
        }
    }
}