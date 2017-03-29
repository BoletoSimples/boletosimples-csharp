using BoletoSimplesApiClient.APIs.CustomerSubscriptions.Models;
using BoletoSimplesApiClient.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.CustomerSubscriptions
{
    public sealed class CustomerSubscriptionApi
    {
        private readonly BoletoSimplesClient _client;
        private readonly HttpClientRequestBuilder _requestBuilder;
        private const string CUSTOMER_SUBSCRIPTION_API = "/customer_subscriptions";

        public CustomerSubscriptionApi(BoletoSimplesClient client)
        {
            _client = client;
            _requestBuilder = new HttpClientRequestBuilder(client);
        }

        /// <summary>
        /// Cria uma assinatura
        /// </summary>
        /// <param name="customerSubscription">dados da assinatura</param>
        /// <returns>Assinatura criada com sucesso</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customer_subscriptions/#criar-assinatura"/>
        public async Task<ApiResponse<CustomerSubscription>> PostAsync(CustomerSubscription customerSubscription)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), CUSTOMER_SUBSCRIPTION_API)
                                         .WithMethod(HttpMethod.Post)
                                         .AndOptionalContent(customerSubscription)
                                         .Build();

            return await _client.SendAsync<CustomerSubscription>(request);
        }

        /// <summary>
        /// Obtêm informação de uma assinatura
        /// </summary>
        /// <param name="id">id da assinatura</param>
        /// <returns>Assinatura criada com sucesso</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customer_subscriptions/#informações-do-assinatura"/>
        public async Task<ApiResponse<CustomerSubscription>> GetAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{CUSTOMER_SUBSCRIPTION_API}/{id}")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();

            return await _client.SendAsync<CustomerSubscription>(request);
        }

        /// <summary>
        /// Atualizar uma assinatura
        /// </summary>
        /// <param name="customerSubscription">dados da assinatura</param>
        /// <param name="customerSubscriptionId">Id da assinatura</param>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customer_subscriptions/#atualizar-assinatura"/>
        /// <returns>Assinatura criada com sucesso</returns>
        public async Task<HttpResponseMessage> PutAsync(int customerSubscriptionId, CustomerSubscription customerSubscription)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{CUSTOMER_SUBSCRIPTION_API}/{customerSubscriptionId}")
                                         .WithMethod(HttpMethod.Put)
                                         .AndOptionalContent(customerSubscription)
                                         .Build();

            return await _client.SendAsync(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Listar assinaturas paginado
        /// </summary>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de carnês</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limite de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customer_subscriptions/#listar-assinaturas"/>
        public async Task<PagedApiResponse<CustomerSubscription>> GetAsync(int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), CUSTOMER_SUBSCRIPTION_API)
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                         {
                                             ["page"] = pageNumber.ToString(),
                                             ["per_page"] = maxPerPage.ToString()
                                         })
                                         .Build();

            return await _client.SendPagedAsync<CustomerSubscription>(request);
        }

        /// <summary>
        /// Gerar a próxima cobrança de uma assinatura
        /// </summary>
        /// <param name="customerSubscriptionId">Id da assinatura</param>
        /// <returns>A próxima assinatura criada com sucesso</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customer_subscriptions/#gerar-próxima-cobrança"/>
        public async Task<ApiResponse<CustomerSubscription>> NextChargeAsync(int customerSubscriptionId)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{CUSTOMER_SUBSCRIPTION_API}/{customerSubscriptionId}/next_charge")
                                         .WithMethod(HttpMethod.Post)
                                         .Build();

            return await _client.SendAsync<CustomerSubscription>(request);
        }

        /// <summary>
        /// Deleta uma assinatura
        /// </summary>
        /// <param name="id">Identificador da assinatura</param>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/customer_subscriptions/#excluir-assinatura"/>
        /// <returns>HttpResponseMessage with HttpStatusCode 204 (NO Content)</returns>
        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{CUSTOMER_SUBSCRIPTION_API}/{id}")
                                         .WithMethod(HttpMethod.Delete)
                                         .Build();

            return await _client.SendAsync(request);
        }

    }
}
