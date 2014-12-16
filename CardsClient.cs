using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Configuration;
using System.Web.Http;
using Smoney.API.Client.Models;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public partial class APIClient
    {        
        public Card GetCard(string appcardid, string userid = null)
        {
            var uri = userid == null ? BaseURL + "cards/" : string.Format(BaseURL + "users/{0}/cards/", userid);

            var response = this.GetAsync(uri + appcardid).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var card = response.Content.ReadAsAsync<Card>().Result;
            return card;
        }

        public CardRegistration GetRegistration(string appcardid, string userid = null)
        {
            var uri = userid == null ? BaseURL + "cards/registrations" : string.Format(BaseURL + "users/{0}/cards/registrations", userid);

            var response = this.GetAsync(uri + appcardid).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var cardRegistration = response.Content.ReadAsAsync<CardRegistration>().Result;
            return cardRegistration;
        }

        public IEnumerable<Card> GetCards(string userid = null)
        {
            var uri = userid == null ? BaseURL + "cards" : string.Format(BaseURL + "users/{0}/cards", userid);

            var response = this.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var cards = response.Content.ReadAsAsync<IEnumerable<Card>>().Result;
            return cards;
        }

        public CardRegistration GetCardRegistration(string appcardid, string userid = null)
        {
            var uri = userid == null ? BaseURL + "cards/registrations/" + appcardid : string.Format(BaseURL + "users/{0}/cards/registrations/" + appcardid, userid);

            var response = this.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var cardRegistration = response.Content.ReadAsAsync<CardRegistration>().Result;
            return cardRegistration;
        }

        public CardRegistration PostCard(CardRegistration cardRegistration, string userId = null)
        {
            var uri = userId == null ? BaseURL + "cards/registrations/" : string.Format(BaseURL + "users/{0}/cards/registrations/", userId);

            var response = this.PostAsJsonAsync<CardRegistration>(uri, cardRegistration).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            var card = response.Content.ReadAsAsync<CardRegistration>().Result;
            return card;
        }

        public HttpResponseMessage DeleteCard(string appcardid, string userId = null)
        {
            var uri = userId == null ? BaseURL + "cards/" + appcardid : string.Format(BaseURL + "users/{0}/cards/" + appcardid, userId);

            var response = this.DeleteAsync(uri).Result;
            if (!response.IsSuccessStatusCode) throw new APIException(response);
            return response;
        }
    }
}
