using GroupInvest.Common.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Commands.RendimentoParcial
{
    public class RegistrarRendimentoParcialCommand : Command
    {
        public DateTime DataReferencia { get; }
        public DateTime DataAtualizacao { get; }
        public decimal PercentualRendimento { get; }

        public RegistrarRendimentoParcialCommand(DateTime dataReferencia, DateTime dataAtualizacao, decimal percentualRendimento)
        {
            DataReferencia = dataReferencia;
            DataAtualizacao = dataAtualizacao;
            PercentualRendimento = percentualRendimento;
        }
    }
}
