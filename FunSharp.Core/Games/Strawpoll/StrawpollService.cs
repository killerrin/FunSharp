using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FunSharp.Core.Games.Strawpoll
{
    public class StrawpollService
    {
        public static StrawpollService Instance { get; } = new StrawpollService();

        /// <summary>
        ///  API INFORMATION - https://github.com/strawpoll/strawpoll/wiki/API
        /// </summary>
        private HttpClient m_client;
        public const string API_ENDPOINT = "https://strawpoll.me/api/v2/";

        public HttpResponseMessage CurrentResponse { get; protected set; }

        private StrawpollService()
        {
            m_client = new HttpClient();
        }

        public async Task<StrawpollPoll> GetPoll(int id)
        {
            Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(GetPoll)} - Begin");

            Uri endpoint = new Uri(API_ENDPOINT + $"polls/{id}");
            Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(GetPoll)} - URI Made");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var response = await m_client.SendAsync(requestMessage);
            Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(GetPoll)} - Message Sent");

            CurrentResponse = response;

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(GetPoll)} - Message Recieved");
                Debug.WriteLine($"{responseText}");

                StrawpollPoll responsePoll = JsonConvert.DeserializeObject<StrawpollPoll>(responseText);
                Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(GetPoll)} - Deserialized");

                Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(GetPoll)} - End");
                return responsePoll;
            }

            return null;
        }

        public async Task<StrawpollPoll> PostPoll(StrawpollSettings settings)
        {
            Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(PostPoll)} - Begin");

            StrawpollPoll rawPoll = settings.CreatePoll();
            Uri endpoint = new Uri(API_ENDPOINT + $"polls");
            Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(PostPoll)} - URI Made");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint);
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(rawPoll), Encoding.UTF8, "application/json");

            var response = await m_client.SendAsync(requestMessage);
            Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(PostPoll)} - Message Sent");

            CurrentResponse = response;

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(PostPoll)} - Message Recieved");
                Debug.WriteLine($"{responseText}");

                StrawpollPoll responsePoll = JsonConvert.DeserializeObject<StrawpollPoll>(responseText);
                Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(PostPoll)} - Deserialized");

                Debug.WriteLine($"{nameof(StrawpollService)}: {nameof(PostPoll)} - End");
                return responsePoll;
            }

            return null;
        }
    }
}
