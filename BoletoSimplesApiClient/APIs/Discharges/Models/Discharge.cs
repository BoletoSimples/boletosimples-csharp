using BoletoSimplesApiClient.APIs.BankBillets.Models;
using System;
using System.Collections.Generic;

namespace BoletoSimplesApiClient.APIs.Discharges.Models
{
    /// <summary>
    /// Modelo que repesenta o arquivo de retorno
    /// </summary>
    public sealed class Discharge
    {
        /// <summary>
        /// ID do CNAB
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Arquivo
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Data de Processamento
        /// </summary>
        public DateTime? ProcessedAt { get; set; }

        /// <summary>
        /// Enviado via Api
        /// </summary>
        public bool CreatedViaApi { get; set; }

        /// <summary>
        /// Situação do arquivo
        /// unprocessed (pendente) - processed (processado)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// ID da Carteira de Cobrança
        /// </summary>
        public int? BankBilletAccountId { get; set; }

        /// <summary>
        /// Data de recebimento automático do banco
        /// </summary>
        public DateTime? CreatedViaIntegration { get; set; }

        /// <summary>
        /// Transações associadas ao arquivo de retorno
        /// </summary>
        public List<BankBillet> DischargeTransactions { get; set; }
    }
}
