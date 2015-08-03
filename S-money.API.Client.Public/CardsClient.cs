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
        private const string cards = "cards";
        private const string cardsRegistration = "cards/registrations";

        public Card GetCard(string appcardid, string userId = null)
        {
            var uri = CreateUri(userId, cards);
            return GetAsync<Card>(uri + appcardid);
        }

        [Obsolete("Use GetCardRegistration")]
        public CardRegistration GetRegistration(string appcardid, string userId = null)
        {
            var uri = CreateUri(userId, cardsRegistration);
            return GetAsync<CardRegistration>(uri + appcardid);
        }

        public IEnumerable<Card> GetCards(string userId = null)
        {
            var uri = CreateUri(userId, cards);
            return GetAsync<IEnumerable<Card>>(uri);
        }

        public CardRegistration GetCardRegistration(string appcardid, string userId = null)
        {
            var uri = CreateUri(userId, cardsRegistration);
            return GetAsync<CardRegistration>(uri + appcardid);
        }

        public IEnumerable<CardRegistration> GetCardsRegistration(string userId = null)
        {
            var uri = CreateUri(userId, cardsRegistration);
            return GetAsync<IEnumerable<CardRegistration>>(uri);
        }

        public CardRegistration PostCard(CardRegistration cardRegistration, string userId = null)
        {
            var uri = CreateUri(userId, "");
            return PostAsync(uri, cardRegistration);
        }

        public HttpResponseMessage DeleteCard(string appcardid, string userId = null)
        {
            var uri = CreateUri(userId, cards);

            var response = this.DeleteAsync(uri + appcardid).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new APIException(response);
            }
            return response;
        }
    }
}