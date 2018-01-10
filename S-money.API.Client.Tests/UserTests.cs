using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Smoney.API.Client.Models.Users;
using Smoney.API.Client.Enumerations;

namespace Smoney.API.Client.Tests
{
    [TestFixture]
    internal class UserTests : CommonTests
    {
        [Test]
        public async Task RetrieveAllUsers()
        {
            using (var client = CreateClient())
            {
                var count = await client.GetUsersCount();
                var users = await client.GetAllUsers();
                Assert.AreEqual(count, users.Count);
            }
        }

        [Test]
        public async Task RetrieveUserNumber()
        {
            using (var client = CreateClient())
            {
                var count = await client.GetUsersCount();
                Assert.GreaterOrEqual(count, 1);
            }
        }

        [Test]
        public async Task GetUserPages()
        {
            using (var client = CreateClient())
            {
                var count = await client.GetUsersCount();
                Assert.GreaterOrEqual(count, client.DefaultPageSize);

                var expected = count % client.DefaultPageSize;
                var pageNumber = 1 + count / client.DefaultPageSize;

                var firstPage = await client.GetUsers();
                var usersList = firstPage.ToList();
                Assert.IsNotNull(usersList);
                Assert.AreEqual(client.DefaultPageSize, usersList.Count);

                var secondPage = await client.GetUsers(pageNumber);
                var pageList = secondPage.ToList();
                Assert.IsNotNull(pageList);
                Assert.AreEqual(expected, pageList.Count);

                var comparer = new UserComparer();
                var result = usersList.Union(pageList, comparer);
                Assert.AreEqual(client.DefaultPageSize + expected, result.Count());
            }
        }

        [Test]
        public async Task CreateUser()
        {
            using (var client = CreateClient())
            {
                var count = await client.GetUsersCount();
                UserProfile profile = new UserProfile
                                      {
                                          FirstName = "TEST",
                                          LastName = "TEST",
                                          Birthdate = DateTime.Today.AddYears(-20),
                                          Address = new Address { Country = Country.France }
                                      };
                User user = new User { AppUserId = GenerateId(), Profile = profile };
                var result = await client.PostUser(user);
                Assert.Greater(result.Id, 0);

                var newNumber = await client.GetUsersCount();
                Assert.AreEqual(count + 1, newNumber);
            }
        }

        private class UserComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                return String.Equals(x.AppUserId, y.AppUserId, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(User obj)
            {
                return obj.AppUserId.GetHashCode();
            }
        }
    }
}