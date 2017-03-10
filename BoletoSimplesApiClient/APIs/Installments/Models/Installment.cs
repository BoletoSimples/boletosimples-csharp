using BoletoSimplesApiClient.APIs.BankBillets.Models;
using BoletoSimplesApiClient.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.Installments.Models
{
    /// <summary>
    /// Classe que representa o boleto
    /// </summary>
    [JsonRoot("installment")]
    public sealed class Installment
    {
        /// <summary>
        /// ID do carnê
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id do cliente
        /// </summary>
        public int CustomerId { get; private set; }

        /// <summary>
        /// ID da Carteira de Cobrança.Se não informado, usará a carteira padrão.
        /// </summary>
        public int BankBilletAccountId { get; set; }

        /// <summary>
        /// Preço do carnê em (R$) Formato: 1234.34
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Ciclo da carnê(possíveis valores). Default: monthly
        /// Use InstallmentsConstants.Cycles para enviar os valores corretos
        /// </summary>
        public string Cycle { get; set; }

        /// <summary>
        /// Data da Primeira cobrança.
        /// </summary>
        public DateTime StartAt { get; private set; }

        /// <summary>
        /// Data da última cobrança.
        /// </summary>
        public DateTime EndAt { get; set; }

        /// <summary>
        /// Quantidade de parcelas
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// Descrição do produto vendido ou serviço prestado.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Instruções para o caixa
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        /// Situação do carnê
        /// Use InstallmentsConstants.Status para enviar os valores corretos
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Multa por Atraso
        /// </summary>
        public decimal FineForDelay { get; set; }

        /// <summary>
        /// Juros de Mora
        /// </summary>
        public decimal LatePaymentInterest { get; set; }

        /// <summary>
        /// ID do Modelo de Boleto
        /// </summary>
        public int? BankBilletLayoutId { get; set; }

        /// <summary>
        /// URL para visualização do carnê
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// IDs de boletos vinculados ao carnê
        /// </summary>
        public List<int> BankBilletIds { get; set; }

        /// <summary>
        /// Boletos vinculados ao carnê
        /// </summary>
        public List<BankBillet> BankBillets { get; set; }

        [JsonConstructor]
        public Installment(int customerId, decimal amount, DateTime startAt, int total, string description)
        {
            CustomerId = customerId;
            Amount = amount;
            StartAt = startAt;
            Total = total;
            Description = description;
        }
    }
}
