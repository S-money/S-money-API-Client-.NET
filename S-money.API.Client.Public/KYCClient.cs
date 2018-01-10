using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Smoney.API.Client.Models.Attachments;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string kyc = "kyc";

        public async Task<KYCDemand> GetKYC(long id, string userId = null)
        {
            var uri = CreateUri(userId, kyc);
            return await GetAsync<KYCDemand>(uri + id);
        }

        public async Task<IEnumerable<KYCDemand>> GetKYCs(string userId = null)
        {
            var uri = CreateUri(userId, kyc);
            return await GetAsync<IEnumerable<KYCDemand>>(uri);
        }

        public async Task<KYCDemand> PostKYC(List<FileAttachment> files, string userId = null)
        {
            var uri = userId == null ? BaseURL + "kyc" : string.Format(BaseURL + "users/{0}/kyc", userId);

            var multipart = new MultipartFormDataContent();

            foreach (var file in files)
            {
                file.Content.Position = 0;

                var streamContent = new StreamContent(file.Content);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.Type);
                multipart.Add(streamContent, file.Name, file.Name);
            }

            var response = base.PostAsync(uri, multipart).Result;
            return await HandleResult<KYCDemand>(response);
        }
    }
}