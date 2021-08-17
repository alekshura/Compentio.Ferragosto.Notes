namespace Compentio.Ferragosto.Api.Configuration
{
    public class AzureAdOptions
    {
        public string ClientId { get; set; }
        public string ApiScopes { get; set; }
        public string TokenUrl { get; set; }
        public string AuthorizationUrl { get; set; }
    }
}
