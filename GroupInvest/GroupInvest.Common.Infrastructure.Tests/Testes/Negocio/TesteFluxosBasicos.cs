using GroupInvest.Common.Domain.Results;
using GroupInvest.Common.Infrastructure.Tests.Configuracao;
using GroupInvest.Common.Infrastructure.Tests.Mocks;
using GroupInvest.Microservices.Participantes.Application;
using GroupInvest.Microservices.Participantes.Application.Interfaces;
using GroupInvest.Microservices.Participantes.Application.Models.Dtos;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Participantes.Domain.Servicos;
using GroupInvest.Microservices.Participantes.Infra.DataAccess.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GroupInvest.Common.Infrastructure.Tests.Testes.Negocio
{
    [TestClass]
    public class TesteFluxosBasicos
    {
        #region Varáveis de controles
        public IServicoParticipante servicoParticipante;
        public IServicoPeriodo servicoPeriodo;

        public IRepositorioAdesao repositorioAdesao;
        public IRepositorioParticipante repositorioParticipante;
        public IRepositorioPeriodo repositorioPeriodo;

        public IParticipanteApi participanteAPI;
        public IPeriodoApi periodoAPI;

        public TestContext TestContext { get; set; }
        #endregion

        #region Construtor
        public TesteFluxosBasicos()
        {
            var dbContext = InMemoryBusTest.Instance().GetDbContext(typeof(IServicoParticipante).FullName);
            repositorioAdesao = new RepositorioAdesao(dbContext);
            repositorioParticipante = new RepositorioParticipante(dbContext);
            repositorioPeriodo = new RepositorioPeriodo(dbContext);

            servicoParticipante = new ServicoParticipante(repositorioParticipante, repositorioAdesao, repositorioPeriodo);
            servicoPeriodo = new ServicoPeriodo(repositorioPeriodo, repositorioAdesao);

            participanteAPI = new ParticipanteApi(InMemoryBusTest.Instance(),
                servicoParticipante,
                AutoMapperConfig.Mapper());

            periodoAPI = new PeriodoApi(InMemoryBusTest.Instance(),
                servicoPeriodo,
                AutoMapperConfig.Mapper());
        }
        #endregion

        #region Cadastramento
        [TestMethod]
        [TestCategory("Negócio")]
        [Priority(0)]
        public void IncluirParticipantes()
        {
            OperationResult result;

            var periodo = Mock.CriarPeriodoDto();
            result = periodoAPI.IncluirPeriodo(periodo);

            var participantes = Mock.CriarParticipantes();
            foreach (var participante in participantes)
            {
                result = participanteAPI.IncluirParticipante(participante);

                var participanteIncluido = participanteAPI.ObterParticipantePorEmail(participante.Email);

                result = participanteAPI.RealizarAdesaoParticipantePeriodoAtivo(participanteIncluido.Id, 100);
            }

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory("Negócio")]
        public void IncluirParticipante()
        {
            var participante = new ParticipanteDto
            {
                Nome = "Jorge Silva Santana",
                Email = "santana.rai@bol.com.br",
                Telefone = "3765-4325",
                Ativo = true
            };
            var result = participanteAPI.IncluirParticipante(participante);

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory("Negócio")]
        public void FluxoAdesaoParticipante()
        {
            var periodo = new PeriodoDto
            {
                AnoReferencia = 2020,
                DataInicio = new DateTime(2019, 12, 1),
                DataTermino = new DateTime(2020, 11, 30),
                Ativo = true
            };
            periodoAPI.IncluirPeriodo(periodo);

            var participante = new ParticipanteDto
            {
                Nome = "Daniel Tostes",
                Email = "daniel.tostes@gmail.com",
                Telefone = "99778-5791",
                Ativo = true
            };
            participanteAPI.IncluirParticipante(participante);

            var participanteIncluido = participanteAPI.ObterParticipantePorEmail(participante.Email);
            var result = participanteAPI.RealizarAdesaoParticipantePeriodoAtivo(participanteIncluido.Id, 100);

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }
        #endregion
    }
}
