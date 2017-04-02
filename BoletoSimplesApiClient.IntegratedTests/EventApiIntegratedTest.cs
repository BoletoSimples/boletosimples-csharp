using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.IntegratedTests
{
    [TestFixture]
    public class EventApiIntegratedTest : IntegratedTestBase
    {
        [Test]
        public async Task Get_Event_information_with_success()
        {
            // Arrange
            var allEvents = await Client.Events.GetAsync(1, 250).ConfigureAwait(false);
            var itemsResponse = await allEvents.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var response = await Client.Events.GetAsync(itemsResponse.Items.First().Id).ConfigureAwait(false);
            var successResponse = await allEvents.GetSuccessResponseAsync().ConfigureAwait(false);
            var firstEvent = successResponse.Items.First();

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(firstEvent.Data.Object, Is.Not.Null);
        }

        [Test]
        public async Task List_Events_with_success()
        {
            // Arrange
            var response = await Client.Events.GetAsync(1, 250).ConfigureAwait(false);

            // Act
            var successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(successResponse.Items, Is.Not.Empty);
        }

        [Test]
        public async Task Try_list_more_than_250_events_throw_exception()
        {
            // Act && Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await Client.BankBillets.GetAsync(0, 1000).ConfigureAwait(false));
            Assert.That(ex.Message, Is.EqualTo("o valor máximo para o argumento maxPerPage é 250"));
        }
    }
}
