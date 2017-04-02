using BoletoSimplesApiClient.APIs.Customers.Models;
using BoletoSimplesApiClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.Customers
{
    /// <summary>
    /// Api de clientes
    /// </summary>
    public sealed class CustomersApi
    {
        private readonly BoletoSimplesClient _client;
        private readonly HttpClientRequestBuilder _requestBuilder;
        private const string CUSTOMERS_API = "/customers";

        public CustomersApi(BoletoSimplesClient client)
        {
            _client = client;
            _requestBuilder = new HttpClientRequestBuilder(client);
        }

        /// <summary>
        /// Cria uma cliente
        /// </summary>
        /// <param name="customer">Dados do cliente</param>
        /// <returns>Cliente criado com sucesso</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customers/#criar-cliente"/>
        public async Task<ApiResponse<Customer>> PostAsync(Customer customer)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), CUSTOMERS_API)
                                         .WithMethod(HttpMethod.Post)
                                         .AndOptionalContent(customer)
                                         .Build();

            return await _client.SendAsync<Customer>(request);
        }

        /// <summary>
        /// Obtêm informação de um cliente
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns>Informações do cliente</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customers/#informações-do-cliente"/>
        public async Task<ApiResponse<Customer>> GetAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{CUSTOMERS_API}/{id}")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();

            return await _client.SendAsync<Customer>(request);
        }

        /// <summary>
        /// Atualizar um cliente
        /// </summary>
        /// <param name="customer">dados do cliente</param>
        /// <param name="customerId">Id do cliente</param>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customers/#atualizar-cliente"/>
        /// <returns>HttpResponseMessage 204 (No Content)</returns>
        public async Task<HttpResponseMessage> PutAsync(int customerId, Customer customer)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{CUSTOMERS_API}/{customerId}")
                                         .WithMethod(HttpMethod.Put)
                                         .AndOptionalContent(customer)
                                         .Build();

            return await _client.SendAsync(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Listar clientes paginado
        /// </summary>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de clientes</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limite de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customers/#listar-clientes"/>
        public async Task<PagedApiResponse<Customer>> GetAsync(int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), CUSTOMERS_API)
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                         {
                                             ["page"] = pageNumber.ToString(),
                                             ["per_page"] = maxPerPage.ToString()
                                         })
                                         .Build();

            return await _client.SendPagedAsync<Customer>(request);
        }

        /// <summary>
        /// Obtêm informação de um cliente por CPF ou CNPJ
        /// </summary>
        /// <param name="formattedCpfOrCnpj">CPF ou CNPJ formatado (formato 999.999.999-99 ou 99.999.999/9999-99)</param>
        /// <returns>Informações do cliente</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customers/#buscar-por-cpf-ou-cnpj"/>
        public async Task<ApiResponse<Customer>> GetByCpfOrCnpjAsync(string formattedCpfOrCnpj)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{CUSTOMERS_API}/cnpj_cpf")
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                         {
                                             ["q"] = formattedCpfOrCnpj
                                         })
                                         .Build();

            return await _client.SendAsync<Customer>(request);
        }

        /// <summary>
        /// Obtêm informação de um cliente por email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Informações do cliente</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customers/#buscar-por-e-mail"/>
        public async Task<ApiResponse<Customer>> GetByEmailAsync(string email)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{CUSTOMERS_API}/email")
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                          {
                                              ["q"] = email
                                          })
                                         .Build();

            return await _client.SendAsync<Customer>(request);
        }
    }
}