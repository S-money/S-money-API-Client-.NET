using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client.Tests
{
    [TestFixture]
    public class CardPaymentTests : CommonTests
    {
        [Test]
        public async Task InitiatePayment()
        {
            using (var client = CreateClient())
            {
                await InitiatePayment(client, 50000);
            }
        }

        private async Task InitiatePayment(APIClient client, int amount = 10000)
        {
            var request = new CardPaymentRequest
                          {
                              Amount = amount,
                              OrderId = "TEST" + TimedId,
                              IsMine = true
                          };
            request.UrlReturn = "http://example.com/dummy/Payment";
            request.Message = request.UrlReturn;
            var result = await client.PostCardPayment(request, UserId);
            Assert.IsNotNull(result);
            Assert.IsNotNullOrEmpty(result.Href);
            Assert.IsTrue(result.Href.StartsWith(BaseUrl));
        }

        [Test]
        public async Task GetCardPayments()
        {
            using (var client = CreateClient())
            {
                await InitiatePayment(client);

                var payments = await client.GetCardPayments(UserId);
                Assert.IsNotNull(payments);
                Assert.Greater(payments.Count(), 0);
            }
        }

        [Test]
        public async Task GetCardPayment()
        {
            using (var client = CreateClient())
            {
                await InitiatePayment(client);

                var payments = await client.GetCardPayments(UserId);
                var list = payments.ToList();
                Assert.IsNotNull(list);
                Assert.Greater(list.Count, 0);

                var item = list[0];
                var payment = await client.GetCardPayment(item.Id.ToString(), UserId);
                Assert.IsNotNull(payment);
            }
        }

        [Test]
        public async Task PagedResults()
        {
            using (var client = CreateClient())
            {
                var size = await client.GetCardPaymentsCount(UserId);
                if (size < client.DefaultPageSize)
                {
                    var to = client.DefaultPageSize * 2 + client.DefaultPageSize / 2;
                    for (int i = size; i < to; i++)
                    {
                        await InitiatePayment(client);
                    }
                    size = await client.GetCardPaymentsCount(UserId);
                }

                List<CardPayment> results = new List<CardPayment>(size);
                int page = 1;
                while (results.Count != size)
                {
                    var items = await client.GetCardPayments(UserId, page++);
                    var list = items.ToList();
                    results.AddRange(list);
                    Assert.Greater(list.Count, 0);

                    if (list.Count < client.DefaultPageSize)
                    {
                        Assert.AreEqual(size, results.Count);
                    }
                }
            }
        }
    }
}