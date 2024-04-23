using GroupInvest.App.Domain.Entidades;
using GroupInvest.App.Domain.Interfaces.Repositorios;
using GroupInvest.App.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Domain.Servicos
{
    public class ServicoDashboard : IServicoDashboard
    {
        public string Token { get; set; }

        #region Injeção de dependência
        private readonly IRepositorioDashboard repositorioDashboard;
        #endregion

        #region Construtor
        public ServicoDashboard(IRepositorioDashboard repositorioDashboard)
        {
            this.repositorioDashboard = repositorioDashboard;
        }
        #endregion

        #region IServicoDashboard
        public Dashboard ObterDashboardParticipante(int participanteId)
        {
            repositorioDashboard.Token = Token;
            var dashboard = repositorioDashboard.ObterDashboardParticipante(participanteId);

            foreach (var transacao in dashboard.UltimasTransacoes)
            {
                transacao.Icone = transacao.CodigoTransacao.Equals(1) ? "icone_pagamento_mensalidade.png" : "icone_emprestimo.png";
            }

            return dashboard;
        }
        #endregion
    }
}
