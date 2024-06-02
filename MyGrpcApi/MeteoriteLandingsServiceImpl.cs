using CommonCode.Data;
using CommonCode.GRPC;
using Grpc.Core;

namespace MyGrpcApi
{
    public class MeteoriteLandingsServiceImpl : MeteoriteLandingsService.MeteoriteLandingsServiceBase
    {
        public override Task<CommonCode.GRPC.Version> GetVersion(EmptyRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CommonCode.GRPC.Version
            {
                ApiVersion = "API Version 1.0"
            });
        }

        public override async Task GetLargePayload(EmptyRequest request, IServerStreamWriter<MeteoriteLanding> responseStream, ServerCallContext context)
        {
            foreach (var meteoriteLanding in MeteoriteLandingData.GrpcMeteoriteLandings)
            {
                await responseStream.WriteAsync(meteoriteLanding);
            }
        }

        public override Task<MeteoriteLandingList> GetLargePayloadAsList(EmptyRequest request, ServerCallContext context)
        {
            return Task.FromResult(MeteoriteLandingData.GrpcMeteoriteLandingList);
        }

        public override Task<StatusResponse> PostLargePayload(MeteoriteLandingList request, ServerCallContext context)
        {
            return Task.FromResult(new StatusResponse { Status = "SUCCESS" });
        }
    }
}
