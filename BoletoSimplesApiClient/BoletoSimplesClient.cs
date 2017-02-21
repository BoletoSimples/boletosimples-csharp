using BoletoSimplesApiClient.APIs.Auth;
using BoletoSimplesApiClient.APIs.BankBilletAccounts;
using BoletoSimplesApiClient.APIs.BankBillets;
using BoletoSimplesApiClient.APIs.Discharges;
using BoletoSimplesApiClient.Common;
using System;
using System.Net.Http;
using System.Threading;
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
        /// Api que prove informações do usuário pelo token de acesso
        /// </summary>
        public readonly AuthApi Auth;

        /// <summary>
        /// BankBilletAccounts Api de carteiras de clientes
        /// </summary>
        public readonly BankBilletAccountsApi BankBilletAccounts;

        /// <summary>
        /// BankBilletsApi Api de boletos
        /// </summary>
        public readonly BankBilletsApi BankBillets;

        /// <summary>
        /// DischargesApi Api de envio de arquivos CNAB de retorno
        /// </summary>
        public readonly DischargesApi DischargesApi;

        private readonly HttpClient _client;

        /// <summary>
        /// Client default sem personalizar nenuma configuração nesse construtor suas configurações provem do arquivo de configuração
        /// </summary>
        public BoletoSimplesClient() : this(new HttpClient(), new ClientConnection())
        { }

        /// <summary>
        /// Client default com dados da conexão de externos ao seu arquivo de configuração
        /// </summary>
        /// <param name="clientConnection">Dados de conexão com a api</param>
        public BoletoSimplesClient(ClientConnection clientConnection) : this(new HttpClient(), clientConnection)
        { }

        /// <summary>
        /// Client http customizado e dados da conexão de externos ao seu arquivo de configuração
        /// </summary>
        /// <param name="client">Sua versão do HttpClient</param>
        /// <param name="clientConnection">Dados de conexão com a api</param>
        public BoletoSimplesClient(HttpClient client, ClientConnection clientConnection)
        {
            _client = client;
            _client.BaseAddress = clientConnection.GetBaseUri();
            Connection = clientConnection;

            Auth = new AuthApi(this);
            BankBilletAccounts = new BankBilletAccountsApi(this);
            BankBillets = new BankBilletsApi(this);
            DischargesApi = new DischargesApi(this);
        }

        public async Task<ApiResponse<T>> SendAsync<T>(HttpRequestMessage request) where T : new()
        {
            var response = await _client.SendAsync(request, default(CancellationToken));
            request.Dispose();

            return new ApiResponse<T>(response);
        }

        public async Task<PagedApiResponse<T>> SendPagedAsync<T>(HttpRequestMessage request) where T : new()
        {
            var response = await _client.SendAsync(request, default(CancellationToken));
            request.Dispose();

            return new PagedApiResponse<T>(response);
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
