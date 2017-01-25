using BoletoSimplesApiClient.APIs.Auth.ResponseMessages;
using BoletoSimplesApiClient.Common;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.IntegratedTests
{
    [TestFixture]
    public class BoletoSimplesClientIntegratedTests
    {
        [Test]
        public async Task Establish_connection_by_access_token_with_sucess()
        {
            // Arrange
            var client = new BoletoSimplesClient();

            // Act
            var response = await client.Auth.GetUserInfoAsync().ConfigureAwait(false);
            var successResponse = await response.GeResponseAsync().ConfigureAwait(false);
            client.Dispose();

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(successResponse, Is.InstanceOf<UserInfoResponseMessage>());
        }

        [Test]
        public async Task When_establish_connection_with_invalid_token_should_return_unauthorize_code()
        {

            // Arrange
            var invalidConnection = new ClientConnection(ConfigurationManager.AppSettings["boletosimple-api-url"],
                                                         ConfigurationManager.AppSettings["boletosimple-api-version"],
                                                         Guid.NewGuid().ToString(),
                                                         ConfigurationManager.AppSettings["boletosimple-useragent"],
                                                         ConfigurationManager.AppSettings["boletosimple-api-return-url"],
                                                         ConfigurationManager.AppSettings["boletosimple-api-client-id"],
                                                         ConfigurationManager.AppSettings["boletosimple-api-client-secret"]);

            var client = new BoletoSimplesClient(new HttpClient(), invalidConnection);

            // Act
            var response = await client.Auth.GetUserInfoAsync().ConfigureAwait(false);
            var successResponse = await response.GeResponseAsync().ConfigureAwait(false);
            client.Dispose();

            // Assert
            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorResponse.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            successResponse.Should().Equals(new UserInfoResponseMessage());
        }
    }
}
