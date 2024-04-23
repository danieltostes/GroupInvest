using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Commands.Emprestimos;
using GroupInvest.Microservices.Mensalidades.Application.Interfaces;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace GroupInvest.Microservices.Mensalidades.Application
{
    public class EmprestimoApi : IEmprestimoApi
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private readonly IMapper mapper;
        private readonly IServicoEmprestimo servicoEmprestimo;
        private readonly IServicoParticipante servicoParticipante;
        #endregion

        #region Construtor
        public EmprestimoApi(IMediatorHandler bus, IMapper mapper, IServicoEmprestimo servicoEmprestimo, IServicoParticipante servicoParticipante)
        {
            this.bus = bus;
            this.mapper = mapper;
            this.servicoEmprestimo = servicoEmprestimo;
            this.servicoParticipante = servicoParticipante;
        }
        #endregion

        #region IEmprestimoApi
        public OperationResult ConcederEmprestimo(ConcessaoEmprestimoDto dto)
        {
            var command = mapper.Map<ConcessaoEmprestimoDto, ConcederEmprestimoCommand>(dto);
            return bus.SendCommand(command);
        }

        public OperationResult AtualizarSaldoEmprestimosIntegracao(DateTime dataReferencia)
        {
            var command = new AtualizarPrevisoesPagamentoEmprestimosCommand(dataReferencia);
            return bus.SendCommand(command);
        }

        public EmprestimoDto ObterEmprestimoParticipante(int participanteId, DateTime dataEmprestimo)
        {
            var participante = servicoParticipante.ObterParticipantePorId(participanteId);
            if (participante == null)
                return null;

            var emprestimo = servicoEmprestimo.ObterEmprestimoParticipante(participante, dataEmprestimo);
            var emprestimoDto = mapper.Map<Emprestimo, EmprestimoDto>(emprestimo);

            return emprestimoDto;
        }

        public IReadOnlyCollection<EmprestimoDto> ListarEmprestimosParticipante(int participanteId)
        {
            var lista = new List<EmprestimoDto>();

            var participante = servicoParticipante.ObterParticipantePorId(participanteId);
            if (participante == null)
                return lista;

            var emprestimos = servicoEmprestimo.ListarEmprestimosParticipante(participante);
            if (emprestimos.Count > 0)
            {
                foreach (var emprestimo in emprestimos)
                    lista.Add(mapper.Map<Emprestimo, EmprestimoDto>(emprestimo));
            }
            return lista;
        }

        public IReadOnlyCollection<EmprestimoDto> ListarTodosEmprestimos()
        {
            var lista = new List<EmprestimoDto>();
            var emprestimos = servicoEmprestimo.ListarTodosEmprestimos();
            if (emprestimos.Count > 0)
            {
                foreach (var emprestimo in emprestimos)
                    lista.Add(mapper.Map<Emprestimo, EmprestimoDto>(emprestimo));
            }
            return lista;
        }

        public IReadOnlyCollection<PrevisaoPagamentoEmprestimoDto> ListarPrevisoesPagamentoEmprestimo(int emprestimoId)
        {
            var emprestimo = servicoEmprestimo.ObterEmprestimoPorId(emprestimoId);
            if (emprestimo == null)
                return null;

            //var result = servicoEmprestimo.AtualizarPrevisaoPagamentoEmprestimo(emprestimo, DateTime.Today);
            //if (result.StatusCode == StatusCodeEnum.Error)
            //    throw new Exception(string.Format("Erro ao atualizar previsões de pagamento do empréstimo Id: {0}.{1}{2}", emprestimo.Id, Environment.NewLine, result.Messages.ElementAt(0)));

            var previsoesPagamento = servicoEmprestimo.ListarPrevisoesPagamentoEmprestimo(emprestimo, true);
            return mapper.Map<IReadOnlyCollection<PrevisaoPagamentoEmprestimo>, IReadOnlyCollection<PrevisaoPagamentoEmprestimoDto>>(previsoesPagamento);
        }
        #endregion
    }
}
