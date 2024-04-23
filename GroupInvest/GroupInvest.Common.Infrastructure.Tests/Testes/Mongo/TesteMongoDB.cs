using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.AppUsuarios.Domain.Servicos;
using GroupInvest.Microservices.AppUsuarios.Infra.DataAccess.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupInvest.Common.Infrastructure.Tests.Testes.Mongo
{
    [TestClass]
    public class TesteMongoDB
    {
        #region Atributos
        private MongoClient client;
        private IMongoDatabase db;
        private IRepositorioMensalidade repositorioMensalidade;
        private IRepositorioEmprestimo repositorioEmprestimo;
        private IServicoMensalidade servicoMensalidade;
        private IServicoEmprestimo servicoEmprestimo;
        #endregion

        #region Construtor
        public TesteMongoDB()
        {
            client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("Groupinvest");

            repositorioMensalidade = new RepositorioMensalidade(db);
            repositorioEmprestimo = new RepositorioEmprestimo(db);

            servicoMensalidade = new ServicoMensalidade(repositorioMensalidade);
            servicoEmprestimo = new ServicoEmprestimo(repositorioEmprestimo);
        }
        #endregion

        [TestMethod]
        public void GravarMensalidade()
        {
            var mensalidade = new Mensalidade
            {
                MensalidadeId = 3,
                ParticipanteId = 11,
                NomeParticipante = "Leonardo Tostes",
                DataReferencia = new DateTime(2020, 1, 1),
                DataVencimento = new DateTime(2020, 1, 15),
                ValorBase = 500,
                ValorDevido = 500
            };

            var result = servicoMensalidade.IncluirMensalidade(mensalidade);

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }

        [TestMethod]
        public void ObterMensalidades()
        {
            var mensalidades = servicoMensalidade.ListarMensalidadesParticipante(10);
            Assert.AreEqual(2, mensalidades.Count);
        }

        [TestMethod]
        public void AlterarMensalidade()
        {
            var mensalidades = servicoMensalidade.ListarMensalidadesParticipante(10);
            var mensalidade = mensalidades.ElementAt(0);

            mensalidade.ValorBase = 100;
            mensalidade.ValorDevido = 100;

            var result = servicoMensalidade.AlterarMensalidade(mensalidade);
            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }

        [TestMethod]
        public void IncluirPagamentoMensalidade()
        {
            var mensalidades = servicoMensalidade.ListarMensalidadesParticipante(10);
            var mensalidade = mensalidades.ElementAt(0);

            var pagamento = new PagamentoMensalidade
            {
                DataPagamento = DateTime.Today,
                PercentualJuros = 15,
                ValorJuros = mensalidade.ValorBase * 0.15M,
                ValorPago = mensalidade.ValorBase * 1.15M
            };

            var result = servicoMensalidade.IncluirPagamento(mensalidade, pagamento);
            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }

        [TestMethod]
        public void GravarEmprestimo()
        {
            var emprestimo = new Emprestimo
            {
                EmprestimoId = 1,
                ParticipanteId = 10,
                Data = new DateTime(2020, 5, 15),
                Valor = 1000
            };

            var result = servicoEmprestimo.IncluirEmprestimo(emprestimo);

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }

        [TestMethod]
        public void AdicionarPagamentoEmprestimo()
        {
            var emprestimos = servicoEmprestimo.ListarEmprestimosParticipante(10);
            var pagamento = new PagamentoEmprestimo
            {
                DataPagamento = new DateTime(2020, 6, 12),
                ValorPago = 660,
                PercentualJuros = 10,
                ValorJuros = 60,
                SaldoAnterior = 600,
                ValorDevido = 660,
                SaldoAtualizado = 0
            };

            var result = servicoEmprestimo.AdicionarPagamento(emprestimos.ElementAt(0), pagamento);

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }
    }
}
