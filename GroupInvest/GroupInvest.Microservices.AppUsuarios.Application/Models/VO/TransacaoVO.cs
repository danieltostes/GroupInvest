using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Models.VO
{
    public class TransacaoVO
    {
        public int ParticipanteId { get; set; }
        public int CodigoTransacao { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Descricao { get; set; }
        public decimal ValorTransacao { get; set; }
    }
}
