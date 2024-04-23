using GroupInvest.Common.Domain.Results;
using GroupInvest.Common.Infrastructure.Tests.Configuracao;
using GroupInvest.Common.Infrastructure.Tests.Mocks;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Participantes.Domain.Servicos;
using GroupInvest.Microservices.Participantes.Domain.Specifications;
using GroupInvest.Microservices.Participantes.Infra.DataAccess.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GroupInvest.Common.Infrastructure.Tests.Testes
{
    [TestClass]
    public class TestesParticipante : TesteBase
    {
        #region Variáveis de Controle
        private bool LimparDados;
        private IServicoParticipante servicoParticipante;
        private IServicoPeriodo servicoPeriodo;
        #endregion

        #region Construtor
        public TestesParticipante()
        {
            LimparDados = false;

            servicoParticipante = new ServicoParticipante(repositorioParticipante, repositorioAdesao, repositorioPeriodo);
            servicoPeriodo = new ServicoPeriodo(repositorioPeriodo, repositorioAdesao);
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
        public void DeveCriticarNomeParticipanteNulo()
        {
            var participante = Mock.CriarParticipante();
            participante.Nome = null;

            var specification = new ParticipanteSpecificationNomePreenchido();
            var result = specification.IsSatisfiedBy(participante);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarNomeComMenosDeTresCaracteres()
        {
            var participante = Mock.CriarParticipante();
            participante.Nome = "da";

            var specification = new ParticipanteSpecificationNomeMinimoCaracteres();
            var result = specification.IsSatisfiedBy(participante);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarTelefoneComMenosDeNoveCaracteres()
        {
            var participante = Mock.CriarParticipante();
            participante.Telefone = "123";

            var specification = new ParticipanteSpecificationTelefoneMinimoCaracteres();
            var result = specification.IsSatisfiedBy(participante);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarEmailUnicoParticipante()
        {
            var participante = Mock.CriarParticipante();
            servicoParticipante.IncluirParticipante(participante);

            var specification = new ParticipanteSpecificationEmailUnico(repositorioParticipante);
            var novoParticipante = Mock.CriarParticipante();

            var result = specification.IsSatisfiedBy(novoParticipante);

            LimparDados = true;

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarInclusaoDeParticipanteInativo()
        {
            var participante = Mock.CriarParticipante();
            participante.Ativo = false;

            var result = servicoParticipante.IncluirParticipante(participante);

            // Limpa os dados para casos que o teste falhe inesperadamente
            LimparDados = true;

            Assert.AreEqual(StatusCodeEnum.Error, result.StatusCode);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarAdesaoSemParticipante()
        {
            var adesao = Mock.CriarAdesao();
            adesao.Participante = null;

            var specification = new AdesaoSpecificationParticipanteInformado();
            var result = specification.IsSatisfiedBy(adesao);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarAdesaoSemPeriodo()
        {
            var adesao = Mock.CriarAdesao();
            adesao.Periodo = null;

            var specification = new AdesaoSpecificationPeriodoInformado();
            var result = specification.IsSatisfiedBy(adesao);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarAdesaoSemNumeroCotas()
        {
            var adesao = Mock.CriarAdesao();
            adesao.NumeroCotas = 0;

            var specification = new AdesaoSpecificationNumeroMinimoCotas();
            var result = specification.IsSatisfiedBy(adesao);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarAlteracaoParticipanteStatusAtivoFalso()
        {
            var participante = Mock.CriarParticipante();
            servicoParticipante.IncluirParticipante(participante);

            participante.Ativo = false;
            var result = servicoParticipante.AlterarParticipante(participante);

            LimparDados = true;

            Assert.AreEqual(StatusCodeEnum.Error, result.StatusCode);
        }

        [TestMethod]
        [TestCategory("Validações")]
        public void DeveCriticarInativacaoParticipanteComAdesaoAtiva()
        {
            var participante = Mock.CriarParticipante();
            var periodo = Mock.CriarPeriodo();

            servicoParticipante.IncluirParticipante(participante);
            servicoPeriodo.IncluirPeriodo(periodo);

            servicoParticipante.RealizarAdesaoParticipantePeriodoAtivo(participante, 100);

            var result = servicoParticipante.InativarParticipante(participante);
            var participantePesquisado = servicoParticipante.ObterParticipantePorEmail(participante.Email);

            LimparDados = true;

            Assert.AreEqual(StatusCodeEnum.Error, result.StatusCode);
            Assert.IsTrue(participantePesquisado.Ativo);
        }
        #endregion

        #region Persistência
        [TestMethod]
        [TestCategory("Persistência")]
        public void DeveIncluirParticipante()
        {
            var participante = Mock.CriarParticipante();
            var result = servicoParticipante.IncluirParticipante(participante);

            var participanteIncluido = servicoParticipante.ObterParticipantePorEmail(participante.Email);

            LimparDados = true;

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
            Assert.IsNotNull(participanteIncluido);
        }

        [TestMethod]
        [TestCategory("Persistência")]
        public void DeveAlterarParticipante()
        {
            var participante = Mock.CriarParticipante();
            servicoParticipante.IncluirParticipante(participante);

            var novoNome = "Daniel Tostes";
            var novoTelefone = "(21)99778-5791";

            var participanteIncluido = servicoParticipante.ObterParticipantePorEmail(participante.Email);
            participanteIncluido.Nome = novoNome;
            participanteIncluido.Telefone = novoTelefone;

            var result = servicoParticipante.AlterarParticipante(participanteIncluido);
            var participanteAlterado = servicoParticipante.ObterParticipantePorEmail(participante.Email);

            LimparDados = true;

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
            Assert.IsNotNull(participanteAlterado);
            Assert.AreEqual(novoNome, participanteAlterado.Nome);
            Assert.AreEqual(novoTelefone, participanteAlterado.Telefone);
        }

        [TestMethod]
        [TestCategory("Persistência")]
        public void DeveInativarParticipante()
        {
            var participante = Mock.CriarParticipante();
            servicoParticipante.IncluirParticipante(participante);

            var result = servicoParticipante.InativarParticipante(participante);

            var participanteAlterado = servicoParticipante.ObterParticipantePorEmail(participante.Email);

            LimparDados = true;

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
            Assert.IsFalse(participanteAlterado.Ativo);
        }
        #endregion
    }
}
