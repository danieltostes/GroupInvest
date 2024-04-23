using GroupInvest.Common.Application.Commands;
using GroupInvest.Microservices.AppUsuarios.Application.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application.Commands.Emprestimos
{
    public class RegistrarEmprestimoConcedidoCommand : Command
    {
        public EmprestimoVO Emprestimo { get; set; }
    }
}
