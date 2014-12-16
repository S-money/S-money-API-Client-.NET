using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Configuration;
using System.Web.Http;
using Smoney.API.Client.Models.Attachments;
using Smoney.API.Client.Models.Operations;
using Smoney.API.Client.Models.Users;
using System.Net.Http.Headers;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        public KYCDemand PostKYC(List<FileAttachment> files, string userid = null)
        {
            var uri = userid == null ? BaseURL + "kyc" : string.Format(BaseURL + "users/{0}/kyc", userid);

            var multipart = new MultipartFormDataContent();

            foreach (var file in files)
            {
                file.Content.Position = 0;

                var streamContent = new StreamContent(file.Content);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.Type);
                multipart.Add(streamContent, file.Name, file.Name);
            }

            var response = this.PostAsync(uri, multipart).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var kyc = response.Content.ReadAsAsync<KYCDemand>().Result;
            return kyc;
        }

        public KYCDemand GetKYC(long id, string userid = null)
        {
            var uri = userid == null ? BaseURL + "kyc/" : string.Format(BaseURL + "users/{0}/kyc/", userid);

            var response = this.GetAsync(uri + id).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var kyc = response.Content.ReadAsAsync<KYCDemand>().Result;
            return kyc;
        }

        public IList<KYCDemand> GetKYCs(string userid = null)
        {
            var uri = userid == null ? BaseURL + "kyc/" : string.Format(BaseURL + "users/{0}/kyc/", userid);
            var response = this.GetAsync(uri).Result;

            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var list = response.Content.ReadAsAsync<List<KYCDemand>>().Result;
            return list;
        }
    }
}
