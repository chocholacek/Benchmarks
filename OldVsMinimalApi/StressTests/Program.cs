using Contracts.TestData;
using Microsoft.FSharp.Core;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;

var clientFactory = HttpClientFactory.Create();

var baseAddress = "https://localhost";

var oldApiAll = Step.Create("old_api_all_step", clientFactory, async ctx => await Get(ctx, 5003));
var oldApiSpecific = Step.Create("old_api_specific_step", clientFactory, async ctx => await Get(ctx, 5003, $"/{PersonServiceTestData.TestPersonGuid}"));
var minimalApiAll = Step.Create("minimal_api_all_step", clientFactory, async ctx => await Get(ctx, 5001));
var minimalApiSpecific = Step.Create("minimal_api_specific_step", clientFactory, async ctx => await Get(ctx, 5001, $"/{PersonServiceTestData.TestPersonGuid}"));

NBomberRunner.RegisterScenarios(
    BasicScenario("old_api", oldApiAll, oldApiSpecific),
    BasicScenario("minimal_api", minimalApiAll, minimalApiSpecific)
).Run();

async Task<Response> Get(IStepContext<HttpClient, Unit> ctx, int port, string paramaters = "")
{
    var response = await ctx.Client.GetAsync($"{baseAddress}:{port}/person{paramaters}");

    return response.IsSuccessStatusCode
        ? Response.Ok(statusCode: (int)response.StatusCode)
        : Response.Fail(statusCode: (int)response.StatusCode);
}

Scenario BasicScenario(string name, params IStep[] steps) => ScenarioBuilder
    .CreateScenario(name, steps)
    .WithWarmUpDuration(TimeSpan.FromSeconds(5))
    .WithLoadSimulations(Simulation.KeepConstant(12, TimeSpan.FromSeconds(120)));
