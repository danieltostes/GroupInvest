using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Entidades
{
    public class RendimentoParcialPeriodo
    {
        public ObjectId _id { get; set; }
        public DateTime DataReferencia { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public decimal PercentualRendimento { get; set; }
    }
}
