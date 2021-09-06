using MinimalApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointConfigurations();

using var app = builder.Build();
app.UseConfiguredEndpoints();

await app.RunAsync();
