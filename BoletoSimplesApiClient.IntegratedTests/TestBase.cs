using BoletoSimplesApiClient.Common;
using BoletoSimplesApiClient.Utils;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Scotch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.IntegratedTests
{
    public class IntegratedTestBase
    {
        protected readonly BoletoSimplesClient Client;

        public IntegratedTestBase()
        {
            var customClient = new HttpCustomClient();

            Client = new BoletoSimplesClient(customClient, new ClientConnection());

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
                Converters = new List<JsonConverter> { new BrazilianCurrencyJsonConverter() }
            };
        }
    }

    public class HttpCustomClient : HttpClient
    {
        private readonly HttpClient ScotchHttpClient;
        public HttpCustomClient()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin", "").Replace("\\Debug", "").Replace("\\Release", "");
            var cassettePathFile = Path.Combine(baseDir, "recorded-requests", "http-requests-cassette.json");

            ScotchHttpClient = HttpClients.NewHttpClient(cassettePathFile, ScotchMode.Replaying);
        }

        public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancelationToken)
        {
            var contentHash = string.Empty;

            if (request.Content != null)
                contentHash = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

            var uuid = MD5Hash($"{request.Method}-{request.RequestUri.AbsoluteUri}-{contentHash.Trim()}");
            var newUri = QueryHelpers.AddQueryString(request.RequestUri.AbsoluteUri, "uuid", uuid);
            request.RequestUri = new Uri(newUri);
            return await ScotchHttpClient.SendAsync(request).ConfigureAwait(false);
        }

        private static string MD5Hash(string input)
        {
            var hash = new StringBuilder();
            using (var md5provider = new MD5CryptoServiceProvider())
            {
                var bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

                for (int i = 0; i < bytes.Length; i++)
                {
                    hash.Append(bytes[i].ToString("x2"));
                }
                return hash.ToString();
            }
        }
    }
}
