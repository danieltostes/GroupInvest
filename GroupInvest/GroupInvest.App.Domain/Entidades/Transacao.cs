using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Domain.Entidades
{
    public class Transacao
    {
        public int CodigoTransacao { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Descricao { get; set; }
        public decimal ValorTransacao { get; set; }
        public string Icone { get; set; }
    }
}
