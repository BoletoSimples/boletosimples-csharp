using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.Common
{
    /// <summary>
    /// Representa o resultado de uma API simples e sem um resultado paginado contem a request de erro e a 
    /// resposta em caso de sucesso.
    /// </summary>
    /// <typeparam name="TSuccessResponse">Tipo do retorno de sucesso</typeparam>
    public sealed class ApiResponse<TSuccessResponse> where TSuccessResponse : new()
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

        public async Task<TSuccessResponse> GeResponseAsync()
        {
            if (IsSuccess)
            {
                var content = await _response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var responseMessage = await Task.FromResult(JsonConvert.DeserializeObject<TSuccessResponse>(content, _jsonSerializeSettings))
                                                .ConfigureAwait(false);
                return responseMessage;
            }

            return new TSuccessResponse();
        }
    }
}
