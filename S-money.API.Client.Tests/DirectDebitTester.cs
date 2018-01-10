using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client.Tests
{
    [TestFixture]
    public class DirectDebitTester : CommonTests
    {
        [Test]
        public async Task GetMandate()
        {
            using (var client = CreateClient())
            {
                var response = await CreateMandate(client);
                var result = await client.GetMandate(response.Id, UserId);
                Assert.IsNotNull(result);

                var all = await client.GetMandates(UserId);
                var list = all.ToList();
                Assert.IsNotNull(all);
                Assert.Greater(list.Count, 0);
                Assert.IsTrue(list.Any(e => e.Id == result.Id));
            }
        }

        private async Task<MandateResponse> CreateMandate(APIClient client)
        {
            var reference = await BankAccountTester.CreateAccount(client, UserId);
            var mandate = new MandateRequest
                          {
                              BankAccount = reference,
                              UrlReturn = "http://example.com/dummy/Mandate"
                          };

            var result = await client.PostMandate(mandate, UserId);
            Assert.IsNotNull(result);
            Assert.Greater(result.Id, 0);
            Assert.AreEqual(reference.Id, result.BankAccount.Id);
            return result;
        }

        [Test]
        public async Task RequestDirectDebit()
        {
            using (var client = CreateClient())
            {
                var debit = await CreateDirectDebit(client);

                var retrieved = await client.GetDirectDebit(debit.Id, UserId);
                Assert.IsNotNull(retrieved);

                var all = await client.GetDirectDebits(UserId);
                var list = all.ToList();
                Assert.IsNotNull(list);
                Assert.Greater(list.Count, 0);
                Assert.IsTrue(list.Any(e => e.Id == retrieved.Id));
            }
        }

        [Test]
        public async Task GetAllDirectDebits()
        {
            using (var client = CreateClient())
            {
                var requests = await client.GetDirectDebits();
                var list = requests.ToList();
                Assert.IsNotNull(list);
                Assert.Greater(list.Count, 1);
            }
        }

        [Test]
        public async Task UpdateDirectDebit()
        {
            using (var client = CreateClient())
            {
                var debit = await CreateDirectDebit(client);
                const int newAmount = 2 * PaymentClientTest.AMOUNT;
                var update = new MoneyInDirectDebitRequest { Amount = newAmount };
                var updated = await client.UpdateDirectDebit(debit.Id, update, UserId);
                Assert.IsNotNull(updated);
                Assert.AreEqual(debit.Id, updated.Id);
                Assert.AreEqual(newAmount, updated.Amount);
            }
        }

        [Test]
        public async Task RemoveDirectDebit()
        {
            using (var client = CreateClient())
            {
                var debit = await CreateDirectDebit(client);
                MoneyInDirectDebitResponse result = await client.DeleteDirectDebit(debit.Id, UserId);
                Assert.IsNotNull(result);
                Assert.AreEqual(debit.Id, result.Id);
                Assert.AreEqual(PaymentStatus.Canceled, result.Status);
            }
        }

        [Test]
        public async Task GetDirectDebitWithOrderId()
        {
            using (var client = CreateClient())
            {
                var debit = await CreateDirectDebit(client);
                MoneyInDirectDebitResponse result = await client.GetDirectDebit(debit.OrderId, UserId);
                Assert.IsNotNull(result);
                Assert.AreEqual(debit.Id, result.Id);
            }
            
        }

        private async Task<MoneyInDirectDebitResponse> CreateDirectDebit(APIClient client)
        {
            var mandate = await CreateMandate(client);
            var directdebit = new MoneyInDirectDebitRequest
                              {
                                  Mandate = new MandateRef { Id = mandate.Id },
                                  OrderId = "DirectDebit-" + TimedId,
                                  Amount = PaymentClientTest.AMOUNT,
                                  Fee = 0,
                                  IsMine = true,
                                  Message = "Test RequestDirectDebit"
                              };
            var response = await client.PostDirectDebit(directdebit, UserId);

            Assert.IsNotNull(response);
            Assert.AreEqual(PaymentStatus.Pending, response.Status);
            return response;
        }
    }
}