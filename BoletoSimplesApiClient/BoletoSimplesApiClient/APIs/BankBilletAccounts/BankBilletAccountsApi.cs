using BoletoSimplesApiClient.APIs.BankBilletAccounts.Moodels;
using BoletoSimplesApiClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.BankBilletAccounts
{
    public class BankBilletAccountsApi
    {
        private readonly BoletoSimplesClient _client;
        private readonly HttpClientRequestBuilder _requestBuilder;

        public BankBilletAccountsApi(BoletoSimplesClient client)
        {
            _client = client;
            _requestBuilder = new HttpClientRequestBuilder(client);
        }

        /// <summary>
        /// Listar carteiras paginada
        /// </summary>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de carteiras</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limide de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billet_accounts/#listar-carteiras"/>
        public async Task<PagedApiResponse<BankBilletAccount>> GetAsync(int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), "/bank_billet_accounts")
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string> { ["page"] = pageNumber.ToString(), ["per_page"] = maxPerPage.ToString() })
                                         .Build();

            return await _client.SendPagedAsync<BankBilletAccount>(request);
        }
    }
}
