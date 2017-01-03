using System;
using System.Configuration;

namespace BoletoSimplesApiClient
{
    public class ClientConnection
    {
        public readonly string UserAgent;
        public readonly string ApiToken;
        public readonly string ApiVersion;
        public readonly string ApiUrl;
        public readonly string ApiReturnUrl;
        public readonly string ClientId;
        public readonly string ClientSecret;

        public ClientConnection() : this(ConfigurationManager.AppSettings["boletosimple-api-version"],
                                         ConfigurationManager.AppSettings["boletosimple-api-url"],
                                         ConfigurationManager.AppSettings["boletosimple-api-token"],
                                         ConfigurationManager.AppSettings["boletosimple-useragent"],
                                         ConfigurationManager.AppSettings["boletosimple-api-return-url"],
                                         ConfigurationManager.AppSettings["boletosimple-api-client-id"],
                                         ConfigurationManager.AppSettings["boletosimple-api-client-secret"])
        { }

        public ClientConnection(string apiUrl, string apiVersion, string apiToken, string userAgent, string apiReturnUrl, string clientId, string clientSecret)
        {
            ApiUrl = apiUrl;
            ApiVersion = apiVersion;
            ApiToken = apiToken;
            UserAgent = userAgent;
            ApiReturnUrl = apiReturnUrl;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public bool IsOAuthConnection() => !(string.IsNullOrEmpty(ClientId) && string.IsNullOrEmpty(ClientSecret));
        public Uri GetBaseUri() => new Uri($"{ApiUrl}/{ApiVersion}");
    }
}
