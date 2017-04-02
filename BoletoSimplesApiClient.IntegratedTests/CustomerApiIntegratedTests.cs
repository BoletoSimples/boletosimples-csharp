using NUnit.Framework;
using System.Threading.Tasks;
using BoletoSimplesApiClient.APIs.Customers.Models;
using System.Net;

namespace BoletoSimplesApiClient.IntegratedTests
{
    [TestFixture]
    public class CustomerApiIntegratedTests : IntegratedTestBase
    {
        [Test]
        public async Task Create_Customer_with_success()
        {
            // Arrange
            var customer = new Customer("Any Name", "532.631.501-70", "99999-999", "Any Street", "Rio de Janeiro", "RJ", "Any Neighborhood")
            {
                Email = "anyemail@gmail.com",
                EmailCc = "otheremail@gmail.com"
            };

            // Act
            var response = await Client.Customers.PostAsync(customer).ConfigureAwait(false);
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse, Is.Not.Null);
        }

        [Test]
        public async Task Update_Customer_with_success()
        {
            // Arrange
            var customer = new Customer("Any Name", "444.754.717-10", "00000-999", "Any Street", "Rio de Janeiro", "RJ", "Any Neighborhood")
            {
                Email = "anyemail@gmail.com",
                EmailCc = "otheremail@gmail.com"
            };

            var createResponse = await Client.Customers.PostAsync(customer).ConfigureAwait(false);
            var createSucessResponse = await createResponse.GetSuccessResponseAsync().ConfigureAwait(false);

            var newCurtomerData = new Customer("Any Name", "434.621.448-71", "33333-000", "Any Street", "Rio de Janeiro", "RJ", "Any Neighborhood");

            // Act
            var updateResponse = await Client.Customers.PutAsync(createSucessResponse.Id, newCurtomerData).ConfigureAwait(false);
            var content = await updateResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Assert
            Assert.That(createResponse.IsSuccess, Is.True);
            Assert.That(updateResponse.IsSuccessStatusCode, Is.True);
            Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task Get_Customer_information_with_success()
        {
            // Arrange
            var customer = new Customer("Any Name", "477.774.674-76", "88888-888", "Any Street", "Rio de Janeiro", "RJ", "Any Neighborhood")
            {
                Email = "anyemail@gmail.com",
                EmailCc = "otheremail@gmail.com"
            };

            var responseCreate = await Client.Customers.PostAsync(customer).ConfigureAwait(false);
            var sucessCreateResponse = await responseCreate.GetSuccessResponseAsync().ConfigureAwait(false);

            // Act
            var response = await Client.Customers.GetAsync(sucessCreateResponse.Id).ConfigureAwait(false);
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse.CnpjCpf, Is.EqualTo(customer.CnpjCpf));
            Assert.That(sucessResponse.Zipcode, Is.EqualTo(customer.Zipcode.Replace("-", "")));
        }

        [Test]
        public async Task List_Customers_with_sucess()
        {
            // Arrange
            var response = await Client.Customers.GetAsync(1, 250).ConfigureAwait(false);

            // Act
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse.Items, Is.Not.Empty);
        }

        [Test]
        public async Task Get_Customers_by_cpf_or_cnpj_with_sucess()
        {
            // Arrange
            var response = await Client.Customers.GetByCpfOrCnpjAsync("477.774.674-76").ConfigureAwait(false);

            // Act
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse.CnpjCpf, Is.EqualTo("477.774.674-76"));
        }

        [Test]
        public async Task Get_Customers_by_email_with_sucess()
        {
            // Arrange
            var response = await Client.Customers.GetByEmailAsync("anyemail@gmail.com").ConfigureAwait(false);

            // Act
            var sucessResponse = await response.GetSuccessResponseAsync().ConfigureAwait(false);

            // Assert
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(sucessResponse.Email, Is.EqualTo("anyemail@gmail.com"));
        }

    }
}
