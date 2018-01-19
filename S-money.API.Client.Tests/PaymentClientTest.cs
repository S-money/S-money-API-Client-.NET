using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Smoney.API.Client.Models.Operations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Tests
{
    [TestFixture]
    public class PaymentClientTest : CommonTests
    {
        private const string CHARGED_USER_ID = "02b99f43-26d9-4bd4-81a9-e1fdd1316870";
        public const int AMOUNT = 500;

        [TestFixtureSetUp]
        public void CheckUserAccount()
        {
            using (var client = CreateClient())
            {
                var user = client.GetUser(CHARGED_USER_ID);
                Assert.AreEqual(200000, user.DefaultAccount.Amount);
            }
        }

        [Test]
        public void MoveBetweenAccount()
        {
            using (var client = CreateClient())
            {
                var accountRef = new AccountRef
                                 {
                                     AppAccountId = UserId
                                 };
                var payment = new Payment
                              {
                                  Amount = AMOUNT,
                                  Beneficiary = accountRef,
                                  Message = "Test -" + TimedId
                              };
                var result = client.PostPayment(payment, CHARGED_USER_ID);
                Assert.IsNotNull(result);
                Assert.AreEqual(CHARGED_USER_ID, result.Sender.AppAccountId);

                accountRef.AppAccountId = CHARGED_USER_ID;

                result = client.PostPayment(payment, UserId);
                Assert.IsNotNull(result);
                Assert.AreEqual(UserId, result.Sender.AppAccountId);
            }
        }

        [Test]
        public void GetAllTransfert()
        {
            using (var client = CreateClient())
            {
                var transfert = client.GetPayments(CHARGED_USER_ID).ToList();
                Assert.Greater(transfert.Count(), 1);
                transfert.ForEach(t => Assert.AreEqual(AMOUNT, t.Amount));
            }
        }

        [Test]
        public void ReadOnePayment()
        {
            using (var client = CreateClient())
            {
                var transfert = client.GetPayments(CHARGED_USER_ID).Last();

                Assert.IsNotNull(transfert);
                var retrieved = client.GetPayment(transfert.Id, CHARGED_USER_ID);
                Assert.IsNotNull(retrieved);
                Assert.AreEqual(transfert.Id, retrieved.Id);
                Assert.AreEqual(transfert.Amount, retrieved.Amount);
                Assert.AreEqual(transfert.Message, retrieved.Message);
            }
        }
    }
}