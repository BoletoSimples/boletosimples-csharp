using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BoletoSimplesApiClient.Common;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BoletoSimplesApiClient.UnitTests.Common
{
    [TestFixture]
    public class HttpRequestBuilderUnitTest
    {
        private HttpClientRequestBuilder _builder;

        [OneTimeSetUp]
        public void InitTests()
        {
            var connection = new ClientConnection(@"https://sandbox.boletosimples.com.br/api", "v1",
                "83ccd60a3bde2f2ea5dbab40bd2acaf2d7aa3bc769eb5a9bcb55ceaf0f9c3c3c",
                "Meu e-Commerce (meuecommerce@example.com)", null, null, null);
            var client = new BoletoSimplesClient(connection);
            _builder = new HttpClientRequestBuilder(client);
        }

        [Test]
        public async Task Build_a_complex_http_request_message_with_json_body_custom_header_and_query_string()
        {
            // Arrange and Act
            var requestMessage = _builder.To(new Uri("http://any-uri.io"), "any-resource")
                                         .WithMethod(HttpMethod.Post)
                                         .AndOptionalContent(new { AnyContent = "Any Content" })
                                         .AppendQuery(new Dictionary<string, string> { ["query"] = "any" })
                                         .AditionalHeaders(new Dictionary<string, string> { ["My-Header"] = "Any Value" })
                                         .Build();

            var content = await requestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(requestMessage.RequestUri, Is.EqualTo(new Uri("http://any-uri.io/any-resource?query=any")));
            Assert.That(requestMessage.Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(content, Does.Contain("Any Content"));
            Assert.That(requestMessage.Headers.Authorization, Is.Not.Null);
            Assert.That(requestMessage.Headers.GetValues("My-Header").First(), Is.EqualTo("Any Value"));
        }

        [Test]
        public async Task Build_a_complex_http_request_message_with_json_body_and_custom_body_serializer_settings()
        {
            // Arrange and Act
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            var requestMessage = _builder.To(new Uri("http://any-uri.io"), "any-resource")
                                         .WithMethod(HttpMethod.Post)
                                         .AndOptionalContent(new { ANY_CONTENT = "Any Content" })
                                         .Build();

            var customSerializedResponse = _builder.To(new Uri("http://any-uri.io"), "any-resource")
                                                   .WithMethod(HttpMethod.Post)
                                                   .AndOptionalContent(new { AnyContent = "Any Content" }, settings)
                                                   .Build();

            var content = await requestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var contentCustomSerializedResponse = await customSerializedResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(content, Does.Contain("any_content"));
            Assert.That(contentCustomSerializedResponse, Does.Contain("anyContent"));
        }

        [Test]
        public async Task Build_a_http_request_message_with_file_attached_to_body()
        {
            // Arrange 
            var file = await GenerateStreamFromString("Any File In Memory Content").ConfigureAwait(false);

            // Act
            var requestMessage = _builder.To(new Uri("http://any-uri.io"), "any-file-resource")
                                         .WithMethod(HttpMethod.Post)
                                         .AppendFileContent("file", "any-file.txt", file)
                                         .Build();

            var content = await requestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(requestMessage.RequestUri, Is.EqualTo(new Uri("http://any-uri.io/any-file-resource")));
            Assert.That(requestMessage.Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(requestMessage.Content, Is.InstanceOf<MultipartFormDataContent>());
            Assert.That(content, Does.Contain("Any File In Memory Content"));
            Assert.That(requestMessage.Headers.Authorization, Is.Not.Null);
        }

        [Test]
        public void Build_a_simple_http_request_message_without_body()
        {
            // Arrange and Act
            var requestMessage = _builder.To(new Uri("http://any-uri.io"), "any-resource")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();
            // Assert
            Assert.That(requestMessage.RequestUri, Is.EqualTo(new Uri("http://any-uri.io/any-resource")));
            Assert.That(requestMessage.Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(requestMessage.Content, Is.Null);
            Assert.That(requestMessage.Headers.Authorization, Is.Not.Null);
        }

        private static async Task<Stream> GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            await writer.WriteLineAsync(s).ConfigureAwait(false);
            await writer.FlushAsync();
            stream.Position = 0;

            return stream;
        }
    }
}
