using GroupInvest.Common.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Auditoria.Application.Commands.AuditoriaBase
{
    public abstract class AuditoriaBaseCommand : Command
    {
        public int AuditoriaId { get; set; }
        public int AgregadoId { get; set; }
        public string Agregado { get; set; }
        public string Operacao { get; set; }
        public string Conteudo { get; set; }
    }
}
