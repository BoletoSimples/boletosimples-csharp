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
        private Discharge Content { get; set; }

        [SetUp]
        public void PrepareTest()
        {
            Content = JsonConvert.DeserializeObject<Discharge>(JsonConstants.Discharge);
        }


        [Test]
        public async Task Create_discharge_file_with_success()
        {
            // Arrange
            var file = new FileStream($"{BaseDir}/test-assets/arquivo-retorno.ret", FileMode.Open);

            // Act
            var resposta = await Client.DischargesApi.PostAsync("arquivo-test.ret", file).ConfigureAwait(false);
            var sucessResponse = await resposta.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(sucessResponse, Is.Not.Null);
            Assert.That(sucessResponse.Id, Is.GreaterThan(0));
        }


        [Test]
        public async Task Get_discharge_file_information_with_success()
        {
            // Act
            var response = await Client.DischargesApi.GetAsync(177).ConfigureAwait(false);
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
            var resposta = await Client.DischargesApi.PostAsync("arquivo-test.ret", file).ConfigureAwait(false);
            var sucessCreateResponse = await resposta.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var getResponse = await Client.DischargesApi.GetAsync(1, 250).ConfigureAwait(false);
            var getSucessResponse = await getResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            Assert.That(getResponse.IsSuccess, Is.True);
            Assert.That(getSucessResponse.Items, Is.Not.Empty);
        }

        [Test]
        public async Task Payoff_discharge_file_with_success()
        {
            // Arrange
            var getResponse = await Client.DischargesApi.GetAsync(1, 250).ConfigureAwait(false);
            var getSucessResponse = await getResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var payOff = await Client.DischargesApi.PayOffAsync(getSucessResponse.Items.First().Id).ConfigureAwait(false);
            var payOffSucessResponse = await payOff.GetSuccessResponseAsync().ConfigureAwait(false);

            Assert.That(payOff.IsSuccess, Is.True);
            Assert.That(payOff.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            payOffSucessResponse.ShouldBeEquivalentTo(new Discharge());
        }
    }
}
