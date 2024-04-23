using GroupInvest.Common.Domain.Entidades;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Entidades
{
    public class Mensalidade
    {
        // Mapeamento do MongoDB
        public ObjectId _id { get; set; }

        public int MensalidadeId { get; set; }
        public int ParticipanteId { get; set; }
        public string NomeParticipante { get; set; }
        public DateTime DataReferencia { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorBase { get; set; }
        public decimal ValorDevido { get; set; }

        public string Situacao
        {
            get { return Pagamento == null ? "Em aberto" : "Pago"; }
        }

        public PagamentoMensalidade Pagamento { get; set; }
    }
}
