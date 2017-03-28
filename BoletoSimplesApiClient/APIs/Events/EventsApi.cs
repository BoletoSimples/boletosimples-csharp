using BoletoSimplesApiClient.APIs.Events.Models;
using BoletoSimplesApiClient.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.Events
{
    /// <summary>
    /// Api para consulta de eventos
    /// </summary>
    public sealed class EventsApi
    {
        private readonly BoletoSimplesClient _client;
        private readonly HttpClientRequestBuilder _requestBuilder;
        private const string EVENTS_API = "/events";

        public EventsApi(BoletoSimplesClient client)
        {
            _client = client;
            _requestBuilder = new HttpClientRequestBuilder(client);
        }

        /// <summary>
        /// Informações do evento
        /// </summary>
        /// <param name="id">id do evento</param>
        /// <returns>Obtêm informações do evento</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/events/#informações-do-evento"/>
        public async Task<ApiResponse<EventData>> GetAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{EVENTS_API}/{id}")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();

            return await _client.SendAsync<EventData>(request);
        }

        /// <summary>
        /// Listar eventos paginado
        /// </summary>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de eventos</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limite de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/events/#listar-eventos"/>
        public async Task<PagedApiResponse<EventData>> GetAsync(int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), EVENTS_API)
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                         {
                                             ["page"] = pageNumber.ToString(),
                                             ["per_page"] = maxPerPage.ToString()
                                         })
                                         .Build();

            return await _client.SendPagedAsync<EventData>(request);
        }
    }
}
