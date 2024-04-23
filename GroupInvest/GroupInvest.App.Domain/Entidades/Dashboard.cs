using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Domain.Entidades
{
    public class Dashboard
    {
        public decimal SaldoAcumulado { get; set; }
        public decimal RendimentoParcial { get; set; }
        public List<Transacao> UltimasTransacoes { get; set; }
        public List<RendimentoParcialPeriodo> RendimentosParciais { get; set; }

        public Dashboard()
        {
            UltimasTransacoes = new List<Transacao>();
            RendimentosParciais = new List<RendimentoParcialPeriodo>();
        }
    }
}
