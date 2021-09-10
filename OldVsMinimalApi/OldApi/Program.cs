using Contracts.Services;
using Microsoft.OpenApi.Models;
using Contracts.TestData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPersonService, PersonService>(_ => new(PersonServiceTestData.TestDb));

builder.Services.AddControllers();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();
