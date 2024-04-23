using GroupInvest.Common.Application.Events;
using System;

namespace GroupInvest.Microservices.Mensalidades.Application.Events.Adesoes
{
    #region Classes auxiliares
    public class ParticipanteVO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
    }

    public class PeriodoVO
    {
        public int Id { get; set; }
        public int AnoReferencia { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public bool Ativo { get; set; }
    }
    #endregion

    public class IntegracaoAdesaoRealizadaEvent : Event
    {
        public override string EventType => "AdesaoRealizada";

        public ParticipanteVO Participante { get; set; }
        public PeriodoVO Periodo { get; set; }
        public int NumeroCotas { get; set; }
        public DateTime DataAdesao { get; set; }
    }
}
