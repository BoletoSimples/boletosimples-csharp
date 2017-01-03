using System;
using System.Net.Http;

namespace BoletoSimplesApiClient
{
    public class BoletoSimplesClient : IDisposable
    {
        private readonly HttpClient _client;
        private readonly ClientConnection _clientConnection;
        private string OAuthApiToken;

        public BoletoSimplesClient() : this(new HttpClient(), new ClientConnection())
        { }

        public BoletoSimplesClient(HttpClient client, ClientConnection clientConnection)
        {
            _client = client;
            _clientConnection = clientConnection;
            _client.BaseAddress = _clientConnection.GetBaseUri();
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
