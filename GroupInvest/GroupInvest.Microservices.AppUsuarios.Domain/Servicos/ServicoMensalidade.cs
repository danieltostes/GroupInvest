using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Servicos
{
    public class ServicoMensalidade : IServicoMensalidade
    {
        #region Injeção de dependência
        private readonly IRepositorioMensalidade repositorioMensalidade;
        #endregion

        #region Construtor
        public ServicoMensalidade(IRepositorioMensalidade repositorioMensalidade)
        {
            this.repositorioMensalidade = repositorioMensalidade;
        }
        #endregion

        #region IServicoMensalidade
        public OperationResult IncluirMensalidade(Mensalidade mensalidade)
        {
            try { repositorioMensalidade.Incluir(mensalidade); }
            catch (Exception ex) { return new OperationResult(StatusCodeEnum.Error, ex.Message); }

            return OperationResult.OK;
        }

        public OperationResult AlterarMensalidade(Mensalidade mensalidade)
        {
            try { repositorioMensalidade.Alterar(mensalidade); }
            catch (Exception ex) { return new OperationResult(StatusCodeEnum.Error, ex.Message); }

            return OperationResult.OK;
        }

        public OperationResult IncluirPagamento(Mensalidade mensalidade, PagamentoMensalidade pagamento)
        {
            try { repositorioMensalidade.IncluirPagamento(mensalidade, pagamento); }
            catch(Exception ex) { return new OperationResult(StatusCodeEnum.Error, ex.Message); }

            return OperationResult.OK;
        }

        public IReadOnlyCollection<Mensalidade> ListarMensalidadesParticipante(int participanteId)
        {
            return repositorioMensalidade.ListarMensalidadesParticipante(participanteId);
        }

        public Mensalidade ObterMensalidadePorId(int id)
        {
            return repositorioMensalidade.ObterMensalidadePorId(id);
        }

        public decimal ObterValorAcumuladoParticipante(int participanteId)
        {
            var mensalidades = repositorioMensalidade.ListarMensalidadesPagasParticipante(participanteId);
            return mensalidades.Sum(mens => mens.Pagamento.ValorPago - mens.Pagamento.ValorJuros);
        }
        #endregion
    }
}
