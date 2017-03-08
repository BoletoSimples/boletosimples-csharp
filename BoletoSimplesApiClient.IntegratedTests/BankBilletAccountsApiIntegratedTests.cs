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