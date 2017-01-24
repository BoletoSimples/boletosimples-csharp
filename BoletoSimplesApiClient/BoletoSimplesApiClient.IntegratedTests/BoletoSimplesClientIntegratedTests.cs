using BoletoSimplesApiClient;
using BoletoSimplesApiClient.APIs.Auth.ResponseMessages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(successResponse, Is.InstanceOf<UserInfoResponseMessage>());
        }

        [Test]
        public async Task When_establish_connection_with_error_should_return_unaouthorize_code() { }
    }
}
