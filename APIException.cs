using System;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using NLog;

namespace Smoney.API.Client
{
    public class APIException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }

        public Fault SmoneyError { get; private set; }

        public APIException(HttpResponseMessage m)
        {
            HttpStatusCode = m.StatusCode;

            var result = m.Content.ReadAsStringAsync().Result;

            if (IsJson(result))
            {
                SmoneyError = JsonConvert.DeserializeObject<Fault>(result);
            }
            else
            {
                LogManager.GetLogger("SmoneyAPIClient")
                          .Trace("Exception : code [{0}] Phrase=\"{1}\" Message=\"{2}\" Result = [{3}]", m.StatusCode, m.ReasonPhrase,
                                 m.RequestMessage, result);
                throw new HttpException((int)m.StatusCode, m.ReasonPhrase);
            }
        }

        private static bool IsJson(string jsonData)
        {
            if (String.IsNullOrWhiteSpace(jsonData))
            {
                return false;
            }

            return jsonData.Trim().Substring(0, 1).IndexOfAny(new[] { '[', '{' }) == 0;
        }
    }

    public class Fault
    {
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
    }
}