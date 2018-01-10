using System.Collections.Generic;
using System.Threading.Tasks;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string payments = "payments";

        public async Task<Payment> GetPayment(long id, string userId = null)
        {
            var uri = CreateUri(userId, payments);
            return await GetAsync<Payment>(uri + id);
        }

        public async Task<IEnumerable<Payment>> GetPayments(string userId = null)
        {
            var uri = CreateUri(userId, payments);
            return await GetAsync<IEnumerable<Payment>>(uri);
        }

        // Not implemented
        //public int GetPaymentsCount(string userId = null)
        //{
        //    var uri = CreateUri(userId, payments);
        //    return GetCount(uri);
        //}

        public async Task<Payment> PostPayment(Payment payment, string userId = null)
        {
            var uri = CreateUri(userId, payments);
            return await PostAsync(uri, payment);
        }
    }
}