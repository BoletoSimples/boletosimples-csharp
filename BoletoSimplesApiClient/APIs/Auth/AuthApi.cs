using BoletoSimplesApiClient.APIs.Users.Models;
using BoletoSimplesApiClient.Common;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.Auth
{
    /// <summary>
    /// Api que obtem informações do usuário logado
    /// </summary>
    public sealed class AuthApi
    {
        private readonly BoletoSimplesClient _client;
        private readonly HttpClientRequestBuilder _requestBuilder;

        public AuthApi(BoletoSimplesClient client)
        {
            _client = client;
            _requestBuilder = new HttpClientRequestBuilder(client);
        }

        // TODO: Implementar fluxo com OAuth
        /*
        public async Task<ApiResponse<UserInfoResponseMessage>> GetOAuthToken()
        {
            var request = _requestBuilder.To(new Uri("http://locahost"))
                                         .WithMethod(HttpMethod.Get)
                                         .AndOptionalContent(new StringContent(""))
                                         .Build();

            return await _client.SendAsync<UserInfoResponseMessage>(request);
        }
        */

        /// <summary>
        /// Obtem informação do usuário pelo token de acesso
        /// </summary>
        /// <returns>Informações gerais do usuário</returns>
        /// <see cref="http://api.boletosimples.com.br/authentication/token/"/>
        public async Task<ApiResponse<UserInfo>> GetUserInfoAsync()
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), "/userinfo")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();

            return await _client.SendAsync<UserInfo>(request);
        }

    }
}
