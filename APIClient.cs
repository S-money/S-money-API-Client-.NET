using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Configuration;
using System.Web.Http;
using log4net;

namespace Smoney.API.Client
{
    public partial class APIClient : HttpClient
    {
        public string BaseURL { get { return BaseAddress.AbsolutePath + "/" ; } }

        private string MediaTypeVersion { get { return "application/vnd.s-money.v1+json"; } }

        public APIClient(string baseAddress) : base()
        {
            this.BaseAddress = new Uri(baseAddress);

            this.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeVersion));
        }
        
        public void AddRequestHeader(string name, string value)
        {
            this.RemoveRequestHeader(name);
            this.DefaultRequestHeaders.Add(name, value);
        }

        public void RemoveRequestHeader(string name)
        {
            this.DefaultRequestHeaders.Remove(name);
        }

        public void ReplaceRequestHeader(string name, string value)
        {
            RemoveRequestHeader(name);
            AddRequestHeader(name, value);
        }

        public override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            LogManager.GetLogger("SmoneyAPIClient").DebugFormat("Calling API {0}", request.RequestUri);

            if (request.Content != null && request.Content.Headers.ContentType != null
                && request.Content.Headers.ContentType.MediaType != "text/html"
                && request.Content.Headers.ContentType.MediaType != "application/x-www-form-urlencoded"
                && request.Content.Headers.ContentType.MediaType != "multipart/form-data"
                && request.Content.Headers.ContentType.MediaType != "application/vnd.s-money.v2+json")
                request.Content.Headers.ContentType.MediaType = MediaTypeVersion;
            var response = base.SendAsync(request, cancellationToken);

            if (response.Result != null && response.Result.Content != null && response.Result.Content.Headers.ContentType != null)
                response.Result.Content.Headers.ContentType.MediaType = "application/json";

            return response;
        }

        public new System.Threading.Tasks.Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            LogManager.GetLogger("SmoneyAPIClient").DebugFormat("Calling API {0}", requestUri);

            var response = base.GetAsync(requestUri);

            if (response.Result != null && response.Result.Content != null && response.Result.Content.Headers != null
                && response.Result.StatusCode != System.Net.HttpStatusCode.NoContent)
                response.Result.Content.Headers.ContentType.MediaType = "application/json";

            return response;
        }
    }
}
