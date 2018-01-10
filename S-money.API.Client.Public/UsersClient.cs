using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string users = "users";

        public async Task<User> GetUser(long id)
        {
            return await GetUser(id.ToString());
        }

        public async Task<User> GetUser(string identifier)
        {
            var uri = CreateUri(identifier, string.Empty);
            return await GetAsync<User>(uri);
        }

        public async Task<IEnumerable<User>> GetUsers(int? pageNumber = null)
        {
            var uri = CreateUri(string.Empty, users, pageNumber);
            return await GetAsync<IEnumerable<User>>(uri);
        }

        public async Task<int> GetUsersCount()
        {
            var uri = CreateUri(string.Empty, users);
            return await GetCount(uri);
        }

        public async Task<List<User>> GetAllUsers()
        {
            SmoneyLogger.Logger.Warn("");
            var count = await GetUsersCount();
            List<User> results = new List<User>(count);
            int currentPage = 1;
            while (results.Count != count)
            {
                var items = await GetUsers(currentPage++);
                results.AddRange(items);
            }
            return results;
        }

        public async Task<IEnumerable<User>> SearchUser(string firstName = null, string lastName = null, string email = null)
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
            return await HandleResult<IEnumerable<User>>(response);
        }

        public async Task<User> PostUser(User user)
        {
            var uri = CreateUri(string.Empty, users);
            return await PostAsync(uri, user);
        }

        public async Task<User> PutUser(string identifier, User user)
        {
            var response = this.PutAsJsonAsync(string.Format(BaseURL + "users/{0}", identifier), user).Result;
            return await HandleResult<User>(response);
        }

        public async Task<User> PutUser(User user)
        {
            var response = this.PutAsJsonAsync(BaseURL + "users", user).Result;
            return await HandleResult<User>(response);
        }

        public async Task<User> CloseUser(string identifier)
        {
            var closeUser = new User { Status = UserStatus.Closed };

            var response = this.PutAsJsonAsync(string.Format(BaseURL + "users/{0}", identifier), closeUser).Result;
            return await HandleResult<User>(response);
        }

        public async Task<User> CloseUser()
        {
            var closeUser = new User { Status = UserStatus.Closed };

            var response = this.PutAsJsonAsync(BaseURL + "users", closeUser).Result;
            return await HandleResult<User>(response);
        }
    }
}