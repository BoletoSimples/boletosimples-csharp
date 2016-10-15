using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient
{
    public class BoletoSimplesClient : IDisposable
    {
        private readonly HttpClient _client;

        public BoletoSimplesClient() : this(new HttpClient()) { }

        public BoletoSimplesClient(HttpClient client)
        {
            _client = client;
        }

        public void Dispose()
        {
            _client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
