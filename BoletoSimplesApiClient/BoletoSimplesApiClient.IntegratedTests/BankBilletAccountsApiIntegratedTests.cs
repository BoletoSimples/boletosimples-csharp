using BoletoSimplesApiClient.APIs.BankBilletAccounts.Moodels;
using BoletoSimplesApiClient.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;

namespace BoletoSimplesApiClient.IntegratedTests
{
    [TestFixture]
    public class BankBilletAccountsApiIntegratedTests
    {
        private static readonly JsonSerializerSettings _jsonSerializeSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
        };

        private const string BANK_ACCOUNT_BILLET = @"{'beneficiary_name':'Carteira pessoa fisica', 
                                                      'beneficiary_cnpj_cpf':'388.282.473-55', 
                                                      'beneficiary_address':'Qualquer Rua Nº 100, Sala 1001', 
                                                      'bank_contract_slug': 'sicoob-02', 
                                                      'agency_number': '4327', 
                                                      'agency_digit': '3', 
                                                      'account_number': '3666', 
                                                      'account_digit': '8',
                                                      'next_our_number': '1', 
                                                      'extra1': '1234567'}";


        private static readonly BankBilletAccount Content = JsonConvert.DeserializeObject<BankBilletAccount>(BANK_ACCOUNT_BILLET, _jsonSerializeSettings);


        [Test]
        public async Task Create_bank_account_billets_by_id_with_success()
        {
            // Arrange
            ApiResponse<BankBilletAccount> response;
            BankBilletAccount successResponse;

            using (var client = new BoletoSimplesClient())
            {
                // Act
                response = await client.BankBilletAccounts.PostAsync(Content).ConfigureAwait(false);
                successResponse = await response.GeResponseAsync().ConfigureAwait(false);
            }

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
            PagedApiResponse<BankBilletAccount> allBankBilletAccounts;
            ApiResponse<BankBilletAccount> response;
            BankBilletAccount successResponse;
            Content.BeneficiaryCnpjCpf = "850.275.556-01";

            using (var client = new BoletoSimplesClient())
            {
                // Act
                allBankBilletAccounts = await client.BankBilletAccounts.GetAsync(0, 250).ConfigureAwait(false);
                var firtBanckBilletAccounts = await allBankBilletAccounts.GeResponseAsync().ConfigureAwait(false);

                response = await client.BankBilletAccounts.PutAsync(firtBanckBilletAccounts.Items.First().Id, Content)
                                                          .ConfigureAwait(false);

                successResponse = await response.GeResponseAsync()
                                                .ConfigureAwait(false);
            }

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(successResponse, Is.InstanceOf<BankBilletAccount>());
            successResponse.Should().Equals(new BankBilletAccount());
        }

        [Test]
        public async Task Request_homologation_to_bank_account_billets_by_id_with_success()
        {
            // Arrange
            ApiResponse<BankBilletAccount> response;
            BankBilletAccount successResponse;

            using (var client = new BoletoSimplesClient())
            {
                var createResponse = await client.BankBilletAccounts.PostAsync(Content).ConfigureAwait(false);
                var createContent = await createResponse.GeResponseAsync().ConfigureAwait(false);

                // Act
                response = await client.BankBilletAccounts.AskAsync(createContent.Id)
                                                          .ConfigureAwait(false);

                successResponse = await response.GeResponseAsync().ConfigureAwait(false);
            }

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
            ApiResponse<BankBilletAccount> response;
            BankBilletAccount successResponse;

            using (var client = new BoletoSimplesClient())
            {
                var createResponse = await client.BankBilletAccounts.PostAsync(Content).ConfigureAwait(false);
                var createContent = await createResponse.GeResponseAsync().ConfigureAwait(false);

                // Act
                response = await client.BankBilletAccounts.ValidateAsync(createContent.Id, 1.99m)
                                                          .ConfigureAwait(false);

                successResponse = await response.GeResponseAsync().ConfigureAwait(false);
            }

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            successResponse.Should().Equals(new BankBilletAccount());
        }

        [Test]
        public async Task Get_bank_account_billets_by_id_with_success()
        {
            // Arrange
            ApiResponse<BankBilletAccount> createResponse;
            ApiResponse<BankBilletAccount> getResponse;


            BankBilletAccount expectedResponse;
            BankBilletAccount successResponse;

            using (var client = new BoletoSimplesClient())
            {
                createResponse = await client.BankBilletAccounts.PostAsync(Content).ConfigureAwait(false);
                expectedResponse = await createResponse.GeResponseAsync().ConfigureAwait(false);

                // Act
                getResponse = await client.BankBilletAccounts.GetAsync(expectedResponse.Id).ConfigureAwait(false);
                successResponse = await getResponse.GeResponseAsync().ConfigureAwait(false);
            }

            // Assert
            Assert.That(getResponse.IsSuccess, Is.True);
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
