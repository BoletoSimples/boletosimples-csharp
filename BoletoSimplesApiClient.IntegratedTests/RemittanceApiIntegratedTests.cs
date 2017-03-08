using BoletoSimplesApiClient.APIs.BankBillets.Models;
using BoletoSimplesApiClient.APIs.Remittances.Models;
using BoletoSimplesApiClient.UnitTests.Json;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.IntegratedTests
{
    [TestFixture]
    public class RemittanceApiIntegratedTests : IntegratedTestBase
    {
        private BankBillet SuccessResponse;

        [SetUp]
        public async Task InitTests()
        {
            var content = JsonConvert.DeserializeObject<BankBillet>(JsonConstants.BankBillet);
            content.BankBilletAccountId = 337;
            content.BeneficiaryName = "Created-Beneficiary-Name";

            var response = await Client.BankBillets.PostAsync(content).ConfigureAwait(false);
            SuccessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);
        }


        [Test]
        public async Task Create_remittance_file_with_success()
        {
            // Arrange
            var remittance = new Remittance(337);

            // Act
            var response = await Client.RemittanceApi.PostAsync(remittance).ConfigureAwait(false);
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse, Is.Not.Null);
        }


        [Test]
        public async Task Get_remittance_file_with_success()
        {
            // Arrange
            var remittance = new Remittance(337);
            var responseCreate = await Client.RemittanceApi.PostAsync(remittance).ConfigureAwait(false);
            var sucessCreateResponse = await responseCreate.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var response = await Client.RemittanceApi.GetAsync(sucessCreateResponse.Id).ConfigureAwait(false);
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse.Filename, Does.Contain(".REM"));
        }

        [Test]
        public async Task List_remittances_with_sucess()
        {
            // Arrange
            var response = await Client.RemittanceApi.GetAsync(1, 250).ConfigureAwait(false);

            // Act
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse.Items, Is.Not.Empty);
        }

        [Test]
        public async Task Delete_remittances_with_sucess()
        {
            // Arrange
            var responseList = await Client.RemittanceApi.GetAsync(457).ConfigureAwait(false);
            var sucessResponseList = await responseList.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var deleteSuccessResponse = await Client.RemittanceApi.DeleteAsync(sucessResponseList.Id).ConfigureAwait(false);
            var content = await deleteSuccessResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(deleteSuccessResponse.IsSuccessStatusCode, Is.True);
            Assert.That(deleteSuccessResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(content, Is.Empty);
        }
    }
}
