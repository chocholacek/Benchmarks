using MinimalApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointConfigurations();

using var app = builder.Build();
app.UseConfiguredEndpoints();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

await app.RunAsync();
