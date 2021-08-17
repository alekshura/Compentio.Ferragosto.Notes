namespace Compentio.Ferragosto.Api.Configuration
{
    using Compentio.Ferragosto.Notes;

    public class MongoDbOptions : IMongoDbOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
