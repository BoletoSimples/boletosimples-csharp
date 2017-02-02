using BoletoSimplesApiClient.APIs.BankBilletAccounts.Moodels;
using BoletoSimplesApiClient.Common;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.IntegratedTests
{
    [TestFixture]
    public class PagedResponseIntegratedTests
    {
        [Test]
        public async Task Get_bank_account_billets_paged_response_with_success()
        {
            // Arrange
            PagedApiResponse<BankBilletAccount> response;
            Paged<BankBilletAccount> successResponse;
            using (var client = new BoletoSimplesClient())
            {

                // Act
                response = await client.BankBilletAccounts.GetAsync(0, 250).ConfigureAwait(false);
                successResponse = await response.GeResponseAsync().ConfigureAwait(false);

            }

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(successResponse.MaxPageSize, Is.EqualTo(250));
            Assert.That(successResponse.Items, Is.Empty);
            Assert.That(successResponse.CurrentPage, Is.EqualTo(0));
            Assert.That(successResponse, Is.InstanceOf<Paged<BankBilletAccount>>());
        }
    }
}
