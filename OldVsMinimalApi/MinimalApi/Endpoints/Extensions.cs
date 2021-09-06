using System.Reflection;

namespace MinimalApi.Endpoints
{
    public static class Extensions
    {
        public static IServiceCollection AddEndpointConfigurations(this IServiceCollection services)
        {
            var configures = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IEndpointConfiguration).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointConfiguration>()
                .ToList();

            configures.ForEach(c => c.ConfigureServices(services));

            services.AddSingleton(configures as IReadOnlyCollection<IEndpointConfiguration>);

            return services;
        }

        public static WebApplication UseConfiguredEndpoints(this WebApplication app)
        {
            var configures = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointConfiguration>>();

            foreach (var config in configures)
            {
                config.DefineEndpoints(app);
            }

            return app;
        }
    }
}