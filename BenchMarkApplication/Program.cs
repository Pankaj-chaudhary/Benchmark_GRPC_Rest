using BenchMarkApplication;
using BenchmarkDotNet.Running;
using CommonCode.GRPC;
using Grpc.Core;
using Grpc.Net.Client;
using System.Net;
using System.Net.Http.Headers;
using static CommonCode.GRPC.MeteoriteLandingsService;

BenchmarkRunner.Run<BenchmarkHarness>();
//using var channel = GrpcChannel.ForAddress("https://localhost:7056");
//var client = new MeteoriteLandingsServiceClient(channel);
//await Task.Delay(5000);
//Console.WriteLine((await client.GetVersionAsync(new EmptyRequest())).ApiVersion);

//var client = new HttpClient()
//{
//    DefaultRequestVersion = HttpVersion.Version20
//};
//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//await Task.Delay(5000);
//Console.WriteLine(await client.GetStringAsync("https://localhost:7140"));
Console.ReadKey();