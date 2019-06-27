using System.Linq;
using NUnit.Framework;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client.Tests
{
    [TestFixture]
    public class DirectDebitTester : CommonTests
    {
        [Test]
        public void GetMandate()
        {
            using (var client = CreateClient())
            {
                var response = CreateMandate(client);
                var result = client.GetMandate(response.Id, UserId);
                Assert.IsNotNull(result);

                var all = client.GetMandates(UserId).ToList();
                Assert.IsNotNull(all);
                Assert.Greater(all.Count, 0);
                Assert.IsTrue(all.Any(e => e.Id == result.Id));
            }
        }

        private MandateResponse CreateMandate(APIClient client)
        {
            var reference = BankAccountTester.CreateAccount(client, UserId);
            var mandate = new MandateRequest
                          {
                              BankAccount = reference,
                              UrlReturn = "http://example.com/dummy/Mandate"
                          };

            var result = client.PostMandate(mandate, UserId);
            Assert.IsNotNull(result);
            Assert.Greater(result.Id, 0);
            Assert.AreEqual(reference.Id, result.BankAccount.Id);
            return result;
        }

        [Test]
        public void RequestDirectDebit()
        {
            using (var client = CreateClient())
            {
                var debit = CreateDirectDebit(client);

                var retrieved = client.GetDirectDebit(debit.Id, UserId);
                Assert.IsNotNull(retrieved);

                var all = client.GetDirectDebits(UserId).ToList();
                Assert.IsNotNull(all);
                Assert.Greater(all.Count, 0);
                Assert.IsTrue(all.Any(e => e.Id == retrieved.Id));
            }
        }

        [Test]
        public void GetAllDirectDebits()
        {
            using (var client = CreateClient())
            {
                var requests = client.GetDirectDebits().ToList();
                Assert.IsNotNull(requests);
                Assert.Greater(requests.Count, 1);
            }
        }

        [Test]
        public void UpdateDirectDebit()
        {
            using (var client = CreateClient())
            {
                var debit = CreateDirectDebit(client);
                const int newAmount = 2 * PaymentClientTest.AMOUNT;
                var update = new MoneyInDirectDebitRequest { Amount = newAmount };
                var updated = client.UpdateDirectDebit(debit.Id, update, UserId);
                Assert.IsNotNull(updated);
                Assert.AreEqual(debit.Id, updated.Id);
                Assert.AreEqual(newAmount, updated.Amount);
            }
        }

        [Test]
        public void RemoveDirectDebit()
        {
            using (var client = CreateClient())
            {
                var debit = CreateDirectDebit(client);
                MoneyInDirectDebitResponse result = client.DeleteDirectDebit(debit.Id, UserId);
                Assert.IsNotNull(result);
                Assert.AreEqual(debit.Id, result.Id);
                Assert.AreEqual(PaymentStatus.Canceled, result.Status);
            }
        }

        [Test]
        public void GetDirectDebitWithOrderId()
        {
            using (var client = CreateClient())
            {
                var debit = CreateDirectDebit(client);
                MoneyInDirectDebitResponse result = client.GetDirectDebit(debit.OrderId, UserId);
                Assert.IsNotNull(result);
                Assert.AreEqual(debit.Id, result.Id);
            }
            
        }
        
        private MoneyInDirectDebitResponse CreateDirectDebit(APIClient client)
        {
            var mandate = CreateMandate(client);
            var directdebit = new MoneyInDirectDebitRequest
                              {
                                  Mandate = new MandateRef { Id = mandate.Id },
                                  OrderId = "DirectDebit-" + TimedId,
                                  Amount = PaymentClientTest.AMOUNT,
                                  Fee = 0,
                                  IsMine = true,
                                  Message = "Test RequestDirectDebit"
                              };
            var response = client.PostDirectDebit(directdebit, UserId);

            Assert.IsNotNull(response);
            Assert.AreEqual(PaymentStatus.Pending, response.Status);
            return response;
        }
    }
}