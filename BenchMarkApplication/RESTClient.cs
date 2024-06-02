using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using CommonCode.REST;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BenchMarkApplication
{
    public class RESTClient
    {
        private static readonly HttpClient client = new HttpClient()
        {
            DefaultRequestVersion = HttpVersion.Version20
        };

        public async Task<string> GetSmallPayloadAsync()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await client.GetStringAsync("https://localhost:7140");
        }

        public async Task<List<MeteoriteLanding>> GetLargePayloadAsync()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string meteoriteLandingsString = await client.GetStringAsync("https://localhost:7140/LargePayload");

            return JsonConvert.DeserializeObject<List<MeteoriteLanding>>(meteoriteLandingsString);
        }

        public async Task<string> PostLargePayloadAsync(List<MeteoriteLanding> meteoriteLandings)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("https://localhost:7140/LargePayload", meteoriteLandings);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
