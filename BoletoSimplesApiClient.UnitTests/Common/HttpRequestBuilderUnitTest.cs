using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BoletoSimplesApiClient.Common;
using System.IO;

namespace BoletoSimplesApiClient.UnitTests.Common
{
    [TestFixture]
    public class HttpRequestBuilderUnitTest
    {
        private readonly HttpClientRequestBuilder Builder;

        public HttpRequestBuilderUnitTest()
        {
            var client = new BoletoSimplesClient();
            Builder = new HttpClientRequestBuilder(client);
        }

        [Test]
        public async Task Build_a_complex_http_request_message_with_json_body_custom_header_and_query_string()
        {
            // Arrange and Act
            var buildedRequestMessage = Builder.To(new Uri("http://any-uri.io"), "any-resource")
                                               .WithMethod(HttpMethod.Post)
                                               .AndOptionalContent(new { AnyContent = "Any Content" })
                                               .AppendQuery(new Dictionary<string, string> { ["query"] = "any" })
                                               .AditionalHeaders(new Dictionary<string, string> { ["My-Header"] = "Any Value" })
                                               .Build();

            var content = await buildedRequestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(buildedRequestMessage.RequestUri, Is.EqualTo(new Uri("http://any-uri.io/any-resource?query=any")));
            Assert.That(buildedRequestMessage.Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(content, Does.Contain("Any Content"));
            Assert.That(buildedRequestMessage.Headers.Authorization, Is.Not.Null);
            Assert.That(buildedRequestMessage.Headers.GetValues("My-Header").First(), Is.EqualTo("Any Value"));
        }

        [Test]
        public async Task Build_a_http_request_message_with_file_attached_to_body()
        {
            // Arrange 
            var file = await GenerateStreamFromString("Any File In Memory Content").ConfigureAwait(false);

            // Act
            var buildedRequestMessage = Builder.To(new Uri("http://any-uri.io"), "any-file-resource")
                                               .WithMethod(HttpMethod.Post)
                                               .AppendFileContent("file", "any-file.txt", file)
                                               .Build();

            var content = await buildedRequestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(buildedRequestMessage.RequestUri, Is.EqualTo(new Uri("http://any-uri.io/any-file-resource")));
            Assert.That(buildedRequestMessage.Method, Is.EqualTo(HttpMethod.Post));
            Assert.That(buildedRequestMessage.Content, Is.InstanceOf<MultipartFormDataContent>());
            Assert.That(content, Does.Contain("Any File In Memory Content"));
            Assert.That(buildedRequestMessage.Headers.Authorization, Is.Not.Null);
        }

        [Test]
        public async Task Build_a_simple_http_request_message_without_body()
        {
            // Arrange and Act
            var buildedRequestMessage = Builder.To(new Uri("http://any-uri.io"), "any-resource")
                                               .WithMethod(HttpMethod.Get)
                                               .Build();
            // Assert
            Assert.That(buildedRequestMessage.RequestUri, Is.EqualTo(new Uri("http://any-uri.io/any-resource")));
            Assert.That(buildedRequestMessage.Method, Is.EqualTo(HttpMethod.Get));
            Assert.That(buildedRequestMessage.Content, Is.Null);
            Assert.That(buildedRequestMessage.Headers.Authorization, Is.Not.Null);
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
