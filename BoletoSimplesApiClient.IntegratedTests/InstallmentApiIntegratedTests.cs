using BoletoSimplesApiClient.APIs.Installments.Models;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.IntegratedTests
{
    [TestFixture]
    public class InstallmentApiIntegratedTests : IntegratedTestBase
    {
        [Test]
        public async Task Create_installment_with_success()
        {
            // Arrange
            var installment = new Installment(1, 1999.55m, new DateTime(2050, 1, 1), 12, "Any Subscription");

            // Act
            var response = await Client.Installments.PostAsync(installment).ConfigureAwait(false);
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse, Is.Not.Null);
        }


        [Test]
        public async Task Get_installment_information_with_success()
        {
            // Arrange
            var installment = new Installment(1, 9999.55m, new DateTime(2050, 1, 1), 12, "Any Subscription");
            var responseCreate = await Client.Installments.PostAsync(installment).ConfigureAwait(false);
            var sucessCreateResponse = await responseCreate.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var response = await Client.Installments.GetAsync(sucessCreateResponse.Id).ConfigureAwait(false);
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse.Amount, Is.EqualTo(9999.55m));
            Assert.That(sucessResponse.Total, Is.EqualTo(12));
        }

        [Test]
        public async Task List_installments_with_sucess()
        {
            // Arrange
            var response = await Client.Installments.GetAsync(1, 250).ConfigureAwait(false);

            // Act
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse.Items, Is.Not.Empty);
        }

        [Test]
        public async Task Delete_installment_with_sucess()
        {
            // Arrange
            var installment = new Installment(1, 300.55m, new DateTime(2050, 1, 1), 1, "Any Subscription");
            var responseCreate = await Client.Installments.PostAsync(installment).ConfigureAwait(false);
            var sucessCreateResponse = await responseCreate.GetSuccessResponseAsync().ConfigureAwait(false);

            var responseList = await Client.Installments.GetAsync(sucessCreateResponse.Id).ConfigureAwait(false);
            var sucessResponseList = await responseList.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var deleteSuccessResponse = await Client.Installments.DeleteAsync(sucessResponseList.Id).ConfigureAwait(false);
            var content = await deleteSuccessResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(deleteSuccessResponse.IsSuccessStatusCode, Is.True);
            Assert.That(deleteSuccessResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(content, Is.Empty);
        }
    }
}
