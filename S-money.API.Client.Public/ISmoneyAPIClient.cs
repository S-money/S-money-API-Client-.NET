using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Smoney.API.Client.Models.Attachments;
using Smoney.API.Client.Models.Operations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public interface ISmoneyApiClient : IDisposable
    {
        string BaseURL { get; }
        Uri BaseAddress { get; }
        int DefaultPageSize { get; }

        Task<Card> GetCard(string appcardid, string userId = null);
        CardRegistration GetRegistration(string appcardid, string userId = null);
        Task<IEnumerable<Card>> GetCards(string userId = null);
        Task<CardRegistration> GetCardRegistration(string appcardid, string userId = null);
        Task<IEnumerable<CardRegistration>> GetCardsRegistration(string userId = null);
        Task<CardRegistration> PostCard(CardRegistration cardRegistration, string userId = null);
        HttpResponseMessage DeleteCard(string appcardid, string userId = null);
        Task<Payment> GetPayment(long id, string userId = null);
        Task<IEnumerable<Payment>> GetPayments(string userId = null);
        Task<Payment> PostPayment(Payment payment, string userId = null);
        Task<User> GetUser(long id);
        Task<User> GetUser(string identifier);
        Task<IEnumerable<User>> GetUsers(int? pageNumber = null);
        Task<int> GetUsersCount();
        Task<List<User>> GetAllUsers();
        Task<IEnumerable<User>> SearchUser(string firstName = null, string lastName = null, string email = null);
        Task<User> PostUser(User user);
        Task<User> PutUser(string identifier, User user);
        Task<User> PutUser(User user);
        Task<User> CloseUser(string identifier);
        Task<User> CloseUser();
        Task<Account> GetSubAccount(string identifier, string userid = null);
        Task<Account> GetSubAccount(long id, string userid = null);
        Task<IEnumerable<Account>> GetSubAccounts(string userid = null);
        Task<Account> PostSubAccount(Account subaccount, string userid = null);
        Task<Account> PutSubAccount(string identifier, Account subaccount, string userid = null);
        Task<Account> PutSubAccount(long id, Account subaccount, string userid = null);
        Task DeleteSubAccount(string identifier, string userid = null);
        Task DeleteSubAccount(long id, string userid = null);
        Task<StoredCardPayment> PostStoredCardPayment(StoredCardPayment moneyIn, string userId = null);
        Task<IEnumerable<StoredCardPayment>> GetStoredCardPayments(string userId = null);
        Task<StoredCardPayment> GetStoredCardPayment(string orderId, string userId = null);
        Task<KYCDemand> GetKYC(long id, string userId = null);
        Task<IEnumerable<KYCDemand>> GetKYCs(string userId = null);
        Task<KYCDemand> PostKYC(List<FileAttachment> files, string userId = null);
        Task<MoneyOut> PostRecurringMoneyOut(MoneyOut moneyOut, string userIdentifier = null);
        Task<MoneyOut> GetMoneyOut(long id, string userId = null);
        Task<IEnumerable<MoneyOut>> GetMoneyOuts(string userId = null);
        Task<MoneyOut> PostOneShotMoneyOut(MoneyOut moneyOut, string userId = null);
        Task<MandateResponse> GetMandate(long id, string userId = null);
        Task<IEnumerable<MandateResponse>> GetMandates(string userId = null);
        Task<MandateResponse> PostMandate(MandateRequest mandate, string userId = null);
        Task<bool> PostMandateDocument(FileAttachment file, int mandateId, string userId = null);
        Task<MoneyInDirectDebitResponse> GetDirectDebit(string orderId, string userId = null);
        Task<MoneyInDirectDebitResponse> GetDirectDebit(long id, string userId = null);
        Task<IEnumerable<MoneyInDirectDebitResponse>> GetDirectDebits(string userId = null);
        Task<MoneyInDirectDebitResponse> PostDirectDebit(MoneyInDirectDebitRequest directdebit, string userId = null);
        Task<MoneyInDirectDebitResponse> UpdateDirectDebit(long id, MoneyInDirectDebitRequest update, string userId = null);
        Task<MoneyInDirectDebitResponse> DeleteDirectDebit(long id, string userId = null);
        Task<BankTransferReferenceResponse> PostBankTransferReference(string userId);
        Task<BankTransferReferenceResponse> PostBankTransferReference(BankTransferReferenceRequest request, string userId);
        Task<BankTransferReferenceResponse> GetBankTransfertReference(string reference, string userId);
        Task<BankTransferReferenceResponse> GetBankTransfertReference(long referenceId, string userId);
        Task<IEnumerable<BankTransferReferenceResponse>> GetBankTransfertReferences(string userId);
        Task<IEnumerable<MoneyInBankTransfer>> GetBankTransfer(string reference, string userId);
        Task<MoneyInBankTransfer> GetBankTransfer(long id, string userId);
        Task<IEnumerable<MoneyInBankTransfer>> GetBankTransfers(string userId);
        Task<IEnumerable<CardPayment>> GetCardPayments(string userIdentifier = null, int? pageNumber = null);
        Task<int> GetCardPaymentsCount(string userIdentifier = null);
        Task<CardPayment> GetCardPayment(string id, string userIdentifier = null);
        Task<CardPaymentCreated> PostCardPayment(CardPaymentRequest cardPayment, string userIdentifier = null);
        Task<BankAccount> GetBankAccount(long id, string userId = null);
        Task<List<BankAccount>> GetBankAccounts(string userId = null);
        Task<BankAccount> PostBankAccount(BankAccount bankAccount, string userId = null);
        Task<BankAccount> PutBankAccount(BankAccount bankAccount, string userId = null);
        void DeleteBankAccount(long id, string userId = null);
    }
}