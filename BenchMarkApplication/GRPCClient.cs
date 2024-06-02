using CommonCode.GRPC;
using Grpc.Core;
using Grpc.Net.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static CommonCode.GRPC.MeteoriteLandingsService;

namespace BenchMarkApplication
{
    public class GRPCClient
    {
        private readonly ChannelBase channel;
        private readonly MeteoriteLandingsServiceClient client;
        private static readonly HttpClient httpClient = new HttpClient()
        {
            DefaultRequestVersion = HttpVersion.Version20
        };

        public GRPCClient()
        {
            channel = GrpcChannel.ForAddress("https://localhost:7056");
            client = new MeteoriteLandingsServiceClient(channel);
        }

        public async Task<string> GetSmallPayloadAsync()
        {
            return (await client.GetVersionAsync(new EmptyRequest())).ApiVersion;
        }

        public async Task<List<MeteoriteLanding>> StreamLargePayloadAsync()
        {
            List<MeteoriteLanding> meteoriteLandings = new List<MeteoriteLanding>();
            var response = client.GetLargePayload(new EmptyRequest()).ResponseStream;
            while (await response.MoveNext())
            {
                meteoriteLandings.Add(response.Current);
            }

            return meteoriteLandings;
        }

        public async Task<IList<MeteoriteLanding>> GetLargePayloadAsListAsync()
        {
            return (await client.GetLargePayloadAsListAsync(new EmptyRequest())).MeteoriteLandings;
        }

        public async Task<string> PostLargePayloadAsync(MeteoriteLandingList meteoriteLandings)
        {
            return (await client.PostLargePayloadAsync(meteoriteLandings)).Status;
        }

        public async Task<string> GetSmallPayloadAsyncViaApi()
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await httpClient.GetStringAsync("https://localhost:7056/api/getversion");
        }

        public async Task<string> PostLargePayloadAsyncViaApi(MeteoriteLandingList meteoriteLandings)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.PostAsJsonAsync("https://localhost:7056/api/postlargepayload", meteoriteLandings);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
