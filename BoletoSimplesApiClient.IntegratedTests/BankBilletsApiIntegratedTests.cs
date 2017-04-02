using BoletoSimplesApiClient.APIs.BankBillets.Models;
using BoletoSimplesApiClient.Common;
using BoletoSimplesApiClient.UnitTests.Json;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using BoletoSimplesApiClient.APIs.BankBillets.RequestMessages;
using System;
using System.Linq;
using BoletoSimplesApiClient.APIs.BankBillets;

namespace BoletoSimplesApiClient.IntegratedTests
{
    [TestFixture]
    public class BankBilletsApiIntegratedTests : IntegratedTestBase
    {
        private BankBillet Content { get; set; }

        [SetUp]
        public void InitTests()
        {
            Content = JsonConvert.DeserializeObject<BankBillet>(JsonConstants.BankBillet);
        }

        [Test]
        public async Task Create_a_valid_bank_billet()
        {
            // Arrange
            ApiResponse<BankBillet> response;
            BankBillet successResponse;
            Content.BankBilletAccountId = 337;
            Content.BeneficiaryName = "Created-Beneficiary-Name";

            // Act
            response = await Client.BankBillets.PostAsync(Content).ConfigureAwait(false);
            successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(successResponse, Is.InstanceOf<BankBillet>());
        }


        [Test]
        public async Task Get_a_bank_billet_with_success()
        {
            // Arrange
            Content.BankBilletAccountId = 337;
            var createResponse = await Client.BankBillets.PostAsync(Content).ConfigureAwait(false);
            var successCreateResponse = await createResponse.GetSuccessResponseAsync().ConfigureAwait(false);
            Content.BeneficiaryName = "Get-Beneficiary-Name";
            ApiResponse<BankBillet> response;
            BankBillet successResponse;

            // Act
            response = await Client.BankBillets.GetAsync(successCreateResponse.Id).ConfigureAwait(false);
            successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            successCreateResponse.ShouldBeEquivalentTo(successResponse);
        }

        [Test]
        public async Task List_all_bank_billet_paged_with_success()
        {
            PagedApiResponse<BankBillet> response;
            Paged<BankBillet> successResponse;

            // Act
            response = await Client.BankBillets.GetAsync(0, 250).ConfigureAwait(false);
            successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(successResponse.Items, Is.Not.Null);
        }

        [Test]
        public async Task Try_list_more_than_250_bank_billet_throw_exception()
        {
            // Act && Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await Client.BankBillets.GetAsync(0, 1000).ConfigureAwait(false));
            Assert.That(ex.Message, Is.EqualTo("o valor máximo para o argumento maxPerPage é 250"));
        }

        [Test]
        public async Task Cancel_a_bank_billet_paged_with_success()
        {
            // Arrange
            Content.BankBilletAccountId = 337;
            Content.BeneficiaryName = "Cancel-Beneficiary-Name";
            var createResponse = await Client.BankBillets.PostAsync(Content).ConfigureAwait(false);
            var successCreateResponse = await createResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var cancelResponse = await Client.BankBillets.CancelAsync(successCreateResponse.Id).ConfigureAwait(false);
            var afterCancelResponse = await Client.BankBillets.GetAsync(successCreateResponse.Id).ConfigureAwait(false);
            var afterCancelSuccessResponse = await afterCancelResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(cancelResponse.IsSuccessStatusCode, Is.True);
            Assert.That(cancelResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(successCreateResponse.Status, Is.Not.EqualTo(afterCancelSuccessResponse.Status));
        }

        [Test]
        public async Task Duplicate_a_bank_billet_paged_with_success()
        {
            // Arrange
            PagedApiResponse<BankBillet> response;
            Paged<BankBillet> successResponse;

            response = await Client.BankBillets.GetAsync(0, 250).ConfigureAwait(false);
            successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);
            var lastBillet = successResponse.Items.Last();

            // Act
            var duplicateResponse = await Client.BankBillets.DuplicateAsync(lastBillet.Id, new Duplicate { Amount = lastBillet.Amount + 100 }).ConfigureAwait(false);
            var afterDuplicateSuccessResponse = await duplicateResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(duplicateResponse.IsSuccess, Is.True);
            Assert.That(duplicateResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(lastBillet.Amount, Is.LessThan(afterDuplicateSuccessResponse.Amount));

        }

        [Test]
        public async Task List_all_bank_billet_paged_by_custom_filter_cpf_or_cnpj_with_success()
        {
            PagedApiResponse<BankBillet> response;
            Paged<BankBillet> successResponse;

            // Act
            response = await Client.BankBillets.GetByCpfOrCnpjAsync("450.824.194-80", 0, 250).ConfigureAwait(false);
            successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(successResponse.Items, Is.Not.Null);
        }

        [Test]
        public async Task List_all_bank_billet_paged_by_custom_filter_our_number_with_success()
        {
            PagedApiResponse<BankBillet> response;
            Paged<BankBillet> successResponse;

            // Act
            response = await Client.BankBillets.GetByOurNumberAsync("0000005", 0, 250).ConfigureAwait(false);
            successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(successResponse.Items, Is.Not.Null);
        }

        [Test]
        public async Task List_all_bank_billet_paged_by_custom_filter_status_with_success()
        {
            PagedApiResponse<BankBillet> response;
            Paged<BankBillet> successResponse;

            // Act
            response = await Client.BankBillets.GetByStatusAsync(BankBilletStatus.Generating, 0, 250).ConfigureAwait(false);
            successResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(successResponse.Items, Is.Not.Null);
        }
    }
}
