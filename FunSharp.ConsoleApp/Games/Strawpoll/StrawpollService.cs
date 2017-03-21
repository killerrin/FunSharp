using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FunSharp.Games.Strawpoll
{
    public class StrawpollService
    {
        public static StrawpollService Instance { get; } = new StrawpollService();

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
            Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(GetPoll)} - Message Sent");

            var responseText = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(GetPoll)} - Message Recieved");
            Debug.WriteLine($"{responseText}");

            if (response.IsSuccessStatusCode)
            {
                StrawpollPoll responsePoll = JsonConvert.DeserializeObject<StrawpollPoll>(responseText);
                Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(GetPoll)} - Deserialized");

                return responsePoll;
            }

            return new StrawpollPoll();
        } 

        public async Task<StrawpollPoll> PostPoll(StrawpollSettings settings)
        {
            StrawpollPoll rawPoll = settings.CreatePoll();
            Uri endpoint = new Uri(API_ENDPOINT + $"polls");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint);
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(rawPoll), Encoding.UTF8, "application/json");

            var response = await m_client.SendAsync(requestMessage);
            Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(PostPoll)} - Message Sent");

            var responseText = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(PostPoll)} - Message Recieved");
            Debug.WriteLine($"{responseText}");

            if (response.IsSuccessStatusCode)
            {
                StrawpollPoll responsePoll = JsonConvert.DeserializeObject<StrawpollPoll>(responseText);
                Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(PostPoll)} - Deserialized");
                
                return responsePoll;
            }

            return new StrawpollPoll();
        }
    }
}
