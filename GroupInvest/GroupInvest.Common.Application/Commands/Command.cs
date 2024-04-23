using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Common.Application.Commands
{
    /// <summary>
    /// Classe base para os comandos
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Data e hora do comando
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Noma da fila no serviço de mensageria
        /// </summary>
        public virtual string QueueName { get; }
    }
}
