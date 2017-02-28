using BoletoSimplesApiClient.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BoletoSimplesApiClient.APIs.Remittances.Models
{
    /// <summary>
    /// Modelo que representa o arquivo CNAB de remessa
    /// </summary>
    [JsonRoot("remittance")]
    public sealed class Remittance
    {
        /// <summary>
        /// ID do CNAB (Remesa)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do Arquivo
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
        /// unprocessed (pendente) - processed (processado) - downloaded(Baixada pelo usuário) - sent(enviado para o banco)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Data de Criação
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Url do arquivo
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// ID da Carteira de Cobrança
        /// </summary>
        public int BankBilletAccountId { get; private set; }

        /// <summary>
        /// IDs de boletos vinculados a remessa
        /// </summary>
        public List<int> BankBilletIds { get; set; }

        /// <summary>
        /// Número da remessa
        /// </summary>
        public int RemittanceNumber { get; set; }

        /// <summary>
        /// Data de envio automático para o banco
        /// </summary>
        public DateTime? SentViaIntegration { get; set; }

        [JsonConstructor]
        public Remittance(int bankBilletAccountId)
        {
            BankBilletAccountId = bankBilletAccountId;
        }
    }
}
