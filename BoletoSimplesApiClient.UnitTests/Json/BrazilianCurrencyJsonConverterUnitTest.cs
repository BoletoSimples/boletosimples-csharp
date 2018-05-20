using BoletoSimplesApiClient.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;

namespace BoletoSimplesApiClient.UnitTests.Json
{
    [TestFixture]
    public class BrazilianCurrencyJsonConverterUnitTest
    {
        [Test]
        public async Task Given_any_json_with_brazilian_currency_should_be_serialize_and_deserialize_right()
        {
            // Arrange
            var inputJson = @"['99.999,99', '0,33', '12.345,67', '']";
            var expectedItems = new decimal[] { 99999.99m, 0.33m, 12345.67m, 0.00m };
            var expectedSerializedJson = "[\"99.999,99\",\"0,33\",\"12.345,67\",\"0,00\"]";

            // Act
            var deserializedJson = await Task.FromResult(JsonConvert.DeserializeObject<List<decimal>>(inputJson, new BrazilianCurrencyJsonConverter()));
            var serializedJson = await Task.FromResult(JsonConvert.SerializeObject(deserializedJson, new BrazilianCurrencyJsonConverter()));

            // Assert
            expectedItems.Should().BeEquivalentTo(deserializedJson);
            expectedSerializedJson.Should().BeEquivalentTo(serializedJson);
        }
    }
}
