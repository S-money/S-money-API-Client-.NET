using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Configuration;
using System.Web.Http;
using Smoney.API.Client.Models;
using Smoney.API.Client.Models.Operations;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        public Payment GetPayment(long id, string userid = null)
        {
            var uri = userid == null ? BaseURL + "payments/" : string.Format(BaseURL + "users/{0}/payments/", userid);

            var response = this.GetAsync(uri + id).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var payment = response.Content.ReadAsAsync<Payment>().Result;
            return payment;
        }

        public IEnumerable<Payment> GetPayments(string userid = null)
        {
            var uri = userid == null ? BaseURL + "payments" : string.Format(BaseURL + "users/{0}/payments", userid);

            var response = this.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var payments = response.Content.ReadAsAsync<IEnumerable<Payment>>().Result;
            return payments;
        }

        public Payment PostPayment(Payment payment, string userid = null)
        {
            var uri = userid == null ? BaseURL + "payments" : string.Format(BaseURL + "users/{0}/payments", userid);

            var response = this.PostAsJsonAsync(uri, payment).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var createdpayment = response.Content.ReadAsAsync<Payment>().Result;
            return createdpayment;
        }
    }
}
