using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Attachments;
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

        public async Task<MoneyOut> PostRecurringMoneyOut(MoneyOut moneyOut, string userIdentifier = null)
        {
            var uri = CreateUri(userIdentifier, moneyoutsReccuring);
            return await PostAsync(uri, moneyOut);
        }

        public async Task<MoneyOut> GetMoneyOut(long id, string userId = null)
        {
            var uri = CreateUri(userId, moneyouts);
            return await GetAsync<MoneyOut>(uri + id);
        }

        public async Task<IEnumerable<MoneyOut>> GetMoneyOuts(string userId = null)
        {
            var uri = CreateUri(userId, moneyouts);
            return await GetAsync<IEnumerable<MoneyOut>>(uri);
        }

        public async Task<MoneyOut> PostOneShotMoneyOut(MoneyOut moneyOut, string userId = null)
        {
            var uri = CreateUri(userId, moneyoutsOneshot);
            return await PostAsync(uri, moneyOut);
        }

        #endregion

        #region MoneyIn DirectDebits

        public async Task<MandateResponse> GetMandate(long id, string userId = null)
        {
            var uri = CreateUri(userId, mandates);
            return await GetAsync<MandateResponse>(uri + id);
        }

        public async Task<IEnumerable<MandateResponse>> GetMandates(string userId = null)
        {
            var uri = CreateUri(userId, mandates);
            return await GetAsync<IEnumerable<MandateResponse>>(uri);
        }

        public async Task<MandateResponse> PostMandate(MandateRequest mandate, string userId = null)
        {
            var uri = CreateUri(userId, mandates);
            return await PostAsync<MandateRequest, MandateResponse>(uri, mandate);
        }

        public async Task<bool> PostMandateDocument(FileAttachment file, int mandateId, string userId = null)
        {
            var uri = userId == null
                ? string.Format("{0}mandates/{1}/attachments", BaseURL, mandateId)
                : string.Format("{0}users/{1}/mandates/{2}/attachments", BaseURL, userId, mandateId);
            var multipart = new MultipartFormDataContent();

            file.Content.Position = 0;
            var streamContent = new StreamContent(file.Content);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.Type);
            multipart.Add(streamContent, file.Name, file.Name);

            var response = await base.PostAsync(uri, multipart);
            return response.IsSuccessStatusCode;
        }

        public async Task<MoneyInDirectDebitResponse> GetDirectDebit(string orderId, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return await GetAsync<MoneyInDirectDebitResponse>(uri + orderId);
        }

        public async Task<MoneyInDirectDebitResponse> GetDirectDebit(long id, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return await GetAsync<MoneyInDirectDebitResponse>(uri + id);
        }

        public async Task<IEnumerable<MoneyInDirectDebitResponse>> GetDirectDebits(string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return await GetAsync<IEnumerable<MoneyInDirectDebitResponse>>(uri);
        }

        public async Task<MoneyInDirectDebitResponse> PostDirectDebit(MoneyInDirectDebitRequest directdebit, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return await PostAsync<MoneyInDirectDebitRequest, MoneyInDirectDebitResponse>(uri, directdebit);
        }

        public async Task<MoneyInDirectDebitResponse> UpdateDirectDebit(long id, MoneyInDirectDebitRequest update, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            return await PutAsync<MoneyInDirectDebitRequest, MoneyInDirectDebitResponse>(uri + id, update);
        }

        public async Task<MoneyInDirectDebitResponse> DeleteDirectDebit(long id, string userId = null)
        {
            var uri = CreateUri(userId, directdebits);
            var update = new CancelRequest { Status = PaymentStatus.Canceled };
            return await PutAsync<CancelRequest, MoneyInDirectDebitResponse>(uri + id, update);
        }

        #endregion

        #region MoneyIn Bank transfer

        public async Task<BankTransferReferenceResponse> PostBankTransferReference(string userId)
        {
            var request = new BankTransferReferenceRequest { IsMine = true };
            return await PostBankTransferReference(request, userId);
        }

        public async Task<BankTransferReferenceResponse> PostBankTransferReference(BankTransferReferenceRequest request, string userId)
        {
            var uri = CreateUri(userId, banktransferreferences);
            return await PostAsync<BankTransferReferenceRequest, BankTransferReferenceResponse>(uri, request);
        }


        public async Task<BankTransferReferenceResponse> GetBankTransfertReference(string reference, string userId)
        {
            var uri = CreateUri(userId, banktransferreferences);
            return await GetAsync<BankTransferReferenceResponse>(uri + reference);
        }

        public async Task<BankTransferReferenceResponse> GetBankTransfertReference(long referenceId, string userId)
        {
            var uri = CreateUri(userId, banktransferreferences);
            return await GetAsync<BankTransferReferenceResponse>(uri + referenceId);
        }

        public async Task<IEnumerable<BankTransferReferenceResponse>> GetBankTransfertReferences(string userId)
        {
            var uri = CreateUri(userId, banktransferreferences);
            return await GetAsync<IEnumerable<BankTransferReferenceResponse>>(uri);
        }

        public async Task<IEnumerable<MoneyInBankTransfer>> GetBankTransfer(string reference, string userId)
        {
            var uri = CreateUri(userId, banktransfers);
            uri += string.Format("?reference={0}", reference);
            return await GetAsync<IEnumerable<MoneyInBankTransfer>>(uri);
        }


        public async Task<MoneyInBankTransfer> GetBankTransfer(long id, string userId)
        {
            var uri = CreateUri(userId, banktransfers);
            return await GetAsync<MoneyInBankTransfer>(uri + id);
        }

        public async Task<IEnumerable<MoneyInBankTransfer>> GetBankTransfers(string userId)
        {
            var uri = CreateUri(userId, banktransfers);
            return await GetAsync<IEnumerable<MoneyInBankTransfer>>(uri);
        }

        #endregion
    }
}