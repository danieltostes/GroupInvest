using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Entidades
{
    public static class Parametrizacao
    {
        public static decimal ValorCota { get => 10; }
        public static decimal PercentualJurosMensalidades { get => 10; }
        public static decimal PercentualJurosEmprestimos { get => 10; }
        public static decimal PercentualJurosEmprestimosAtrasados { get => 15; }
        public static int DiaVencimentoMensalidade { get => 15; }
    }
}
