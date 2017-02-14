using BoletoSimplesApiClient.APIs.BankBillets.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Converters;
using BoletoSimplesApiClient.APIs.BankBilletAccounts.Moodels;

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
    }
}
