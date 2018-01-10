using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Smoney.API.Client.Models.Users;

namespace Smoney.API.Client
{
    public partial class APIClient
    {
        private const string cards = "cards";
        private const string cardsRegistration = "cards/registrations";

        public async Task<Card> GetCard(string appcardid, string userId = null)
        {
            var uri = CreateUri(userId, cards);
            return await GetAsync<Card>(uri + appcardid);
        }

        [Obsolete("Use GetCardRegistration")]
        public CardRegistration GetRegistration(string appcardid, string userId = null)
        {
            var uri = CreateUri(userId, cardsRegistration);
            return GetAsync<CardRegistration>(uri + appcardid).Result;
        }

        public async Task<IEnumerable<Card>> GetCards(string userId = null)
        {
            var uri = CreateUri(userId, cards);
            return await GetAsync<IEnumerable<Card>>(uri);
        }

        public async Task<CardRegistration> GetCardRegistration(string appcardid, string userId = null)
        {
            var uri = CreateUri(userId, cardsRegistration);
            return await GetAsync<CardRegistration>(uri + appcardid);
        }

        public async Task<IEnumerable<CardRegistration>> GetCardsRegistration(string userId = null)
        {
            var uri = CreateUri(userId, cardsRegistration);
            return await GetAsync<IEnumerable<CardRegistration>>(uri);
        }

        public async Task<CardRegistration> PostCard(CardRegistration cardRegistration, string userId = null)
        {
            var uri = CreateUri(userId, "");
            return await PostAsync(uri, cardRegistration);
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