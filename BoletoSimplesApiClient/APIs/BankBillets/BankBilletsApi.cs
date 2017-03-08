using BoletoSimplesApiClient.APIs.BankBillets.Models;
using BoletoSimplesApiClient.APIs.BankBillets.RequestMessages;
using BoletoSimplesApiClient.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.BankBillets
{
    /// <summary>
    /// Api de boletos
    /// </summary>
    public sealed class BankBilletsApi
    {
        private readonly BoletoSimplesClient _client;
        private readonly HttpClientRequestBuilder _requestBuilder;
        private const string BANK_BILLET_API = "/bank_billets";

        public BankBilletsApi(BoletoSimplesClient client)
        {
            _client = client;
            _requestBuilder = new HttpClientRequestBuilder(client);
        }


        /// <summary>
        /// Cria um boleto
        /// </summary>
        /// <param name="bankBillet">dados do boleto</param>
        /// <returns>Boleto criado com sucesso</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billets/#criar-boleto"/>
        public async Task<ApiResponse<BankBillet>> PostAsync(BankBillet bankBillet)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), BANK_BILLET_API)
                                         .WithMethod(HttpMethod.Post)
                                         .AndOptionalContent(bankBillet)
                                         .Build();

            return await _client.SendAsync<BankBillet>(request);
        }

        /// <summary>
        /// Obter informações de um boleto
        /// </summary>
        /// <param name="id">Identificador do boleto</param>
        /// <returns>Boleto criado com sucesso</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billets/#informaes-do-boleto"/>
        public async Task<ApiResponse<BankBillet>> GetAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{BANK_BILLET_API}/{id}")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();

            return await _client.SendAsync<BankBillet>(request);
        }

        /// <summary>
        /// Listar boletos paginado
        /// </summary>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de boletos</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limite de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billets/#listar-boletos"/>
        public async Task<PagedApiResponse<BankBillet>> GetAsync(int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), BANK_BILLET_API)
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                         {
                                             ["page"] = pageNumber.ToString(),
                                             ["per_page"] = maxPerPage.ToString()
                                         })
                                         .Build();

            return await _client.SendPagedAsync<BankBillet>(request);
        }

        /// <summary>
        /// Cancelar um boleto
        /// Você pode cancelar boletos nos status de Aberto(opened) ou Vencido(overdue)
        /// </summary>
        /// <param name="id">Identificador do boleto</param>
        /// <returns>HttpResponseMessage with HttpStatusCode 204 (NO Content)</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billets/#cancelar-boleto"/>
        public async Task<HttpResponseMessage> CancelAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{BANK_BILLET_API}/{id}/cancel")
                                         .WithMethod(HttpMethod.Put)
                                         .Build();

            return await _client.SendAsync(request);
        }

        /// <summary>
        /// Duplicar um boleto
        /// No momento não há cálculo de juros automáticos que atualizem o valor do boleto
        /// </summary>
        /// <param name="id">Identificador do boleto</param>
        /// <param name="requestMessage">Parâmetros de duplicação</param>
        /// <returns>Boleto criado com sucesso</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billets/#gerar-segunda-via-do-boleto"/>
        public async Task<ApiResponse<BankBillet>> DuplicateAsync(int id, Duplicate requestMessage)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{BANK_BILLET_API}/{id}/duplicate")
                                         .WithMethod(HttpMethod.Post)
                                         .AndOptionalContent(requestMessage)
                                         .Build();

            return await _client.SendAsync<BankBillet>(request);
        }


        /// <summary>
        /// Listar boletos paginado filtrado por CPF ou CNPJ
        /// </summary>
        /// <param name="cpfOrCnpj">CPF ou CNPJ formatado (125.812.717-28)</param>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de carteiras</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limite de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billets/#buscar-por-cpf-ou-cnpj" />
        public async Task<PagedApiResponse<BankBillet>> GetByCpfOrCnpjAsync(string cpfOrCnpj, int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{BANK_BILLET_API}/cnpj_cpf")
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                         {
                                             ["q"] = cpfOrCnpj,
                                             ["page"] = pageNumber.ToString(),
                                             ["per_page"] = maxPerPage.ToString()
                                         })
                                         .Build();

            return await _client.SendPagedAsync<BankBillet>(request);
        }

        /// <summary>
        /// Listar boletos paginado filtrado por nosso numero
        /// </summary>
        /// <param name="ourNumber">Nosso número</param>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de carteiras</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limite de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billets/#buscar-por-nosso-nmero" />
        public async Task<PagedApiResponse<BankBillet>> GetByOurNumberAsync(string ourNumber, int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{BANK_BILLET_API}/our_number")
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                         {
                                             ["q"] = ourNumber,
                                             ["page"] = pageNumber.ToString(),
                                             ["per_page"] = maxPerPage.ToString()
                                         })
                                         .Build();

            return await _client.SendPagedAsync<BankBillet>(request);
        }

        /// <summary>
        /// Listar boletos paginado filtrado por nosso numero
        /// </summary>
        /// <param name="status">Nosso número</param>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de carteiras</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limite de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billets/#buscar-por-situao-do-boleto" />
        public async Task<PagedApiResponse<BankBillet>> GetByStatusAsync(string status, int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{BANK_BILLET_API}/status")
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                         {
                                             ["q"] = status,
                                             ["page"] = pageNumber.ToString(),
                                             ["per_page"] = maxPerPage.ToString()
                                         })
                                         .Build();

            return await _client.SendPagedAsync<BankBillet>(request);
        }
    }
}
