using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs.BankBillets
{
    /// <summary>
    /// Possiveis status dos boletos
    /// </summary>
    public static class BankBilletStatus
    {
        /// <summary>
        /// Gerando
        /// </summary>
        public const string Generating = "generating";

        /// <summary>
        /// Aberto
        /// </summary>
        public const string Opened = "opened";

        /// <summary>
        /// Cancelado
        /// </summary>
        public const string Canceled = "canceled";

        /// <summary>
        /// Pago
        /// </summary>
        public const string Paid = "paid";

        /// <summary>
        ///  Vencido
        /// </summary>
        public const string Overdue = "overdue";

        /// <summary>
        /// Bloqueado
        /// </summary>
        public const string Blocked = "blocked";

        /// <summary>
        /// Estornado
        /// </summary>
        public const string Chargeback = "chargeback";
    }
}