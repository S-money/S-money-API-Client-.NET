using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client.Tests
{
    [TestFixture]
    public class BankTransferTester : CommonTests
    {
        [Test]
        public async Task CreateTransfertReference()
        {
            using (var client = CreateClient())
            {
                await CreateTransfertReference(client);
            }
        }

        [Test]
        public async Task GetTransfertReference()
        {
            using (var client = CreateClient())
            {
                var payment = await CreateTransfertReference(client);
                var resultByReference = await client.GetBankTransfertReference(payment.Reference, UserId);
                Assert.IsNotNull(resultByReference);
                Assert.AreEqual(payment.Id, resultByReference.Id);
                Assert.AreEqual(payment.Reference, resultByReference.Reference);
                var resultById = await client.GetBankTransfertReference(payment.Id, UserId);
                Assert.IsNotNull(resultById);
                Assert.AreEqual(resultByReference.Id, resultById.Id);
                Assert.AreEqual(resultByReference.Reference, resultById.Reference);

                var allReferences = await client.GetBankTransfertReferences(UserId);
                var references = allReferences.ToList();
                Assert.Greater(references.Count, 0);
                Assert.IsTrue(references.Any(e=> e.Id == resultById.Id));
            }
        }

        private async Task<BankTransferReferenceResponse> CreateTransfertReference(APIClient client)
        {
            var result = await client.PostBankTransferReference(UserId);
            Assert.IsNotNull(result);
            Assert.IsNotNullOrEmpty(result.Reference);
            return result;
        }

        [Test]
        public async Task GetBankTransfert()
        {
            using (var client = CreateClient())
            {
                var payment = await CreateTransfertReference(client);
                var result = await client.GetBankTransfer(payment.Reference, UserId);
                Assert.IsNotNull(result);

                var allTransferts = await client.GetBankTransfers(UserId);
                Assert.IsNotNull(allTransferts);
                Assert.GreaterOrEqual(allTransferts.Count(), result.Count());
            }
        }
    }
}