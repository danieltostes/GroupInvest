using GroupInvest.Common.Application.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Auditoria.Application.Events
{
    public class RegistroAuditoriaBaseIncluidoEvent : Event
    {
        public override string EventType => "RegistroAuditoriaBaseIncluido";

        #region Propriedades
        public int Id { get; set; }
        public int AgregadoId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Operacao { get; set; }
        public string Conteudo { get; set; }
        #endregion
    }
}
