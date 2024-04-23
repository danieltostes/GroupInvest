using AutoMapper;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.APIs;
using GroupInvest.Microservices.AppUsuarios.Application.Models.Dtos;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Application
{
    public class DashboardApi : IDashboardApi
    {
        #region Injeção de dependência
        private readonly IServicoMensalidade servicoMensalidade;
        private readonly IServicoTransacao servicoTransacao;
        private readonly IServicoRendimentoParcialPeriodo servicoRendimentoParcial;
        #endregion

        #region Construtor
        public DashboardApi(IServicoMensalidade servicoMensalidade, IServicoTransacao servicoTransacao, IServicoRendimentoParcialPeriodo servicoRendimentoParcial)
        {
            this.servicoMensalidade = servicoMensalidade;
            this.servicoTransacao = servicoTransacao;
            this.servicoRendimentoParcial = servicoRendimentoParcial;
        }
        #endregion

        #region IDashboardApi
        public DashboardDto ObterDashboardParticipante(int participanteId)
        {
            var valorAcumulado = servicoMensalidade.ObterValorAcumuladoParticipante(participanteId);
            var transacoes = servicoTransacao.ListarUltimasTransacoesParticipante(participanteId);
            var rendimentosParciais = servicoRendimentoParcial.ListarRendimentosParciais();

            var dto = new DashboardDto();
            dto.SaldoAcumulado = valorAcumulado;

            foreach (var transacao in transacoes.OrderByDescending(trans => trans.DataTransacao))
            {
                var transacaoDto = new TransacaoDto
                {
                    CodigoTransacao = transacao.CodigoTransacao,
                    DataTransacao = transacao.DataTransacao,
                    Descricao = transacao.Descricao,
                    ValorTransacao = transacao.ValorTransacao
                };
                dto.UltimasTransacoes.Add(transacaoDto);
            }

            foreach (var rendimento in rendimentosParciais)
            {
                var rendimentoDto = new RendimentoParcialPeriodoDto
                {
                    DataReferencia = rendimento.DataReferencia,
                    PercentualRendimento = rendimento.PercentualRendimento
                };
                dto.RendimentosParciais.Add(rendimentoDto);
            }

            if (rendimentosParciais.Count > 0)
                dto.RendimentoParcial = rendimentosParciais.OrderByDescending(rend => rend.DataReferencia).First().PercentualRendimento;

            return dto;
        }
        #endregion
    }
}
