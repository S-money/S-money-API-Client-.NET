using System;
using System.Collections.Generic;
using System.Net.Http;
using Smoney.API.Client.Models.Attachments;
using Smoney.API.Client.Models.Operations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public interface ISmoneyApiClient : IDisposable
    {
        Card GetCard(string appcardid, string userId = null);
        CardRegistration GetRegistration(string appcardid, string userId = null);
        IEnumerable<Card> GetCards(string userId = null);
        CardRegistration GetCardRegistration(string appcardid, string userId = null);
        IEnumerable<CardRegistration> GetCardsRegistration(string userId = null);
        CardRegistration PostCard(CardRegistration cardRegistration, string userId = null);
        HttpResponseMessage DeleteCard(string appcardid, string userId = null);
        Payment GetPayment(long id, string userId = null);
        IEnumerable<Payment> GetPayments(string userId = null);
        Payment PostPayment(Payment payment, string userId = null);
        User GetUser(long id);
        User GetUser(string identifier);
        IEnumerable<User> GetUsers(int? pageNumber = null);
        int GetUsersCount();
        List<User> GetAllUsers();
        IEnumerable<User> SearchUser(string firstName = null, string lastName = null, string email = null);
        User PostUser(User user);
        User PutUser(string identifier, User user);
        User PutUser(User user);
        User CloseUser(string identifier);
        User CloseUser();
        Account GetSubAccount(string identifier, string userid = null);
        Account GetSubAccount(long id, string userid = null);
        IEnumerable<Account> GetSubAccounts(string userid = null);
        Account PostSubAccount(Account subaccount, string userid = null);
        Account PutSubAccount(string identifier, Account subaccount, string userid = null);
        Account PutSubAccount(long id, Account subaccount, string userid = null);
        void DeleteSubAccount(string identifier, string userid = null);
        void DeleteSubAccount(long id, string userid = null);
        StoredCardPayment PostStoredCardPayment(StoredCardPayment moneyIn, string userId = null);
        IEnumerable<StoredCardPayment> GetStoredCardPayments(string userId = null);
        StoredCardPayment GetStoredCardPayment(string orderId, string userId = null);
        KYCDemand GetKYC(long id, string userId = null);
        IEnumerable<KYCDemand> GetKYCs(string userId = null);
        KYCDemand PostKYC(List<FileAttachment> files, string userId = null);
        MoneyOut PostRecurringMoneyOut(MoneyOut moneyOut, string userIdentifier = null);
        MoneyOut GetMoneyOut(long id, string userId = null);
        IEnumerable<MoneyOut> GetMoneyOuts(string userId = null);
        MoneyOut PostOneShotMoneyOut(MoneyOut moneyOut, string userId = null);
        MandateResponse GetMandate(long id, string userId = null);
        IEnumerable<MandateResponse> GetMandates(string userId = null);
        MandateResponse PostMandate(MandateRequest mandate, string userId = null);
        MoneyInDirectDebitResponse GetDirectDebit(long id, string userId = null);
        IEnumerable<MoneyInDirectDebitResponse> GetDirectDebits(string userId = null);
        MoneyInDirectDebitResponse PostDirectDebit(MoneyInDirectDebitRequest directdebit, string userId = null);
        MoneyInDirectDebitResponse UpdateDirectDebit(long id, MoneyInDirectDebitRequest update, string userId = null);
        MoneyInDirectDebitResponse DeleteDirectDebit(long id, string userId = null);
        BankTransferReferenceResponse PostBankTransferReference(string userId);
        BankTransferReferenceResponse PostBankTransferReference(BankTransferReferenceRequest request, string userId);
        BankTransferReferenceResponse GetBankTransfertReference(string reference, string userId);
        BankTransferReferenceResponse GetBankTransfertReference(long referenceId, string userId);
        IEnumerable<BankTransferReferenceResponse> GetBankTransfertReferences(string userId);
        IEnumerable<MoneyInBankTransfer> GetBankTransfer(string reference, string userId);
        MoneyInBankTransfer GetBankTransfer(long id, string userId);
        IEnumerable<MoneyInBankTransfer> GetBankTransfers(string userId);
        IEnumerable<CardPayment> GetCardPayments(string userIdentifier = null, int? pageNumber = null);
        int GetCardPaymentsCount(string userIdentifier = null);
        CardPayment GetCardPayment(string id, string userIdentifier = null);
        CardPaymentCreated PostCardPayment(CardPaymentRequest cardPayment, string userIdentifier = null);
        BankAccount GetBankAccount(long id, string userId = null);
        List<BankAccount> GetBankAccounts(string userId = null);
        BankAccount PostBankAccount(BankAccount bankAccount, string userId = null);
        BankAccount PutBankAccount(BankAccount bankAccount, string userId = null);
        void DeleteBankAccount(long id, string userId = null);
        string BaseURL { get; }
        int DefaultPageSize { get; }
        void Dispose();
    }
}