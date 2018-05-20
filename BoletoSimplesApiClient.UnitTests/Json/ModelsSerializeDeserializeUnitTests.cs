using BoletoSimplesApiClient.APIs.BankBillets.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;
using BoletoSimplesApiClient.APIs.BankBilletAccounts.Moodels;
using BoletoSimplesApiClient.APIs.Discharges.Models;
using BoletoSimplesApiClient.APIs.Remittances.Models;
using BoletoSimplesApiClient.APIs.Installments.Models;
using BoletoSimplesApiClient.APIs.CustomerSubscriptions.Models;
using BoletoSimplesApiClient.APIs.Events.Models;
using BoletoSimplesApiClient.APIs.Customers.Models;

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
            firstBankBilletAccount.Should().BeEquivalentTo(secondBankBilletAccount);
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
            firstBankBillets.Should().BeEquivalentTo(secondBankBillets);
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
                var dischargesJson = await Task.FromResult(JsonConvert.SerializeObject(firstDischarge)).ConfigureAwait(false);
                secondDischarge = await Task.FromResult(JsonConvert.DeserializeObject<Discharge>(dischargesJson)).ConfigureAwait(false);
            });

            // Other Asserts
            firstDischarge.Should().BeEquivalentTo(secondDischarge);
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
                var remittancesJson = await Task.FromResult(JsonConvert.SerializeObject(firstRemittance)).ConfigureAwait(false);
                secondRemittance = await Task.FromResult(JsonConvert.DeserializeObject<Remittance>(remittancesJson)).ConfigureAwait(false);
            });

            // Other Asserts
            firstRemittance.Should().BeEquivalentTo(secondRemittance);
        }

        [Test]
        public async Task Given_input_json_of_model_Installments_should_be_serialize_and_desesialize_right()
        {
            // Arrange
            Installment firstInstallment = null;
            Installment secondInstallment = null;

            // Act && Assert
            Assert.DoesNotThrowAsync(async () =>
            {
                firstInstallment = await Task.FromResult(JsonConvert.DeserializeObject<Installment>(JsonConstants.Installment)).ConfigureAwait(false);
                var installmentsJson = await Task.FromResult(JsonConvert.SerializeObject(firstInstallment)).ConfigureAwait(false);
                secondInstallment = await Task.FromResult(JsonConvert.DeserializeObject<Installment>(installmentsJson)).ConfigureAwait(false);
            });

            // Other Asserts
            firstInstallment.Should().BeEquivalentTo(secondInstallment);
        }


        [Test]
        public async Task Given_input_json_of_model_CustomerSubscription_should_be_serialize_and_desesialize_right()
        {
            // Arrange
            CustomerSubscription firstCustomerSubscription = null;
            CustomerSubscription secondCustomerSubscription = null;

            // Act && Assert
            Assert.DoesNotThrowAsync(async () =>
            {
                firstCustomerSubscription = await Task.FromResult(JsonConvert.DeserializeObject<CustomerSubscription>(JsonConstants.CurstomerSubscription))
                                                      .ConfigureAwait(false);

                var customerSubscriptionJson = await Task.FromResult(JsonConvert.SerializeObject(firstCustomerSubscription))
                                                          .ConfigureAwait(false);

                secondCustomerSubscription = await Task.FromResult(JsonConvert.DeserializeObject<CustomerSubscription>(customerSubscriptionJson))
                                                       .ConfigureAwait(false);
            });

            // Other Asserts
            firstCustomerSubscription.Should().BeEquivalentTo(secondCustomerSubscription);
        }

        [Test]
        public async Task Given_input_json_of_model_Event_should_be_serialize_and_desesialize_right()
        {
            // Arrange
            EventData firstEventData = null;
            EventData secondEventData = null;

            // Act && Assert
            Assert.DoesNotThrowAsync(async () =>
            {
                firstEventData = await Task.FromResult(JsonConvert.DeserializeObject<EventData>(JsonConstants.Event))
                                                                  .ConfigureAwait(false);

                var eventDataJson = await Task.FromResult(JsonConvert.SerializeObject(firstEventData))
                                                                     .ConfigureAwait(false);

                secondEventData = await Task.FromResult(JsonConvert.DeserializeObject<EventData>(eventDataJson))
                                                                   .ConfigureAwait(false);
            });

            // Other Asserts
            firstEventData.Should().BeEquivalentTo(secondEventData);
        }

        [Test]
        public async Task Given_input_json_of_model_Customer_should_be_serialize_and_desesialize_right()
        {
            // Arrange
            Customer firstCustomer = null;
            Customer secondCustomer = null;

            // Act && Assert
            Assert.DoesNotThrowAsync(async () =>
            {
                firstCustomer = await Task.FromResult(JsonConvert.DeserializeObject<Customer>(JsonConstants.Customer))
                                                                  .ConfigureAwait(false);

                var eventDataJson = await Task.FromResult(JsonConvert.SerializeObject(firstCustomer))
                                                                     .ConfigureAwait(false);

                secondCustomer = await Task.FromResult(JsonConvert.DeserializeObject<Customer>(eventDataJson))
                                                                   .ConfigureAwait(false);
            });

            // Other Asserts
            firstCustomer.Should().BeEquivalentTo(secondCustomer);
        }
    }
}
