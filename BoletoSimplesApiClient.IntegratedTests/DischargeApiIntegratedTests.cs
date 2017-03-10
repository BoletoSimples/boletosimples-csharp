using BoletoSimplesApiClient.APIs.Discharges.Models;
using BoletoSimplesApiClient.UnitTests.Json;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;

namespace BoletoSimplesApiClient.IntegratedTests
{
    [TestFixture]
    public class DischargeApiIntegratedTests : IntegratedTestBase
    {
        [Test]
        public async Task Create_discharge_file_with_success()
        {
            // Arrange
            var file = new FileStream($"{BaseDir}/test-assets/arquivo-retorno.ret", FileMode.Open);

            // Act
            var resposta = await Client.Discharges.PostAsync("arquivo-test.ret", file).ConfigureAwait(false);
            var sucessResponse = await resposta.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(sucessResponse, Is.Not.Null);
            Assert.That(sucessResponse.Id, Is.GreaterThan(0));
        }


        [Test]
        public async Task Get_discharge_file_information_with_success()
        {
            // Act
            var response = await Client.Discharges.GetAsync(177).ConfigureAwait(false);
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse.Filename, Is.EqualTo("arquivo-test.ret"));
        }

        [Test]
        public async Task List_discharge_files_paged_with_success()
        {
            // Arrange
            var file = new FileStream($"{BaseDir}/test-assets/arquivo-retorno.ret", FileMode.Open);
            var resposta = await Client.Discharges.PostAsync("arquivo-test.ret", file).ConfigureAwait(false);
            var sucessCreateResponse = await resposta.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var getResponse = await Client.Discharges.GetAsync(1, 250).ConfigureAwait(false);
            var getSucessResponse = await getResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(getResponse.IsSuccess, Is.True);
            Assert.That(getSucessResponse.Items, Is.Not.Empty);
        }

        [Test]
        public async Task Payoff_discharge_file_with_success()
        {
            // Arrange
            var getResponse = await Client.Discharges.GetAsync(1, 250).ConfigureAwait(false);
            var getSucessResponse = await getResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var payOffResponse = await Client.Discharges.PayOffAsync(getSucessResponse.Items.First().Id).ConfigureAwait(false);
            var content = await payOffResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(payOffResponse.IsSuccessStatusCode, Is.True);
            Assert.That(payOffResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(content, Is.Empty);
        }
    }
}
