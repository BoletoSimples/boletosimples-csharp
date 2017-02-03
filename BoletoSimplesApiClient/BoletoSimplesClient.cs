using BoletoSimplesApiClient.APIs.Auth;
using BoletoSimplesApiClient.APIs.BankBilletAccounts;
using BoletoSimplesApiClient.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient
{
    /// <summary>
    /// Cliente de acesso aos recursos da Api do boleto simples
    /// </summary>
    public class BoletoSimplesClient : IDisposable
    {
        public readonly ClientConnection Connection;

        /// <summary>
        /// Auth prove informações do usuário pelo token de acesso
        /// </summary>
        public readonly AuthApi Auth;

        /// <summary>
        /// BankBilletAccounts Api de carteiras de clientes
        /// </summary>
        public readonly BankBilletAccountsApi BankBilletAccounts;


        private readonly HttpClient _client;
        private readonly string _oAuthApiToken;

        public BoletoSimplesClient() : this(new HttpClient(), new ClientConnection())
        { }

        public BoletoSimplesClient(HttpClient client, ClientConnection clientConnection)
        {
            _client = client;
            Connection = clientConnection;
            _client.BaseAddress = Connection.GetBaseUri();
            Auth = new AuthApi(this);
            BankBilletAccounts = new BankBilletAccountsApi(this);
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<ApiResponse<T>> SendAsync<T>(HttpRequestMessage request) where T : new()
        {
            var response = await _client.SendAsync(request);
            request.Dispose();

            return new ApiResponse<T>(response);
        }

        public async Task<PagedApiResponse<T>> SendPagedAsync<T>(HttpRequestMessage request) where T : new()
        {
            var response = await _client.SendAsync(request);
            request.Dispose();

            return new PagedApiResponse<T>(response);
        }
    }
}
