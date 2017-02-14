using BoletoSimplesApiClient.Common;
using BoletoSimplesApiClient.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Scotch;
using System;
using System.Collections.Generic;
using System.IO;

namespace BoletoSimplesApiClient.IntegratedTests
{
    public class IntegratedTestBase
    {
        protected readonly BoletoSimplesClient Client;

        public IntegratedTestBase()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin", "").Replace("\\Debug", "").Replace("\\Release", "");
            var cassettePathFile = Path.Combine(baseDir, "recorded-requests","http-requests-cassette.log");

            Client = new BoletoSimplesClient(HttpClients.NewHttpClient(cassettePathFile, ScotchMode.Replaying), new ClientConnection());

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
                Converters = new List<JsonConverter> { new BrazilianCurrencyJsonConverter() }
            };
        }
    }
}
