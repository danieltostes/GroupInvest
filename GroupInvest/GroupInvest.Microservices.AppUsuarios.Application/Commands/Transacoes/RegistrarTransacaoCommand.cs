using GroupInvest.Common.Application.Commands;
using GroupInvest.Microservices.AppUsuarios.Application.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Commands.Transacoes
{
    public class RegistrarTransacaoCommand : Command
    {
        public TransacaoVO Transacao { get; set; }

        public RegistrarTransacaoCommand(TransacaoVO transacao)
        {
            Transacao = transacao;
        }
    }
}
