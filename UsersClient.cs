using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Configuration;
using System.Web.Http;
using Smoney.API.Client.Enumerations;
using Smoney.API.Client.Models;
using Smoney.API.Client.Models.Users;
using System.Web;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        public User GetUser(long id)
        {
            return GetUser(id.ToString());
        }

        public User GetUser(string identifier)
        {
            var response = this.GetAsync(string.Format(BaseURL + "users/{0}", identifier)).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var user = response.Content.ReadAsAsync<User>().Result;
            return user;
        }

        public User GetUser()
        {
            var response = this.GetAsync(BaseURL + "users").Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var user = response.Content.ReadAsAsync<User>().Result;
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            var response = this.GetAsync(BaseURL + "users").Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
            return users;
        }

        public IEnumerable<User> SearchUser(string firstName = null, string lastName = null, string email = null)
        {
            string url = BaseURL + "users/search";
            var parameters = new System.Collections.Specialized.NameValueCollection();
            if (!String.IsNullOrWhiteSpace(firstName))
                parameters.Add("firstname", firstName);
            if (!String.IsNullOrWhiteSpace(lastName))
                parameters.Add("lastname", lastName);
            if (!String.IsNullOrWhiteSpace(email))
                parameters.Add("email", email);

            url = url + "?" + string.Join("&", parameters.AllKeys.Select(x => x + "=" + parameters[x]));

            var response = this.GetAsync(url).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
            return users;
        }

        public User PostUser(User user)
        {
            var response = this.PostAsJsonAsync(BaseURL + "users", user).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var createdUser = response.Content.ReadAsAsync<User>().Result;
            return createdUser;
        }

        public User PutUser(string identifier, User user)
        {
            var response = this.PutAsJsonAsync(string.Format(BaseURL + "users/{0}", identifier), user).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var createdUser = response.Content.ReadAsAsync<User>().Result;
            return createdUser;
        }

        public User PutUser(User user)
        {
            var response = this.PutAsJsonAsync(BaseURL + "users", user).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var createdUser = response.Content.ReadAsAsync<User>().Result;
            return createdUser;
        }

        public User CloseUser(string identifier)
        {
            var closeUser = new User { Status = UserStatus.Closed };

            var response = this.PutAsJsonAsync(string.Format(BaseURL + "users/{0}", identifier), closeUser).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var createdUser = response.Content.ReadAsAsync<User>().Result;
            return createdUser;
        }

        public User CloseUser()
        {
            var closeUser = new User { Status = UserStatus.Closed };

            var response = this.PutAsJsonAsync(BaseURL + "users", closeUser).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var createdUser = response.Content.ReadAsAsync<User>().Result;
            return createdUser;
        }
    }
}
