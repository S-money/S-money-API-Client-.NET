using System.Collections.Generic;
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
        private const string banktransfers = payins + "/banktransfers";
        private const string banktransferreferences = banktransfers + "/references";

        #region MoneyOut

        public MoneyOut PostRecurringMoneyOut(MoneyOut moneyOut, string userIdentifier = null)
        {
            var uri = CreateUri(userIdentifier, moneyoutsReccuring);
            return PostAsync(uri, moneyOut);
        }

        public MoneyOut GetMoneyOut(long id, string userId = null)
        {
            var uri = CreateUri(userId, moneyouts);
            return GetAsync<MoneyOut>(uri + id);
        }

        public IEnumerable<MoneyOut> GetMoneyOuts(string userId = null)
        {
            var uri = CreateUri(userId, moneyouts);
            return GetAsync<IEnumerable<MoneyOut>>(uri);
        }

        public MoneyOut PostOneShotMoneyOut(MoneyOut moneyOut, string userId = null)
        {
            var uri = CreateUri(userId, moneyoutsOneshot);
            return PostAsync(uri, moneyOut);
        }

        #endregion

        #region MoneyIn DirectDebits

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

        public MoneyInDirectDebitResponse GetDirectDebit(string orderId, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return GetAsync<MoneyInDirectDebitResponse>(uri + orderId);
        }

        public MoneyInDirectDebitResponse GetDirectDebit(long id, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return GetAsync<MoneyInDirectDebitResponse>(uri + id);
        }

        public IEnumerable<MoneyInDirectDebitResponse> GetDirectDebits(string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return GetAsync<IEnumerable<MoneyInDirectDebitResponse>>(uri);
        }

        public MoneyInDirectDebitResponse PostDirectDebit(MoneyInDirectDebitRequest directdebit, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return PostAsync<MoneyInDirectDebitRequest, MoneyInDirectDebitResponse>(uri, directdebit);
        }

        public MoneyInDirectDebitResponse UpdateDirectDebit(long id, MoneyInDirectDebitRequest update, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return PutAsync<MoneyInDirectDebitRequest, MoneyInDirectDebitResponse>(uri + id, update);
        }

        public MoneyInDirectDebitResponse DeleteDirectDebit(long id, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            var update = new CancelRequest { Status = PaymentStatus.Canceled };
            return PutAsync<CancelRequest, MoneyInDirectDebitResponse>(uri + id, update);
        }

        #endregion

        #region MoneyIn Bank transfer

        public BankTransferReferenceResponse PostBankTransferReference(string userId)
        {
            var request = new BankTransferReferenceRequest { IsMine = true };
            return PostBankTransferReference(request, userId);
        }

        public BankTransferReferenceResponse PostBankTransferReference(BankTransferReferenceRequest request, string userId)
        {
            var uri = CreateUri(userId, banktransferreferences);
            return PostAsync<BankTransferReferenceRequest, BankTransferReferenceResponse>(uri, request);
        }


        public BankTransferReferenceResponse GetBankTransfertReference(string reference, string userId)
        {
            var uri = CreateUri(userId, banktransferreferences);
            return GetAsync<BankTransferReferenceResponse>(uri + reference);
        }

        public BankTransferReferenceResponse GetBankTransfertReference(long referenceId, string userId)
        {
            var uri = CreateUri(userId, banktransferreferences);
            return GetAsync<BankTransferReferenceResponse>(uri + referenceId);
        }

        public IEnumerable<BankTransferReferenceResponse> GetBankTransfertReferences(string userId)
        {
            var uri = CreateUri(userId, banktransferreferences);
            return GetAsync<IEnumerable<BankTransferReferenceResponse>>(uri);
        }

        public IEnumerable<MoneyInBankTransfer> GetBankTransfer(string reference, string userId)
        {
            var uri = CreateUri(userId, banktransfers);
            uri += string.Format("?reference={0}", reference);
            return GetAsync<IEnumerable<MoneyInBankTransfer>>(uri);
        }


        public MoneyInBankTransfer GetBankTransfer(long id, string userId)
        {
            var uri = CreateUri(userId, banktransfers);
            return GetAsync<MoneyInBankTransfer>(uri + id);
        }

        public IEnumerable<MoneyInBankTransfer> GetBankTransfers(string userId)
        {
            var uri = CreateUri(userId, banktransfers);
            return GetAsync<IEnumerable<MoneyInBankTransfer>>(uri);
        }

        #endregion
    }
}