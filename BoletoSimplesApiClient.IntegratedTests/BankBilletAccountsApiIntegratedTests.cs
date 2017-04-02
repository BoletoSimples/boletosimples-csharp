using BoletoSimplesApiClient.APIs.BankBilletAccounts.Moodels;
using BoletoSimplesApiClient.Common;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;
using BoletoSimplesApiClient.UnitTests.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace BoletoSimplesApiClient.IntegratedTests
{
    [TestFixture]
    public class BankBilletAccountsApiIntegratedTests : IntegratedTestBase
    {
        private BankBilletAccount Content { get; set; }

        [SetUp]
        public void PrepareTest()
        {
            Content = JsonConvert.DeserializeObject<BankBilletAccount>(JsonConstants.BankBilletAccount);
        }

        [Test]
        public async Task Create_bank_account_billets_by_with_success()
        {
            // Arrange
            ApiResponse<BankBilletAccount> response;
            BankBilletAccount successResponse;
            Content.BeneficiaryName = "Create-Beneficiary-Name";

            // Act
            response = await Client.BankBilletAccounts.PostAsync(Content).ConfigureAwait(false);
            successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(successResponse, Is.InstanceOf<BankBilletAccount>());
            Assert.That(successResponse.BeneficiaryCnpjCpf, Is.EqualTo(Content.BeneficiaryCnpjCpf));
        }

        [Test]
        public async Task List_bank_account_billets_paged_response_with_success()
        {
            // Arrange
            PagedApiResponse<BankBilletAccount> response;
            Paged<BankBilletAccount> successResponse;
            using (var client = new BoletoSimplesClient())
            {

                // Act
                response = await client.BankBilletAccounts.GetAsync(0, 250).ConfigureAwait(false);
                successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);
            }

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(successResponse.MaxPageSize, Is.EqualTo(250));
            Assert.That(successResponse.CurrentPage, Is.EqualTo(0));
            Assert.That(successResponse, Is.InstanceOf<Paged<BankBilletAccount>>());
        }

        [Test]
        public async Task Try_list_more_than_250_bank_account_billets_throw_exception()
        {
            // Act && Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await Client.BankBilletAccounts.GetAsync(0, 1000).ConfigureAwait(false));
            Assert.That(ex.Message, Is.EqualTo("o valor máximo para o argumento maxPerPage é 250"));
        }

        [Test]
        public async Task Update_bank_account_billets_by_id_with_success()
        {
            // Arrange
            HttpResponseMessage response;
            Content.BeneficiaryName = "Updatable-Beneficiary-Name";

            // Act
            var createResponse = await Client.BankBilletAccounts.PostAsync(Content).ConfigureAwait(false);
            var createContent = await createResponse.GetSuccessResponseAsync().ConfigureAwait(false);
            createContent.BeneficiaryCnpjCpf = "850.275.556-01";

            response = await Client.BankBilletAccounts.PutAsync(createContent.Id, createContent)
                                                      .ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);


            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(content, Is.Empty);
        }

        [Test]
        public async Task Request_homologation_to_bank_account_billets_by_id_with_success()
        {
            // Arrange
            ApiResponse<BankBilletAccount> response;
            BankBilletAccount successResponse;
            Content.BeneficiaryName = "Homologation-Beneficiary-Name";

            var createResponse = await Client.BankBilletAccounts.PostAsync(Content).ConfigureAwait(false);
            var createContent = await createResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            response = await Client.BankBilletAccounts.AskAsync(createContent.Id)
                                                      .ConfigureAwait(false);

            successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(successResponse, Is.InstanceOf<BankBilletAccount>());
        }

        [Test]
        [Ignore("Apenas para documentar o uso - O processo de homologação não é realizado na hora logo o teste quebra")]
        public async Task Request_validate_bank_account_billets_by_id_with_success()
        {
            // Arrange
            HttpResponseMessage response;
            Content.BeneficiaryName = "Validate-Beneficiary-Name";
            var createResponse = await Client.BankBilletAccounts.PostAsync(Content).ConfigureAwait(false);
            var createContent = await createResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            response = await Client.BankBilletAccounts.ValidateAsync(createContent.Id, 1.99m)
                                                      .ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(response, Is.Empty);
        }

        [Test]
        public async Task Get_bank_account_billets_by_id_with_success()
        {
            // Arrange
            ApiResponse<BankBilletAccount> createResponse;
            ApiResponse<BankBilletAccount> getResponse;

            BankBilletAccount expectedResponse;
            BankBilletAccount successResponse;
            Content.BeneficiaryName = "Get-Beneficiary-Name";

            createResponse = await Client.BankBilletAccounts.PostAsync(Content).ConfigureAwait(false);
            expectedResponse = await createResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            getResponse = await Client.BankBilletAccounts.GetAsync(expectedResponse.Id).ConfigureAwait(false);
            successResponse = await getResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(getResponse.IsSuccess, Is.True);
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}