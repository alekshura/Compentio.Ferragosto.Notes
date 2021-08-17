namespace Compentio.Ferragosto.Api.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Compentio.Ferragosto.Notes;
    using System;

    public static class ServicesCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {            
            services.AddHealthChecks();


            // add new services below 
            services.AddTransient<INotesService, NotesService>();
        }
    }
}
