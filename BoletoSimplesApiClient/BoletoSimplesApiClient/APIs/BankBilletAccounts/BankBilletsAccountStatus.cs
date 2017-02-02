using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.BankBilletAccounts
{
    public static class BankBilletsAccountStatus
    {
        /// <summary>
        /// Homologação não iniciada
        /// </summary>
        public const string Pending = "pending";

        /// <summary>
        /// Em homologação, aguardando pagamento do boleto
        /// </summary>
        public const string Homologating = "homologating";

        /// <summary>
        /// Boleto pago, aguardando validação
        /// </summary>
        public const string Validating = "validating";

        /// <summary>
        /// Ativa e pronta para uso
        /// </summary>
        public const string Active = "active";
    }
}
