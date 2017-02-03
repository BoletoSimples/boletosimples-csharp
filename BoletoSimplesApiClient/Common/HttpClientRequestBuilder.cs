using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BoletoSimplesApiClient.Common
{
    internal sealed class HttpClientRequestBuilder
    {
        private readonly Uri _uri;
        private readonly HttpMethod _method;
        private readonly HttpContent _content;
        private readonly BoletoSimplesClient _client;
        private readonly Dictionary<string, string> _additionalHeaders = new Dictionary<string, string>();

        public HttpClientRequestBuilder(BoletoSimplesClient client) : this(client, null, null, null, new Dictionary<string, string>()) { }

        private HttpClientRequestBuilder(BoletoSimplesClient client,
                                         Uri resorceUri,
                                         HttpMethod method,
                                         HttpContent content,
                                         Dictionary<string, string> additionalHeaders)
        {
            _client = client;
            _uri = resorceUri;
            _method = method;
            _content = content;
            _additionalHeaders = additionalHeaders;
        }

        public HttpClientRequestBuilder To(Uri baseUri, string resourcePath)
        {
            var finalUri = CombinedUris(baseUri, resourcePath);
            return new HttpClientRequestBuilder(_client, finalUri, _method, _content, _additionalHeaders);
        }

        public HttpClientRequestBuilder WithMethod(HttpMethod method)
        {
            return new HttpClientRequestBuilder(_client, _uri, method, _content, _additionalHeaders);
        }

        public HttpClientRequestBuilder AppendQuery(Dictionary<string, string> queryStringParameters)
        {
            if (queryStringParameters.Any())
            {
                var queryString = string.Join("&", queryStringParameters.Select(p => string.Format("{0}={1}", p.Key, p.Value)));
                var completeUri = new Uri(Uri.EscapeUriString($"{_uri.AbsoluteUri}?{queryString}"));
                return new HttpClientRequestBuilder(_client, completeUri, _method, _content, _additionalHeaders);
            }

            return this;
        }

        public HttpClientRequestBuilder AndOptionalContent(object content)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
            };

            var jsonContent = JsonConvert.SerializeObject(content);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            return new HttpClientRequestBuilder(_client, _uri, _method, stringContent, _additionalHeaders);
        }

        public HttpClientRequestBuilder AditionalHeaders(Dictionary<string, string> additionalHeaders)
        {
            return new HttpClientRequestBuilder(_client, _uri, _method, _content, additionalHeaders);
        }

        public HttpRequestMessage Build()
        {
            var message = new HttpRequestMessage(_method, _uri) { Content = _content };
            message.Headers.Authorization = GetAuthHeader();
            message.Headers.Add("User-Agent", _client.Connection.UserAgent);

            foreach (var header in _additionalHeaders)
            {
                message.Headers.Add(header.Key, header.Value);
            }

            return message;
        }

        private static Uri CombinedUris(Uri baseUri, string resourcePath)
        {
            if (string.IsNullOrEmpty(resourcePath))
            {
                return baseUri;
            }

            var finalUri = resourcePath.First().Equals('/') ? $"{baseUri.ToString()}{resourcePath}" : $"{baseUri.ToString()}/{resourcePath}";

            return new Uri(finalUri);
        }

        private AuthenticationHeaderValue GetAuthHeader()
        {
            var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_client.Connection.ApiToken}:X"));
            return new AuthenticationHeaderValue("Basic", authToken);
        }

    }
}
