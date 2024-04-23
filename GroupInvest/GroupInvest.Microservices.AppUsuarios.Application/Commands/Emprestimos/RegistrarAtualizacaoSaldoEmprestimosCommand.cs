using GroupInvest.Common.Application.Commands;
using GroupInvest.Microservices.AppUsuarios.Application.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Commands.Emprestimos
{
    public class RegistrarAtualizacaoSaldoEmprestimosCommand : Command
    {
        public IEnumerable<SaldoEmprestimoVO> Saldos { get; }

        public RegistrarAtualizacaoSaldoEmprestimosCommand(IEnumerable<SaldoEmprestimoVO> saldos)
        {
            this.Saldos = saldos;
        }
    }
}
