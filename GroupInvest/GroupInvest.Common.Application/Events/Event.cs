using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Common.Application.Events
{
    /// <summary>
    /// Classe base para os eventos
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Tipo do evento
        /// </summary>
        public abstract string EventType { get; }

        /// <summary>
        /// Nome da fila no serviço de mensageria
        /// </summary>
        public virtual string[] QueueNames { get; }
    }
}
