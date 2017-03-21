using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FunSharp.Games.Strawpoll
{
    public class StrawpollService
    {
        public static StrawpollService Instance { get; }

        /// <summary>
        ///  API INFORMATION - https://github.com/strawpoll/strawpoll/wiki/API
        /// </summary>
        private HttpClient m_client;
        public const string API_ENDPOINT = "https://strawpoll.me/api/v2/";

        private StrawpollService()
        {
            m_client = new HttpClient();
        }

        public async Task<StrawpollPoll> GetPoll(int id)
        {
            Uri endpoint = new Uri(API_ENDPOINT + $"polls/{id}");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var response = await m_client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                StrawpollPoll responsePoll = JsonConvert.DeserializeObject<StrawpollPoll>(responseText);
                return responsePoll;
            }

            return null;
        } 

        public async Task<StrawpollPoll> CreatePoll(StrawpollSettings settings)
        {
            StrawpollPoll postPoll = settings.CreatePoll();

            Uri endpoint = new Uri(API_ENDPOINT + $"polls");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint);
            requestMessage.Headers.Add("Content-Type", "application/json");
            requestMessage.Content = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("title", postPoll.title),
                            new KeyValuePair<string, string>("options", JsonConvert.SerializeObject(postPoll.options)),
                            new KeyValuePair<string, string>("multi", postPoll.multi.ToString()),
                        }
            );

            var response = await m_client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                StrawpollPoll responsePoll = JsonConvert.DeserializeObject<StrawpollPoll>(responseText);
                return responsePoll;
            }

            return null;
        }
    }
}
