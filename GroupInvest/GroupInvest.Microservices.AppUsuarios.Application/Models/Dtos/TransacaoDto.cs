using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Models.Dtos
{
    public class TransacaoDto
    {
        public int CodigoTransacao { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Descricao { get; set; }
        public decimal ValorTransacao { get; set; }
    }
}
