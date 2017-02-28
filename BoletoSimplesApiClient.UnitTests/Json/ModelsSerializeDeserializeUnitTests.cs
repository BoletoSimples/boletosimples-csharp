using BoletoSimplesApiClient.APIs.BankBillets.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;
using BoletoSimplesApiClient.APIs.BankBilletAccounts.Moodels;
using BoletoSimplesApiClient.APIs.Discharges.Models;
using BoletoSimplesApiClient.APIs.Remittances.Models;

namespace BoletoSimplesApiClient.UnitTests.Json
{
    [TestFixture]
    public class ModelsSerializeDeserializeUnitTests
    {
        public ModelsSerializeDeserializeUnitTests()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
            };
        }

        [Test]
        public async Task Given_input_json_of_model_Bank_Billets_Account_should_be_serialize_and_desesialize_right()
        {
            // Arrange
            BankBilletAccount firstBankBilletAccount = null;
            BankBilletAccount secondBankBilletAccount = null;

            // Act && Assert
            Assert.DoesNotThrowAsync(async () =>
            {
                firstBankBilletAccount = await Task.FromResult(JsonConvert.DeserializeObject<BankBilletAccount>(JsonConstants.BankBilletAccount)).ConfigureAwait(false);
                var bankBilletAccountJson = await Task.FromResult(JsonConvert.SerializeObject(firstBankBilletAccount)).ConfigureAwait(false);
                secondBankBilletAccount = await Task.FromResult(JsonConvert.DeserializeObject<BankBilletAccount>(bankBilletAccountJson)).ConfigureAwait(false);
            });

            // Other Asserts
            firstBankBilletAccount.ShouldBeEquivalentTo(secondBankBilletAccount);
        }

        [Test]
        public async Task Given_input_json_of_model_Bank_Billets_should_be_serialize_and_desesialize_right()
        {
            // Arrange
            BankBillet firstBankBillets = null;
            BankBillet secondBankBillets = null;

            // Act && Assert
            Assert.DoesNotThrowAsync(async () =>
            {
                firstBankBillets = await Task.FromResult(JsonConvert.DeserializeObject<BankBillet>(JsonConstants.BankBillet)).ConfigureAwait(false);
                var bankBilletJson = await Task.FromResult(JsonConvert.SerializeObject(firstBankBillets)).ConfigureAwait(false);
                secondBankBillets = await Task.FromResult(JsonConvert.DeserializeObject<BankBillet>(bankBilletJson)).ConfigureAwait(false);
            });

            // Other Asserts
            firstBankBillets.ShouldBeEquivalentTo(secondBankBillets);
        }

        [Test]
        public async Task Given_input_json_of_model_Discharges_should_be_serialize_and_desesialize_right()
        {
            // Arrange
            Discharge firstDischarge = null;
            Discharge secondDischarge = null;

            // Act && Assert
            Assert.DoesNotThrowAsync(async () =>
            {
                firstDischarge = await Task.FromResult(JsonConvert.DeserializeObject<Discharge>(JsonConstants.Discharge)).ConfigureAwait(false);
                var bankBilletJson = await Task.FromResult(JsonConvert.SerializeObject(firstDischarge)).ConfigureAwait(false);
                secondDischarge = await Task.FromResult(JsonConvert.DeserializeObject<Discharge>(bankBilletJson)).ConfigureAwait(false);
            });

            // Other Asserts
            firstDischarge.ShouldBeEquivalentTo(secondDischarge);
        }

        [Test]
        public async Task Given_input_json_of_model_Remittances_should_be_serialize_and_desesialize_right()
        {
            // Arrange
            Remittance firstRemittance = null;
            Remittance secondRemittance = null;

            // Act && Assert
            Assert.DoesNotThrowAsync(async () =>
            {
                firstRemittance = await Task.FromResult(JsonConvert.DeserializeObject<Remittance>(JsonConstants.Remittance)).ConfigureAwait(false);
                var bankBilletJson = await Task.FromResult(JsonConvert.SerializeObject(firstRemittance)).ConfigureAwait(false);
                secondRemittance = await Task.FromResult(JsonConvert.DeserializeObject<Remittance>(bankBilletJson)).ConfigureAwait(false);
            });

            // Other Asserts
            firstRemittance.ShouldBeEquivalentTo(secondRemittance);
        }
    }
}
