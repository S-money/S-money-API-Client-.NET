using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace Smoney.API.Client
{
    public partial class APIClient : HttpClient, ISmoneyApiClient
    {
        private readonly int allocationSize;
        private const string page = "?page=";

        public string BaseURL
        {
            get { return BaseAddress.AbsolutePath + "/"; }
        }

        private string MediaTypeVersion
        {
            get { return "application/vnd.s-money.v1+json"; }
        }

        public int DefaultPageSize { get { return 50; } }

        public APIClient(string baseAddress)
        {
            BaseAddress = new Uri(baseAddress);

            allocationSize = BaseURL.Length + 1;
            allocationSize += Guid.Empty.ToString().Length + 1;
            allocationSize += users.Length + 1;
            allocationSize += storedcardpayments.Length + 1;
            allocationSize += page.Length + 1;

            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeVersion));
        }

        public void AddRequestHeader(string name, string value)
        {
            RemoveRequestHeader(name);
            DefaultRequestHeaders.Add(name, value);
        }

        public void RemoveRequestHeader(string name)
        {
            DefaultRequestHeaders.Remove(name);
        }

        public void ReplaceRequestHeader(string name, string value)
        {
            RemoveRequestHeader(name);
            AddRequestHeader(name, value);
        }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                            CancellationToken cancellationToken)
        {
            SmoneyLogger.Logger.Debug(string.Format("Calling API {0}", request.RequestUri));

            if (request.Content != null && request.Content.Headers.ContentType != null
                && request.Content.Headers.ContentType.MediaType != "text/html"
                && request.Content.Headers.ContentType.MediaType != "application/x-www-form-urlencoded"
                && request.Content.Headers.ContentType.MediaType != "multipart/form-data"
                && request.Content.Headers.ContentType.MediaType != "application/vnd.s-money.v2+json")
            {
                request.Content.Headers.ContentType.MediaType = DefaultRequestHeaders.Accept.ToString();
            }
            var response = base.SendAsync(request, cancellationToken);

            if (response.Result != null && response.Result.Content != null && response.Result.Content.Headers.ContentType != null)
            {
                response.Result.Content.Headers.ContentType.MediaType = "application/json";
            }

            return response;
        }

        public new Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            SmoneyLogger.Logger.Debug(string.Format("Calling API {0}", requestUri));

            var response = base.GetAsync(requestUri);

            if (response.Result != null && response.Result.Content != null && response.Result.Content.Headers != null
                && response.Result.StatusCode != HttpStatusCode.NoContent)
            {
                response.Result.Content.Headers.ContentType.MediaType = "application/json";
            }

            return response;
        }

        public T GetAsync<T>(string uri)
        {
            var response = GetAsync(uri).Result;
            return HandleResult<T>(response);
        }

        private T PostAsync<T>(string uri, T item)
        {
            var response = this.PostAsJsonAsync(uri, item).Result;
            return HandleResult<T>(response);
        }

        private TResponse PostAsync<TRequest, TResponse>(string uri, TRequest item)
        {
            var response = this.PostAsJsonAsync(uri, item).Result;
            return HandleResult<TResponse>(response);
        }


        private T PutAsync<T>(string uri, T item)
        {
            var response = this.PutAsJsonAsync(uri, item).Result;
            return HandleResult<T>(response);
        }

        private TResponse PutAsync<TRequest, TResponse>(string uri, TRequest item)
        {
            var response = this.PutAsJsonAsync(uri, item).Result;
            return HandleResult<TResponse>(response);
        }




        private static T HandleResult<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new APIException(response);
            }

            var result = response.Content.ReadAsAsync<T>().Result;
            return result;
        }

        private int GetCount(string uri)
        {
            uri += "?perpage=1";
            var response = GetAsync(uri).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new APIException(response);
            }

            IEnumerable<string> items = response.Headers.GetValues("Total-count");
            var first = items.First();
            var result = int.Parse(first);
            return result;
        }

        private string CreateUri(string userId, string path, int? pageNumber = null)
        {
            StringBuilder builder = new StringBuilder(BaseURL, allocationSize);

            if (!string.IsNullOrEmpty(userId))
            {
                builder.Append(users).Append('/')
                       .Append(userId).Append('/');
            }

            if (!string.IsNullOrEmpty(path))
            {
                builder.Append(path).Append('/');
            }

            if (pageNumber.HasValue)
            {
                if (pageNumber.Value == 0)
                {
                    SmoneyLogger.Logger.Warn(string.Format("Start Page numbering is 1, not 0"));
                }
                builder.Append(page).Append(pageNumber.Value);
            }

            var result = builder.ToString();
            return result;
        }

        private void UseV2()
        {
            ReplaceRequestHeader("Accept", "application/vnd.s-money.v2+json");
        }
    }
}