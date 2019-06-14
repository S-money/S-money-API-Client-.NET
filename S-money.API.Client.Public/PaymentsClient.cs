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
        private const string payments = "payments";

        public Payment GetPayment(long id, string userId = null)
        {
            var uri = CreateUri(userId, payments);
            return GetAsync<Payment>(uri + id);
        }

        public IEnumerable<Payment> GetPayments(string userId = null)
        {
            var uri = CreateUri(userId, payments);
            return GetAsync<IEnumerable<Payment>>(uri);
        }

        // Not implemented
        //public int GetPaymentsCount(string userId = null)
        //{
        //    var uri = CreateUri(userId, payments);
        //    return GetCount(uri);
        //}

        public Payment PostPayment(Payment payment, string userId = null)
        {
            var uri = CreateUri(userId, payments);
            return PostAsync(uri, payment);
        }

        public IEnumerable<HistoryItem> GetHistoryItems(string userId = null, DateTime? startDate = null, DateTime? endDate = null, int perPage = 10000, int page = 1)
        {
            var path = string.Format("{0}{1}{2}{3}", "historyitems?perpage=", perPage, "&page=", page);
            if (startDate.HasValue)
            {
                path = string.Format("{0}{1}{2}", path, "&DateStart=", startDate.Value.ToString("O"));
            }
            if (endDate.HasValue)
            {
                path = string.Format("{0}{1}{2}", path, "&DateEnd=", endDate.Value.ToString("O"));
            }

            var uri = CreateUri(userId, path);
            return GetAsync<IEnumerable<HistoryItem>>(uri);
        }
    }
}