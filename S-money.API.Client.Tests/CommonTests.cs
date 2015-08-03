using System;
using System.Globalization;
using NUnit.Framework;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client.Tests
{
    public class CommonTests
    {
        protected string Key
        {
            get { return "YOUR KEY HERE"; }
        }

        protected string BaseUrl
        {
            get { return "https://rest-pp.s-money.fr/api/sandbox"; }
        }

        protected APIClient CreateClient()
        {
            var client = new APIClient(BaseUrl);
            client.AddRequestHeader("Authorization", "Bearer " + Key);
            return client;
        }

        protected string GenerateId()
        {
            return "Test" + TimedId;
        }

        protected string TimedId
        {
            get
            {
                return DateTime.UtcNow.ToString("yyMMdd-HHmmssfff", CultureInfo.InvariantCulture);
            }
        }

        protected string UserId { get; private set; }

        [TestFixtureSetUp]
        public void GetUserId()
        {
            using (var client = CreateClient())
            {
                UserId = "johndoe-" + TimedId;
                try
                {
                    var user = client.GetUser(UserId);
                    Assert.IsNotNull(user);
                }
                catch { }
                finally
                {
                    CreateTestUser(client, UserId);
                }
            }
        }

        private void CreateTestUser(APIClient client, string userId)
        {
            UserProfile profile = new UserProfile
                                  {
                                      FirstName = "Test - John",
                                      LastName = "Doe",
                                      Birthdate = DateTime.Today.AddYears(-20),
                                      Address = new Address { Country = Country.France }
                                  };
            var user = new User { AppUserId = userId, Profile = profile };
            client.PostUser(user);
        }
    }
}