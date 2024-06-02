using MyGrpcApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo { Title = "gRPC Api via transcoding", Version = "v1" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
if (app.Environment.IsDevelopment())
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Grpc Transcoding API V1");
    });
app.MapGrpcService<MeteoriteLandingsServiceImpl>();
app.UseHsts();
app.Run();
