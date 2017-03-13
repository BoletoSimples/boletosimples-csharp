using NUnit.Framework;
using System.Threading.Tasks;
using BoletoSimplesApiClient.APIs.CustomerSubscriptions.Models;
using System.Net;
using BoletoSimplesApiClient.APIs;
using System.Linq;

namespace BoletoSimplesApiClient.IntegratedTests
{
    [TestFixture]
    public class CustomerSubscriptionsApiIntegratedTests : IntegratedTestBase
    {
        [Test]
        public async Task Create_CustomerSubscription_with_success()
        {
            // Arrange
            var CustomerSubscription = new CustomerSubscription(1, 1999.55m, "Any Subscription");

            // Act
            var response = await Client.CustomerSubscriptions.PostAsync(CustomerSubscription).ConfigureAwait(false);
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse, Is.Not.Null);
        }

        [Test]
        public async Task Update_CustomerSubscription_with_success()
        {
            // Arrange
            var CustomerSubscription = new CustomerSubscription(1, 150.55m, "Any Subscription");
            var createResponse = await Client.CustomerSubscriptions.PostAsync(CustomerSubscription).ConfigureAwait(false);
            var createSucessResponse = await createResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            var subscription = new CustomerSubscription(1, 301.10m, "Other Subscription")
            {
                Cycle = Constants.Cycles.BIMONTHLY,
                Id = createSucessResponse.Id,
            };

            // Act
            var updateResponse = await Client.CustomerSubscriptions.PutAsync(createSucessResponse.Id, subscription).ConfigureAwait(false);
            var content = await updateResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(createResponse.IsSuccess, Is.True);
            Assert.That(updateResponse.IsSuccessStatusCode, Is.True);
            Assert.That(content, Is.Empty);
        }

        [Test]
        public async Task Get_CustomerSubscription_information_with_success()
        {
            // Arrange
            var CustomerSubscription = new CustomerSubscription(1, 9999.55m, "Any Subscription");
            var responseCreate = await Client.CustomerSubscriptions.PostAsync(CustomerSubscription).ConfigureAwait(false);
            var sucessCreateResponse = await responseCreate.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var response = await Client.CustomerSubscriptions.GetAsync(sucessCreateResponse.Id).ConfigureAwait(false);
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse.Amount, Is.EqualTo(9999.55m));
        }

        [Test]
        public async Task List_CustomerSubscriptions_with_sucess()
        {
            // Arrange
            var response = await Client.CustomerSubscriptions.GetAsync(1, 250).ConfigureAwait(false);

            // Act
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse.Items, Is.Not.Empty);
        }


        [Test]
        public async Task Create_next_charge_for_CustomerSubscription_with_sucess()
        {
            // Arrange
            var response = await Client.CustomerSubscriptions.GetAsync(1, 250).ConfigureAwait(false);
            var allChargesResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var nextSubscription = await Client.CustomerSubscriptions.PostAsync(allChargesResponse.Items.First().Id).ConfigureAwait(false);
            var sucessResponse = await nextSubscription.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(nextSubscription.IsSuccess, Is.True);
            Assert.That(nextSubscription.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(sucessResponse, Is.Not.Null);
        }

        [Test]
        public async Task Delete_CustomerSubscription_with_sucess()
        {
            // Arrange
            var CustomerSubscription = new CustomerSubscription(1, 300.55m, "Any Subscription");
            var responseCreate = await Client.CustomerSubscriptions.PostAsync(CustomerSubscription).ConfigureAwait(false);
            var sucessCreateResponse = await responseCreate.GetSuccessResponseAsync().ConfigureAwait(false);

            var responseList = await Client.CustomerSubscriptions.GetAsync(sucessCreateResponse.Id).ConfigureAwait(false);
            var sucessResponseList = await responseList.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var deleteSuccessResponse = await Client.CustomerSubscriptions.DeleteAsync(sucessResponseList.Id).ConfigureAwait(false);
            var content = await deleteSuccessResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(deleteSuccessResponse.IsSuccessStatusCode, Is.True);
            Assert.That(deleteSuccessResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(content, Is.Empty);
        }
    }
}
