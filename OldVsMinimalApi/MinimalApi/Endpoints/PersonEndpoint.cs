using Contracts.Models;
using Contracts.Services;
using Contracts.TestData;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Endpoints
{
    public class PersonEndpoint : IEndpointConfiguration
    {
        public string Base { get; } = "person";

        public IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPersonService, PersonService>(_ => new(PersonServiceTestData.TestDb));

            return services;
        }

        public WebApplication DefineEndpoints(WebApplication app)
        {
            app.MapGet($"/{Base}", GetAllPeople);
            app.MapGet($"/{Base}/{{id}}", GetPerson);
            app.MapPost($"/{Base}", AddOrUpdatePerson);

            return app;
        }

        internal IResult GetAllPeople(IPersonService service)
        {
            var result = service.GetAll();
            return result.Any() ? Results.Ok(result) : Results.NoContent();
        }

        internal IResult GetPerson(IPersonService service, Guid id)
        {
            var person = service.Get(id);
            return person is { } ? Results.Ok(person) : Results.NotFound();
        }

        internal IResult AddOrUpdatePerson(IPersonService service, [FromBody] Person person) => Results.Ok(service.AddOrUpdate(person));
    }
}