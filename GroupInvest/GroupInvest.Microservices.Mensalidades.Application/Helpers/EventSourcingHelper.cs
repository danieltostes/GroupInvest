using GroupInvest.Common.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Application.Events;
using Newtonsoft.Json;
using System;

namespace GroupInvest.Microservices.Mensalidades.Application.Helpers
{
    public static class EventSourcingHelper
    {
        public static AlteracaoDadosEvent CreateEvent(string operacao, Entidade<int> entidade)
        {
            var evento = new AlteracaoDadosEvent
            {
                Auditoria = new Models.Dtos.AuditoriaDto
                {
                    Agregado = entidade.GetType().FullName,
                    AgregadoId = entidade.Id,
                    Operacao = operacao,
                    Timestamp = DateTime.Now,
                    Conteudo = JsonConvert.SerializeObject(entidade)
                }
            };
            return evento;
        }
    }
}
