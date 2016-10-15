using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoletoSimplesApiClient.APIs
{
    public static class Constants
    {
        /// <summary>
        /// Possíveis valores para o ciclo de um carnê
        /// </summary>
        public static class Cycles
        {
            /// <summary>
            /// Quinzenal
            /// </summary>
            public const string BIWEEKLY = "biweekly";
            /// <summary>
            ///  Bimestral
            /// </summary>
            public const string BIMONTHLY = "bimonthly";

            /// <summary>
            /// Mensal
            /// </summary>
            public const string MONTHLY = "monthly";
            /// <summary>
            /// Trimestral
            /// </summary>
            public const string QUARTERLY = "quarterly";
            /// <summary>
            /// Semestral
            /// </summary>
            public const string SEMIANNUAL = "semiannual";

            /// <summary>
            /// Anual
            /// </summary>
            public const string ANNUAL = "annual";
        }

        /// <summary>
        /// Possíveis valores de status
        /// </summary>
        public static class Status
        {
            /// <summary>
            /// Criado
            /// </summary>
            public const string CREATED = "created";

            /// <summary>
            /// Aberto
            /// </summary>
            public const string PROCESSED = "processed";

            /// <summary>
            /// Finalizado
            /// </summary>
            public const string FINISHED = "finished";

            /// <summary>
            /// Gerado
            /// </summary>
            public const string GENERATING = "generating";

            /// <summary>
            /// Aberto
            /// </summary>
            public const string OPENED = "opened";

            /// <summary>
            /// Cancelado
            /// </summary>
            public const string CANCELED = "canceled";

            /// <summary>
            /// Pago
            /// </summary>
            public const string PAID = "paid";

            /// <summary>
            /// Vencido
            /// </summary>
            public const string OVERDUE = "overdue";

            /// <summary>
            /// Bloqueado
            /// </summary>
            public const string BLOCKED = "blocked";

            /// <summary>
            /// Estornado
            /// </summary>
            public const string CHARGEBACK = "Estornado";
        }
    }
}
