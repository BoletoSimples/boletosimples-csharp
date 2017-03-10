using BoletoSimplesApiClient.APIs.Installments.Models;
using BoletoSimplesApiClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.Installments
{
    public sealed class InstallmentsApi
    {
        private readonly BoletoSimplesClient _client;
        private readonly HttpClientRequestBuilder _requestBuilder;
        private const string INSTALLMENTS_API = "/installments";

        public InstallmentsApi(BoletoSimplesClient client)
        {
            _client = client;
            _requestBuilder = new HttpClientRequestBuilder(client);
        }

        /// <summary>
        /// Cria um carnê
        /// </summary>
        /// <param name="installment">dados do carne</param>
        /// <returns>Carnê criado com sucesso</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/installments/#criar-carnê"/>
        public async Task<ApiResponse<Installment>> PostAsync(Installment installment)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), INSTALLMENTS_API)
                                         .WithMethod(HttpMethod.Post)
                                         .AndOptionalContent(installment)
                                         .Build();

            return await _client.SendAsync<Installment>(request);
        }

        /// <summary>
        /// Obtêm informação de um carnê
        /// </summary>
        /// <param name="id">id carnê</param>
        /// <returns>Carnê criado com sucesso</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/installments/#informaes-do-carnê"/>
        public async Task<ApiResponse<Installment>> GetAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{INSTALLMENTS_API}/{id}")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();

            return await _client.SendAsync<Installment>(request);
        }

        /// <summary>
        /// Listar carnês paginado
        /// </summary>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de carnês</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limite de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/installments/#listar-carnês"/>
        public async Task<PagedApiResponse<Installment>> GetAsync(int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), INSTALLMENTS_API)
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                         {
                                             ["page"] = pageNumber.ToString(),
                                             ["per_page"] = maxPerPage.ToString()
                                         })
                                         .Build();

            return await _client.SendPagedAsync<Installment>(request);
        }

        /// <summary>
        /// Deleta um carnê
        /// </summary>
        /// <param name="id">Identificador do carnê</param>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/installments/#excluir-carnê"/>
        /// <returns>HttpResponseMessage with HttpStatusCode 204 (NO Content)</returns>
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{INSTALLMENTS_API}/{id}")
                                         .WithMethod(HttpMethod.Delete)
                                         .Build();
           
            return await _client.SendAsync(request);
        }
    }
}
