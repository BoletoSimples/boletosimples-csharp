using BoletoSimplesApiClient.APIs.Auth;
using BoletoSimplesApiClient.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient
{
    public class BoletoSimplesClient : IDisposable
    {
        public readonly AuthApi Auth;
        public readonly ClientConnection Connection;
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
    }
}
