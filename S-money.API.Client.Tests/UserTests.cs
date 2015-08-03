using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using Smoney.API.Client.Models.Users;
using Smoney.API.Client.Enumerations;

namespace Smoney.API.Client.Tests
{
    [TestFixture]
    internal class UserTests : CommonTests
    {
        [Test]
        public void RetrieveAllUsers()
        {
            using (var client = CreateClient())
            {
                var users = client.GetUsers();
                Assert.GreaterOrEqual(users.Count(), 0);
            }
        }

        [Test]
        public void RetrieveUserNumber()
        {
            using (var client = CreateClient())
            {
                var count = client.GetUsersCount();
                Assert.GreaterOrEqual(count, 1);
            }
        }

        [Test]
        public void GetUserPages()
        {
            using (var client = CreateClient())
            {
                var count = client.GetUsersCount();
                Assert.GreaterOrEqual(count, client.DefaultPageSize);

                var expected = count % client.DefaultPageSize;
                var pageNumber = 1 + count / client.DefaultPageSize;

                var firstPage = client.GetUsers().ToList();
                Assert.IsNotNull(firstPage);
                Assert.AreEqual(client.DefaultPageSize, firstPage.Count);

                var secondPage = client.GetUsers(pageNumber).ToList();
                Assert.IsNotNull(secondPage);
                Assert.AreEqual(expected, secondPage.Count);

                var comparer = new UserComparer();
                var result = firstPage.Union(secondPage, comparer);
                Assert.AreEqual(client.DefaultPageSize + expected, result.Count());
            }
        }

        [Test]
        public void CreateUser()
        {
            using (var client = CreateClient())
            {
                var count = client.GetUsersCount();
                UserProfile profile = new UserProfile
                                      {
                                          FirstName = "TEST",
                                          LastName = "TEST",
                                          Birthdate = DateTime.Today.AddYears(-20),
                                          Address = new Address { Country = Country.France }
                                      };
                User user = new User { AppUserId = GenerateId(), Profile = profile };
                var result = client.PostUser(user);
                Assert.Greater(result.Id, 0);

                var newNumber = client.GetUsersCount();
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