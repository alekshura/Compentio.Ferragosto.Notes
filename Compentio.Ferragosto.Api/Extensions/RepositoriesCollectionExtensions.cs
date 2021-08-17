namespace Compentio.Ferragosto.Api.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Compentio.Ferragosto.Notes;

    public static class RepositoriesCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            // TODO add repositories here
            services.AddSingleton<INotesRepository, NotesRepository>();
        }
    }
}
