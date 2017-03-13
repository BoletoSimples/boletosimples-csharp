using BoletoSimplesApiClient.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BoletoSimplesApiClient.APIs.CustomerSubscriptions.Models
{
    /// <summary>
    /// Classe que representa a assinatura
    /// </summary>
    [JsonRoot("customer_subscription")]
    public sealed class CustomerSubscription
    {
        /// <summary>
        /// ID da assinatura
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
        /// Com quantos dias de antecedência à data de vencimento a cobrança será gerada.Default: 7.
        /// </summary>
        public int DaysInAdvance { get; set; }

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
        /// Data da Primeira ou Próxima cobrança.Caso não seja enviado uma data, 
        /// esse campo será calculado para ter o valor do dia da criação da assinatura mais o ciclo escolhido.Ex.: Mensal(Hoje + 30 dias).
        /// </summary>
        public DateTime NextBilling { get; private set; }

        /// <summary>
        /// Data em que deseja parar as cobranças.Caso em branco, as cobranças serão geradas automaticamente até que se informe uma data ou se exclua a assinatura.
        /// </summary>
        public DateTime? EndAt { get; set; }

        /// <summary>
        /// Descrição do produto vendido ou serviço prestado.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Instruções para o caixa
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        ///  Multa por Atraso Ex: 2% x R$ 250,00 = R$ 5,00
        /// </summary>
        public decimal FineForDelay { get; set; }

        /// <summary>
        /// Juros de Mora Mensal(O valor será dividido por 30. Ex 3% = 0,1% ao dia.)
        /// </summary>
        public decimal LatePaymentInterest { get; set; }

        /// <summary>
        /// ID do Modelo de Boleto
        /// </summary>
        public int? BankBilletLayoutId { get; set; }

        /// <summary>
        /// IDs de boletos vinculados ao carnê
        /// </summary>
        public List<int> BankBilletIds { get; set; }

        [JsonConstructor]
        public CustomerSubscription(int customerId, decimal amount, string description)
        {
            CustomerId = customerId;
            Amount = amount;
            Description = description;
        }
    }
}
