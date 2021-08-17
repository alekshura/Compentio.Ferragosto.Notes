namespace Compentio.Ferragosto.Api.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Compentio.Ferragosto.Api.Configuration;
    using Microsoft.Extensions.Options;
    using Compentio.Ferragosto.Notes;

    public static class ConfigurationsCollectionExtensions
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Add configuration here

            services.Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)));
            services.AddSingleton<IMongoDbOptions>(sp =>
                sp.GetRequiredService<IOptions<MongoDbOptions>>().Value);

        }
    }
}
