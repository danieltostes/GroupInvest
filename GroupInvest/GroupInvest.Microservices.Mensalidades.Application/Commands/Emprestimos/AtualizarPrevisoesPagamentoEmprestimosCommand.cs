using GroupInvest.Common.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Application.Commands.Emprestimos
{
    public class AtualizarPrevisoesPagamentoEmprestimosCommand : Command
    {
        public DateTime DataReferencia { get; set; }

        public AtualizarPrevisoesPagamentoEmprestimosCommand(DateTime dataReferencia)
        {
            DataReferencia = dataReferencia;
        }
    }
}
