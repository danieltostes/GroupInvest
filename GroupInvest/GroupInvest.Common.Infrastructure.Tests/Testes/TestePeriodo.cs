using GroupInvest.Common.Domain.Results;
using GroupInvest.Common.Infrastructure.Tests.Configuracao;
using GroupInvest.Common.Infrastructure.Tests.Mocks;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Participantes.Domain.Servicos;
using GroupInvest.Microservices.Participantes.Domain.Specifications;
using GroupInvest.Microservices.Participantes.Infra.DataAccess.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.CompilerServices;

namespace GroupInvest.Common.Infrastructure.Tests.Testes
{
    [TestClass]
    public class TestePeriodo : TesteBase
    {
        #region Variáveis de controle
        private readonly IServicoPeriodo servicoPeriodo;
        private readonly IServicoParticipante servicoParticipante;

        private bool LimparDados;
        #endregion

        #region Construtor
        public TestePeriodo()
        {
            servicoPeriodo = new ServicoPeriodo(repositorioPeriodo, repositorioAdesao);
            servicoParticipante = new ServicoParticipante(repositorioParticipante, repositorioAdesao, repositorioPeriodo);
        }
        #endregion

        #region Initialize e CleanUp
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (LimparDados)
            {
                ExcluirRegistros();
            }
        }
        #endregion

        #region Validações
        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarPeriodoSemDataInicio()
        {
            var periodo = Mock.CriarPeriodo();
            periodo.DataInicio = null;

            var specification = new PeriodoSpecificationDataInicioInformada();
            var result = specification.IsSatisfiedBy(periodo);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarPeriodoSemDataTermino()
        {
            var periodo = Mock.CriarPeriodo();
            periodo.DataTermino = null;

            var specification = new PeriodoSpecificationDataTerminoInformada();
            var result = specification.IsSatisfiedBy(periodo);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarPeriodoAnoReferenciaMenorQue2020()
        {
            var periodo = Mock.CriarPeriodo();
            periodo.AnoReferencia = 2019;

            var specification = new PeriodoSpecificationAnoReferenciaValido();
            var result = specification.IsSatisfiedBy(periodo);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarInclusaoPeriodoAnoReferenciaExistente()
        {
            var periodo = Mock.CriarPeriodo();
            servicoPeriodo.IncluirPeriodo(periodo);

            var novoPeriodo = Mock.CriarPeriodo();
            var result = servicoPeriodo.IncluirPeriodo(novoPeriodo);

            LimparDados = true;

            Assert.AreEqual(StatusCodeEnum.Error, result.StatusCode);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarAlteracaoPeriodoComMensalidadesPagas()
        {
            Assert.Fail("Teste não implementado");
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarExclusaoPeriodoComAdesaoAtiva()
        {
            var adesao = Mock.CriarAdesao();
            servicoParticipante.IncluirParticipante(adesao.Participante);
            servicoPeriodo.IncluirPeriodo(adesao.Periodo);
            servicoParticipante.RealizarAdesaoParticipantePeriodoAtivo(adesao.Participante, 100);

            var result = servicoPeriodo.ExcluirPeriodo(adesao.Periodo);

            LimparDados = true;            
            Assert.AreEqual(StatusCodeEnum.Error, result.StatusCode);
        }
        #endregion

        #region Persistência
        [TestMethod]
        [TestCategory("Persistência")]
        public void DeveIncluirPeriodo()
        {
            var periodo = Mock.CriarPeriodo();
            var result = servicoPeriodo.IncluirPeriodo(periodo);

            LimparDados = true;

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }

        [TestMethod]
        [TestCategory("Persistência")]
        public void DeveAlterarPeriodo()
        {
            var periodo = Mock.CriarPeriodo();
            servicoPeriodo.IncluirPeriodo(periodo);

            var periodoIncluido = servicoPeriodo.ObterPeriodoPorAnoReferencia(periodo.AnoReferencia);
            var novaDataInicio = new DateTime(2020, 1, 1);
            var novaDataTermino = new DateTime(2020, 12, 31);

            periodoIncluido.DataInicio = novaDataInicio;
            periodoIncluido.DataTermino = novaDataTermino;

            var result = servicoPeriodo.AlterarPeriodo(periodoIncluido);
            var periodoAlterado = servicoPeriodo.ObterPeriodoPorAnoReferencia(periodo.AnoReferencia);

            LimparDados = true;

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
            Assert.AreEqual(novaDataInicio, periodoAlterado.DataInicio.GetValueOrDefault());
            Assert.AreEqual(novaDataTermino, periodoAlterado.DataTermino.GetValueOrDefault());
        }

        [TestMethod]
        [TestCategory("Persistência")]
        public void DeveExcluirPeriodo()
        {
            var periodo = Mock.CriarPeriodo();
            servicoPeriodo.IncluirPeriodo(periodo);

            var periodoIncluido = servicoPeriodo.ObterPeriodoPorAnoReferencia(periodo.AnoReferencia);

            var result = servicoPeriodo.ExcluirPeriodo(periodoIncluido);
            var periodoExcluido = servicoPeriodo.ObterPeriodoPorAnoReferencia(periodo.AnoReferencia);

            LimparDados = true;

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
            Assert.IsNull(periodoExcluido);
        }
        #endregion
    }
}
