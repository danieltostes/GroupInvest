using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Entidades
{
    public class Transacao
    {
        // Mapeamento do MongoDB
        public ObjectId _id { get; set; }

        public int ParticipanteId { get; set; }
        public int CodigoTransacao { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Descricao { get; set; }
        public decimal ValorTransacao { get; set; }
    }
}
