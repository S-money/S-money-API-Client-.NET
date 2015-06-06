using System.Collections.Generic;
using System.Net.Http;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string moneyouts = "moneyouts";
        private const string moneyoutsOneshot = moneyouts + "/oneshot";
        private const string moneyoutsReccuring = moneyouts + "/recurring";

        public BankTransfer PostRecurringMoneyOut(BankTransfer moneyOut, string userIdentifier = null)
        {
            var uri = CreateUri(userIdentifier, moneyoutsReccuring);
            return PostAsync(uri, moneyOut);
        }

        public BankTransfer GetMoneyOut(long id, string userId = null)
        {
            var uri = CreateUri(userId, moneyouts);
            return GetAsync<BankTransfer>(uri + id);
        }

        public IEnumerable<BankTransfer> GetMoneyOuts(string userId = null)
        {
            var uri = CreateUri(userId, moneyouts);
            return GetAsync<IEnumerable<BankTransfer>>(uri);
        }

        public BankTransfer PostOneShotMoneyOut(BankTransfer moneyOut, string userId = null)
        {
            var uri = CreateUri(userId, moneyoutsOneshot);
            return PostAsync(uri, moneyOut);
        }
    }
}