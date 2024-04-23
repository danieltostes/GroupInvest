using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Auditoria.Application.Commands.AuditoriaBase
{
    public class IncluirAuditoriaBaseCommand : AuditoriaBaseCommand
    {
        public override string QueueName => "inclusao-auditoria-command-queue";

        public IncluirAuditoriaBaseCommand(int agregadoId, string agregado, string operacao, string conteudo, DateTime timestamp)
        {
            AgregadoId = agregadoId;
            Agregado = agregado;
            Operacao = operacao;
            Conteudo = conteudo;
            Timestamp = timestamp;
        }
    }
}
