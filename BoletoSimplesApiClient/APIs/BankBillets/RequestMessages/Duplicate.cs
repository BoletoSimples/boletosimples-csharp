using BoletoSimplesApiClient.Utils;
using Newtonsoft.Json;

namespace BoletoSimplesApiClient.APIs.BankBillets.RequestMessages
{
    public class Duplicate
    {
        /// <summary>
        /// Nº de dias para vencimento a partir da data de hoje (Default: 7)
        /// </summary>
        public int ExpireAtInDays { get; set; } = 7;
        /// <summary>
        /// Cancelar o boleto que está sendo duplicado(Default: true)
        /// </summary>
        public bool Cancel { get; set; } = true;

        /// <summary>
        /// Um possivel novo valor do novo boleto
        /// </summary>
        [JsonConverter(typeof(BrazilianCurrencyJsonConverter))]
        public decimal Amount { get; set; }

        /// <summary>
        /// Atualizar o valor do novo boleto com juros e multa (Default: false)
        /// </summary>
        public bool WithFines { get; set; }
    }
}
