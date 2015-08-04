using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string users = "users";

        public User GetUser(long id)
        {
            return GetUser(id.ToString());
        }

        public User GetUser(string identifier)
        {
            var uri = CreateUri(identifier, string.Empty);
            return GetAsync<User>(uri);
        }

        public IEnumerable<User> GetUsers(int? pageNumber = null)
        {
            var uri = CreateUri(string.Empty, users, pageNumber);
            return GetAsync<IEnumerable<User>>(uri);
        }

        public int GetUsersCount()
        {
            var uri = CreateUri(string.Empty, users);
            return GetCount(uri);
        }

        public List<User> GetAllUsers()
        {
            SmoneyLogger.Logger.Warn("");
            var count = GetUsersCount();
            List<User> results = new List<User>(count);
            int currentPage = 1;
            while (results.Count != count)
            {
                var items = GetUsers(currentPage++);
                results.AddRange(items);
            }
            return results;
        }

        public IEnumerable<User> SearchUser(string firstName = null, string lastName = null, string email = null)
        {
            string url = BaseURL + "users/search";
            var parameters = new NameValueCollection();
            if (!String.IsNullOrWhiteSpace(firstName))
            {
                parameters.Add("firstname", firstName);
            }
            if (!String.IsNullOrWhiteSpace(lastName))
            {
                parameters.Add("lastname", lastName);
            }
            if (!String.IsNullOrWhiteSpace(email))
            {
                parameters.Add("email", email);
            }

            url = url + "?" + string.Join("&", parameters.AllKeys.Select(x => x + "=" + parameters[x]));

            var response = this.GetAsync(url).Result;
            return HandleResult<IEnumerable<User>>(response);
        }

        public User PostUser(User user)
        {
            var uri = CreateUri(string.Empty, users);
            return PostAsync(uri, user);
        }

        public User PutUser(string identifier, User user)
        {
            var response = this.PutAsJsonAsync(string.Format(BaseURL + "users/{0}", identifier), user).Result;
            return HandleResult<User>(response);
        }

        public User PutUser(User user)
        {
            var response = this.PutAsJsonAsync(BaseURL + "users", user).Result;
            return HandleResult<User>(response);
        }

        public User CloseUser(string identifier)
        {
            var closeUser = new User { Status = UserStatus.Closed };

            var response = this.PutAsJsonAsync(string.Format(BaseURL + "users/{0}", identifier), closeUser).Result;
            return HandleResult<User>(response);
        }

        public User CloseUser()
        {
            var closeUser = new User { Status = UserStatus.Closed };

            var response = this.PutAsJsonAsync(BaseURL + "users", closeUser).Result;
            return HandleResult<User>(response);
        }
    }
}