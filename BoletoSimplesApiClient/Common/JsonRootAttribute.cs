using System;

namespace BoletoSimplesApiClient.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal sealed class JsonRootAttribute : Attribute
    {
        public readonly string JsonRootName;

        public JsonRootAttribute(string jsonRootName)
        {
            JsonRootName = jsonRootName;
        }

        public static string GetAttributeValue(Type t)
        {
            var jsonAttribute = (JsonRootAttribute)GetCustomAttribute(t, typeof(JsonRootAttribute));

            if (jsonAttribute != null)
                return jsonAttribute.JsonRootName;

            return string.Empty;
        }
    }
}
