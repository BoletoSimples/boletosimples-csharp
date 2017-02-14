using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace BoletoSimplesApiClient.Utils
{
    internal sealed class BrazilianCurrencyJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var formattedValue = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:N}", value);
            writer.WriteValue(formattedValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = JToken.Load(reader).Value<string>()?.Trim();

            if (string.IsNullOrEmpty(value))
                value = "0,00";

            var newValue = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
            return newValue;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }
    }
}
