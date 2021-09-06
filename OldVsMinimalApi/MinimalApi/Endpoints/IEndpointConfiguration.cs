namespace MinimalApi.Endpoints
{
    public interface IEndpointConfiguration
    {
        string Base { get; }

        IServiceCollection ConfigureServices(IServiceCollection services);

        WebApplication DefineEndpoints(WebApplication app);
    }
}