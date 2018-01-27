using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace BoletoSimplesApiClient.Common
{
    internal sealed class HttpClientRequestBuilder
    {
        private readonly Uri _uri;
        private readonly HttpMethod _method;
        private readonly HttpContent _content;
        private readonly BoletoSimplesClient _client;
        private readonly Dictionary<string, string> _additionalHeaders;
        private readonly JsonSerializerSettings _jsonDefaultSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

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
            if (!queryStringParameters.Any())
                return this;

            var queryString = string.Join("&", queryStringParameters.Select(p => $"{p.Key}={p.Value}"));
            var completeUri = new Uri(Uri.EscapeUriString($"{_uri.AbsoluteUri}?{queryString}"));
            return new HttpClientRequestBuilder(_client, completeUri, _method, _content, _additionalHeaders);
        }

        /// <summary>
        /// Adiciona objeto como conteudo, não deve ser utilizado para arquivos
        /// </summary>
        /// <param name="content">objeto representando o modelo a ser enviado</param>
        /// <param name="customOptionalSettings">Método de serialização opcional, caso não seja enviado o default para os contratos da boleto simples será</param>
        /// <returns></returns>
        public HttpClientRequestBuilder AndOptionalContent(object content, JsonSerializerSettings customOptionalSettings = null)
        {
            var jsonContent = JsonConvert.SerializeObject(content, customOptionalSettings ?? _jsonDefaultSettings);
            var rootNameValue = JsonRootAttribute.GetAttributeValue(content.GetType());

            if (!string.IsNullOrEmpty(rootNameValue))
                jsonContent = $"{{\"{rootNameValue}\": {jsonContent}}}";

            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            return new HttpClientRequestBuilder(_client, _uri, _method, stringContent, _additionalHeaders);
        }

        /// <summary>
        /// Representa o arquivo a ser enviado ao servidor
        /// </summary>
        /// <param name="paramKey">nome do parâmetro que representa o arquivo</param>
        /// <param name="fileName">nome do arquivo</param>
        /// <param name="paramFileStream">stream do conteudo arquivo</param>
        /// <returns></returns>
        public HttpClientRequestBuilder AppendFileContent(string paramKey, string fileName, Stream paramFileStream)
        {
            HttpContent fileStreamContent = new StreamContent(paramFileStream);
            var content = new MultipartFormDataContent { { fileStreamContent, paramKey, fileName } };
            return new HttpClientRequestBuilder(_client, _uri, _method, content, _additionalHeaders);
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

            var baseUrl = baseUri.AbsoluteUri;
            var concatenatedUrls = resourcePath.First() == '/' || baseUrl.Last() == '/' ?
                                                $"{baseUrl}{resourcePath}" :
                                                $"{baseUrl}/{resourcePath}";
            return new Uri(concatenatedUrls);
        }

        private AuthenticationHeaderValue GetAuthHeader()
        {
            var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_client.Connection.ApiToken}:X"));
            return new AuthenticationHeaderValue("Basic", authToken);
        }
    }
}
