using CommonCode.Data;
using CommonCode.REST;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello to the Rest API V1!");

app.MapGet("/largepayload", () =>
{
    return MeteoriteLandingData.RestMeteoriteLandings;
})
.WithName("GetLargePayloadAsync")
.WithOpenApi();



app.MapPost("/largepayload", ([FromBody] IEnumerable<MeteoriteLanding> meteoriteLandings) =>
{
    return "SUCCESS";
})
.WithName("PostLargePayload")
.WithOpenApi();
app.UseHsts();
app.Run();
