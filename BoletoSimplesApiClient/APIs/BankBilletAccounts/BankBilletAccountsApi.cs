using BoletoSimplesApiClient.APIs.BankBilletAccounts.Moodels;
using BoletoSimplesApiClient.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.BankBilletAccounts
{
    public class BankBilletAccountsApi
    {
        private readonly BoletoSimplesClient _client;
        private readonly HttpClientRequestBuilder _requestBuilder;
        private const string BANK_BILLET_ACCOUNTS_API = "/bank_billet_accounts";

        public BankBilletAccountsApi(BoletoSimplesClient client)
        {
            _client = client;
            _requestBuilder = new HttpClientRequestBuilder(client);
        }

        /// <summary>
        /// Cria uma carteira de cliente
        /// </summary>
        /// <param name="bankBilletAccountData">dados da conta</param>
        /// <returns>Conta criada com sucesso</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billet_accounts/#criar-carteira"/>
        public async Task<ApiResponse<BankBilletAccount>> PostAsync(BankBilletAccount bankBilletAccountData)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), BANK_BILLET_ACCOUNTS_API)
                                         .WithMethod(HttpMethod.Post)
                                         .AndOptionalContent(bankBilletAccountData)
                                         .Build();

            return await _client.SendAsync<BankBilletAccount>(request);
        }

        /// <summary>
        /// Atualizar informações de uma carteira de cliente
        /// </summary>
        /// <param name="bankBilletAccountData">dados da conta</param>
        /// <param name="id">Identifiador da carteira</param>
        /// <returns>Conta criada com sucesso</returns>
        public async Task<ApiResponse<BankBilletAccount>> PutAsync(int id, BankBilletAccount bankBilletAccountData)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{BANK_BILLET_ACCOUNTS_API}/{id}")
                                         .WithMethod(HttpMethod.Put)
                                         .AndOptionalContent(bankBilletAccountData)
                                         .Build();

            return await _client.SendAsync<BankBilletAccount>(request);
        }

        /// <summary>
        /// Obtêm informações da carteira
        /// </summary>
        /// <param name="id">Identifiador da carteira</param>
        /// <returns>Dados da carteira</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billet_accounts/#informaes-do-carteira"/>
        public async Task<ApiResponse<BankBilletAccount>> GetAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{BANK_BILLET_ACCOUNTS_API}/{id}")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();

            return await _client.SendAsync<BankBilletAccount>(request);
        }

        /// <summary>
        /// Listar carteiras paginada
        /// </summary>
        /// <param name="pageNumber">Numero da página</param>
        /// <param name="maxPerPage">Quantidade máxima por pagina, máximo e default são 250 items por página</param>
        /// <returns>Um resultado paginado contendo uma lista de carteiras</returns>
        /// <exception cref="ArgumentException">Parametro máx per page superior ao limite de 250 itens</exception>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billet_accounts/#listar-carteiras"/>
        public async Task<PagedApiResponse<BankBilletAccount>> GetAsync(int pageNumber, int maxPerPage = 250)
        {
            if (maxPerPage > 250)
                throw new ArgumentException("o valor máximo para o argumento maxPerPage é 250");


            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), BANK_BILLET_ACCOUNTS_API)
                                         .WithMethod(HttpMethod.Get)
                                         .AppendQuery(new Dictionary<string, string> { ["page"] = pageNumber.ToString(), ["per_page"] = maxPerPage.ToString() })
                                         .Build();

            return await _client.SendPagedAsync<BankBilletAccount>(request);
        }

        /// <summary>
        /// Solicitar homologação da Carteira de Cobrança
        /// </summary>
        /// <param name="id">Identifiador da carteira</param>
        /// <returns>Conta que foi solicitada a homologação</returns>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billet_accounts/#solicitar-homologao-da-carteira-de-cobrana"/>
        public async Task<ApiResponse<BankBilletAccount>> AskAsync(int id)
        {
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{BANK_BILLET_ACCOUNTS_API}/{id}/ask")
                                         .WithMethod(HttpMethod.Get)
                                         .Build();

            return await _client.SendAsync<BankBilletAccount>(request);
        }

        /// <summary>
        /// Validar uma Carteira de Cobrança
        /// </summary>
        /// <param name="id">Identifiador da carteira</param>
        /// <param name="homologationAmount">O valor em reais utilizado na homologação</param>
        /// <see cref="http://api.boletosimples.com.br/reference/v1/bank_billet_accounts/#validar-carteira-de-cobrana"/>
        /// <returns></returns>
        public async Task<ApiResponse<BankBilletAccount>> ValidateAsync(int id, decimal homologationAmount)
        {
            var convertedDecimal = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:N}", homologationAmount);
            var request = _requestBuilder.To(_client.Connection.GetBaseUri(), $"{BANK_BILLET_ACCOUNTS_API}/{id}/validate")
                                         .WithMethod(HttpMethod.Put)
                                         .AndOptionalContent(new { HomologationAmount = convertedDecimal })
                                         .Build();

            return await _client.SendAsync<BankBilletAccount>(request);
        }
    }
}
