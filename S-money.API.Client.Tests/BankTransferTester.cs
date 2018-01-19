using System.Linq;
using System.Net;
using NUnit.Framework;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client.Tests
{
    [TestFixture]
    public class BankTransferTester : CommonTests
    {
        [Test]
        public void CreateTransfertReference()
        {
            using (var client = CreateClient())
            {
                CreateTransfertReference(client);
            }
        }

        [Test]
        public void GetTransfertReference()
        {
            using (var client = CreateClient())
            {
                var payment = CreateTransfertReference(client);
                var resultByReference = client.GetBankTransfertReference(payment.Reference, UserId);
                Assert.IsNotNull(resultByReference);
                Assert.AreEqual(payment.Id, resultByReference.Id);
                Assert.AreEqual(payment.Reference, resultByReference.Reference);
                var resultById = client.GetBankTransfertReference(payment.Id, UserId);
                Assert.IsNotNull(resultById);
                Assert.AreEqual(resultByReference.Id, resultById.Id);
                Assert.AreEqual(resultByReference.Reference, resultById.Reference);

                var allReferences = client.GetBankTransfertReferences(UserId).ToList();
                Assert.Greater(allReferences.Count, 0);
                Assert.IsTrue(allReferences.Any(e=> e.Id == resultById.Id));
            }
        }

        private BankTransferReferenceResponse CreateTransfertReference(APIClient client)
        {
            var result = client.PostBankTransferReference(UserId);
            Assert.IsNotNull(result);
            Assert.IsNotNullOrEmpty(result.Reference);
            return result;
        }

        [Test]
        public void GetBankTransfert()
        {
            using (var client = CreateClient())
            {
                var payment = CreateTransfertReference(client);
                var result = client.GetBankTransfer(payment.Reference, UserId);
                Assert.IsNotNull(result);

                var allTransferts = client.GetBankTransfers(UserId);
                Assert.IsNotNull(allTransferts);
                Assert.GreaterOrEqual(allTransferts.Count(), result.Count());
            }
        }
    }
}