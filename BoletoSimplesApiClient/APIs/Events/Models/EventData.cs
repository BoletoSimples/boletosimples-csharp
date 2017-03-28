using BoletoSimplesApiClient.Common;
using System;
using System.Collections.Generic;

namespace BoletoSimplesApiClient.APIs.Events.Models
{
    [JsonRoot("event")]
    public class EventData
    {
        /// <summary>
        /// ID do carnê
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Código do evento
        /// </summary>
        /// <see cref="http://api.boletosimples.com.br/webhooks/events"/>
        public string Code { get; set; }

        /// <summary>
        /// Mais informações relativas ao evento
        /// </summary>
        /// <see cref="http://api.boletosimples.com.br/webhooks/payloads"/>
        public EventDetail Data { get; set; }

        /// <summary>
        /// ID do carnê
        /// </summary>
        public DateTime? OccurredAt { get; set; }
    }

    public class EventDetail
    {
        public object Object { get; set; }
        public Dictionary<string, string[]> Changes { get; set; }
    }
}
