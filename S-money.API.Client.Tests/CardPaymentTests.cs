using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client.Tests
{
    [TestFixture]
    public class CardPaymentTests : CommonTests
    {
        [Test]
        public void InitiatePayment()
        {
            using (var client = CreateClient())
            {
                InitiatePayment(client, 50000);
            }
        }

        private void InitiatePayment(APIClient client, int amount = 10000)
        {
            var request = new CardPaymentRequest
                          {
                              Amount = amount,
                              OrderId = "TEST" + TimedId,
                              IsMine = true
                          };
            request.UrlReturn = "http://example.com/dummy/Payment";
            request.Message = request.UrlReturn;
            var result = client.PostCardPayment(request, UserId);
            Assert.IsNotNull(result);
            Assert.IsNotNullOrEmpty(result.Href);
            Assert.IsTrue(result.Href.StartsWith(BaseUrl));
        }

        [Test]
        public void GetCardPayments()
        {
            using (var client = CreateClient())
            {
                InitiatePayment(client);

                var payments = client.GetCardPayments(UserId);
                Assert.IsNotNull(payments);
                Assert.Greater(payments.Count(), 0);
            }
        }

        [Test]
        public void GetCardPayment()
        {
            using (var client = CreateClient())
            {
                InitiatePayment(client);

                var payments = client.GetCardPayments(UserId).ToList();
                Assert.IsNotNull(payments);
                Assert.Greater(payments.Count, 0);

                var item = payments[0];
                var payment = client.GetCardPayment(item.Id.ToString(), UserId);
                Assert.IsNotNull(payment);
            }
        }

        [Test]
        public void PagedResults()
        {
            using (var client = CreateClient())
            {
                var size = client.GetCardPaymentsCount(UserId);
                if (size < client.DefaultPageSize)
                {
                    var to = client.DefaultPageSize * 2 + client.DefaultPageSize / 2;
                    for (int i = size; i < to; i++)
                    {
                        InitiatePayment(client);
                    }
                    size = client.GetCardPaymentsCount(UserId);
                }

                List<CardPayment> results = new List<CardPayment>(size);
                int page = 1;
                while (results.Count != size)
                {
                    var items = client.GetCardPayments(UserId, page++).ToList();
                    results.AddRange(items);
                    Assert.Greater(items.Count, 0);

                    if (items.Count < client.DefaultPageSize)
                    {
                        Assert.AreEqual(size, results.Count);
                    }
                }
            }
        }
    }
}