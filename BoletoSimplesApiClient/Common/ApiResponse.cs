using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.Common
{
    /// <summary>
    /// Representa o resultado de uma API simples e sem um resultado paginado contem a request de erro e a 
    /// resposta em caso de sucesso.
    /// </summary>
    /// <typeparam name="TSuccessResponse">Tipo do retorno de sucesso</typeparam>
    public sealed class ApiResponse<TSuccessResponse>
    {
        public readonly bool IsSuccess;
        public readonly HttpStatusCode StatusCode;
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
            StatusCode = response.StatusCode;

            if (!IsSuccess)
                ErrorResponse = response;
        }

        /// <summary>
        /// Obtêm a responsta de sucesso
        /// </summary>
        /// <returns>Retorna a resposta de sucesso ou nulo em caso de falha</returns>
        public async Task<TSuccessResponse> GetSuccessResponseAsync()
        {
            if (IsSuccess && _response.StatusCode != HttpStatusCode.NoContent)
            {
                var content = await _response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var responseMessage = await Task.FromResult(JsonConvert.DeserializeObject<TSuccessResponse>(content, _jsonSerializeSettings))
                                                .ConfigureAwait(false);
                return responseMessage;
            }

            return default(TSuccessResponse);
        }
    }
}
