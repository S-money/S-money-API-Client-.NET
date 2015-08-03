using System;
using System.Collections.Generic;
using System.Net.Http;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string mandates = "mandates";
        private const string moneyouts = "moneyouts";
        private const string moneyoutsOneshot = moneyouts + "/oneshot";
        private const string moneyoutsReccuring = moneyouts + "/recurring";
        private const string payins = "payins";
        private const string directdebits = payins + "/directdebits";

        #region MoneyOut

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

        #endregion

        #region MoneyIn

        public MandateResponse GetMandate(long id, string userId = null)
        {
            var uri = CreateUri(userId, mandates);
            return GetAsync<MandateResponse>(uri + id);
        }

        public IEnumerable<MandateResponse> GetMandates(string userId = null)
        {
            var uri = CreateUri(userId, mandates);
            return GetAsync<IEnumerable<MandateResponse>>(uri);
        }

        public MandateResponse PostMandate(MandateRequest mandate, string userId = null)
        {
            var uri = CreateUri(userId, mandates);
            return PostAsync<MandateRequest, MandateResponse>(uri, mandate);
        }

        public DirectDebitResponse GetDirectDebit(long id, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return GetAsync<DirectDebitResponse>(uri + id);
        }

        public IEnumerable<DirectDebitResponse> GetDirectDebits(string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return GetAsync<IEnumerable<DirectDebitResponse>>(uri);
        }

        public DirectDebitResponse PostDirectDebit(DirectDebitRequest directdebit, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return PostAsync<DirectDebitRequest, DirectDebitResponse>(uri, directdebit);
        }
        public DirectDebitResponse UpdateDirectDebit(long id, DirectDebitRequest update, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return PutAsync<DirectDebitRequest, DirectDebitResponse>(uri + id, update);
        }

        public DirectDebitResponse DeleteDirectDebit(long id, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            var update = new CancelRequest { Status = CardPaymentStatus.Canceled };
            return PutAsync<CancelRequest, DirectDebitResponse>(uri + id, update);
        }

        #endregion

       
    }
}