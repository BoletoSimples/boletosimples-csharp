using BoletoSimplesApiClient.APIs.Remittances.Models;
using BoletoSimplesApiClient.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.Remittances
{
    /// <summary>
    /// Api de arquivos de remessa
    /// </summary>
    public sealed class RemittancesApi
    {
        private readonly BoletoSimplesClient _client;
        private readonly HttpClientRequestBuilder _requestBuilder;
        private const string REMITTANCE_API = "/remittances";

        public RemittancesApi(BoletoSimplesClient client)
        {
            _client = client;
            _requestBuilder = new HttpClientRequestBuilder(client);
        }

        /// <summary>
        /// Criar CNAB(Remessa)
        /// </summary>
        /// <param name="remittance">nome do arquivo</param>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/remittances/#criar-cnab"/>
        /// <returns>Modelo que representa o arquivo de remessa</returns>
        public async Task<ApiResponse<Remittance>> PostAsync(Remittance remittance)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), REMITTANCE_API)
                                         .WithMethod(HttpMethod.Post)
                                         .AndOptionalContent(remittance)
                                         .Build();

            return await _client.SendAsync<Remittance>(request);
        }

        /// <summary>
        /// Informações do CNAB(Remessa)
        /// </summary>
        /// <param name="id">Informações do CNAB</param>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/remittances/#informaes-do-cnab"/>
        /// <returns>Modelo que representa o arquivo de retorno</returns>
        public async Task<ApiResponse<Remittance>> GetAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{REMITTANCE_API}/{id}")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();

            return await _client.SendAsync<Remittance>(request);
        }

        /// <summary>
        /// Listar CNABs(Remessa)
        /// </summary>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de arquivos de retorno</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limite de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/remittances/#listar-cnabs"/>
        /// <returns>Resultado paginado com uma lista de arquivos de remessa</returns>
        public async Task<PagedApiResponse<Remittance>> GetAsync(int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), REMITTANCE_API)
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                         {
                                             ["page"] = pageNumber.ToString(),
                                             ["per_page"] = maxPerPage.ToString()
                                         })
                                         .Build();

            return await _client.SendPagedAsync<Remittance>(request);
        }

        /// <summary>
        /// Deleta CNAB(Remessa)
        /// </summary>
        /// <param name="id">identificador do arquivo</param>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/remittances/#apagar-cnab"/>
        /// <returns>HttpResponseMessage with HttpStatusCode 204 (NO Content)</returns>
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{REMITTANCE_API}/{id}")
                                         .WithMethod(HttpMethod.Delete)
                                         .Build();

            return await _client.SendAsync(request);
        }
    }
}
