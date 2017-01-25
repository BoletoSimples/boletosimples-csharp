using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.Common
{
    public sealed class ApiResponse<TResponse> where TResponse : new()
    {
        public readonly bool IsSuccess;
        public readonly HttpResponseMessage ErrorResponse;
        private readonly HttpResponseMessage _response;
        private readonly JsonSerializerSettings _jsonSerializeSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
        };

        public ApiResponse(HttpResponseMessage response)
        {
            _response = response;
            IsSuccess = response.IsSuccessStatusCode;

            if (!IsSuccess)
                ErrorResponse = response;
        }

        public async Task<TResponse> GeResponseAsync()
        {
            if (IsSuccess)
            {
                var content = await _response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var responseMessage = await Task.FromResult(JsonConvert.DeserializeObject<TResponse>(content, _jsonSerializeSettings))
                                                .ConfigureAwait(false);
                return responseMessage;
            }

            return new TResponse();
        }
    }
}
