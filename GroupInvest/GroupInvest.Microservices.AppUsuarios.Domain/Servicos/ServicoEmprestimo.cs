using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Servicos
{
    public class ServicoEmprestimo : IServicoEmprestimo
    {
        #region Injeção de dependência
        private readonly IRepositorioEmprestimo repositorioEmprestimo;
        #endregion

        #region Construtor
        public ServicoEmprestimo(IRepositorioEmprestimo repositorioEmprestimo)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
        }
        #endregion

        #region Serviços
        public OperationResult IncluirEmprestimo(Emprestimo emprestimo)
        {
            try { repositorioEmprestimo.Incluir(emprestimo); }
            catch(Exception ex) { return new OperationResult(StatusCodeEnum.Error, ex.Message); }

            return OperationResult.OK;
        }

        public OperationResult AlterarEmprestimo(Emprestimo emprestimo)
        {
            try { repositorioEmprestimo.Alterar(emprestimo); }
            catch (Exception ex) { return new OperationResult(StatusCodeEnum.Error, ex.Message); }

            return OperationResult.OK;
        }

        public OperationResult AtualizarSaldoEmprestimo(Emprestimo emprestimo)
        {
            try
            {
                repositorioEmprestimo.AtualizarSaldoEmprestimo(emprestimo);
                //if (emprestimo.ProximoPagamento != null)
                //    repositorioEmprestimo.AtualizarProximoPagamentoEmprestimo(emprestimo);
            }
            catch(Exception ex) { return new OperationResult(StatusCodeEnum.Error, ex.Message); }

            return OperationResult.OK;
        }

        public OperationResult AdicionarPagamento(Emprestimo emprestimo, PagamentoEmprestimo pagamento)
        {
            try { repositorioEmprestimo.AdicionarPagamento(emprestimo, pagamento); }
            catch(Exception ex) { return new OperationResult(StatusCodeEnum.Error, ex.Message); }

            return OperationResult.OK;
        }

        public IReadOnlyCollection<Emprestimo> ListarEmprestimosParticipante(int participanteId)
        {
            return repositorioEmprestimo.ListarEmprestimosParticipante(participanteId);
        }

        public Emprestimo ObterEmprestimoPorId(int id)
        {
            return repositorioEmprestimo.ObterEmprestimoPorId(id);
        }
        #endregion
    }
}
