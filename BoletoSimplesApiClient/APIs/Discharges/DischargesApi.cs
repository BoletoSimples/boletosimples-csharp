using BoletoSimplesApiClient.APIs.Discharges.Models;
using BoletoSimplesApiClient.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.Discharges
{
    /// <summary>
    /// Api de arquivo de Retorno
    /// </summary>
    public sealed class DischargesApi
    {
        private readonly BoletoSimplesClient _client;
        private readonly HttpClientRequestBuilder _requestBuilder;
        private const string DISCHARGE_API = "/discharges";

        public DischargesApi(BoletoSimplesClient client)
        {
            _client = client;
            _requestBuilder = new HttpClientRequestBuilder(client);
        }

        /// <summary>
        /// Enviar CNAB
        /// </summary>
        /// <param name="fileName">nome do arquivo</param>
        /// <param name="file">conteudo do arquivo</param>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/discharges/#enviar-cnab"/>
        /// <returns>Modelo que representa o arquivo de retorno</returns>
        public async Task<ApiResponse<Discharge>> PostAsync(string fileName, Stream file)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), DISCHARGE_API)
                                         .WithMethod(HttpMethod.Post)
                                         .AppendFileContent("discharge[file]", fileName, file)
                                         .Build();

            return await _client.SendAsync<Discharge>(request);
        }

        /// <summary>
        /// Informações do CNAB
        /// </summary>
        /// <param name="id">Informações do CNAB</param>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/discharges/#informaes-do-cnab"/>
        /// <returns>Modelo que representa o arquivo de retorno</returns>
        public async Task<ApiResponse<Discharge>> GetAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{DISCHARGE_API}/{id}")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();

            return await _client.SendAsync<Discharge>(request);
        }

        /// <summary>
        /// Listar CNABs(Retorno)
        /// </summary>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de arquivo de retorno</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limite de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/discharges/#listar-cnabs"/>
        /// <returns>Modelo que representa o arquivo de retorno</returns>
        public async Task<PagedApiResponse<Discharge>> GetAsync(int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), DISCHARGE_API)
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string>
                                         {
                                             ["page"] = pageNumber.ToString(),
                                             ["per_page"] = maxPerPage.ToString()
                                         })
                                         .Build();

            return await _client.SendPagedAsync<Discharge>(request);
        }

        /// <summary>
        /// Quitar boletos em arquivo CNAB
        /// É necessário já ter enviado o CNAB. Todos os boletos que forem identificados dentro do CNAB serão marcados como PAGO e os Webhooks serão disparados.
        /// </summary>
        /// <param name="id">identificador do arquivo CNAB</param>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/discharges/#quitar-boletos"/>
        /// <returns>Modelo que representa o arquivo de retorno</returns>
        public async Task<ApiResponse<Discharge>> PayOffAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{DISCHARGE_API}/{id}/pay_off")
                                         .WithMethod(HttpMethod.Put)
                                         .Build();

            return await _client.SendAsync<Discharge>(request);
        }
    }
}
