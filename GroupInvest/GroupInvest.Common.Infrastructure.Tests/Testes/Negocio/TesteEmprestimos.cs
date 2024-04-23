using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Mensalidades.Domain.Servicos;
using GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Contextos;
using GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Classes;

namespace GroupInvest.Common.Infrastructure.Tests.Testes.Negocio
{
    [TestClass]
    public class TesteEmprestimos
    {
        #region Injeção de dependência
        public IServicoMensalidade servicoMensalidade;
        public IServicoEmprestimo servicoEmprestimo;
        public IServicoAdesao servicoAdesao;
        public IServicoPeriodo servicoPeriodo;
        public IServicoPagamento servicoPagamento;

        public IRepositorioMensalidade repositorioMensalidade;
        public IRepositorioAdesao repositorioAdesao;
        public IRepositorioPeriodo repositorioPeriodo;
        public IRepositorioEmprestimo repositorioEmprestimo;
        public IRepositorioPrevisaoPagamentoEmprestimo repositorioPrevisaoPagamento;
        public IRepositorioPagamentoParcialEmprestimo repositorioPagamentoParcial;
        public IRepositorioPagamento repositorioPagamento;
        public IRepositorioItemPagamento repositorioItemPagamento;
        public IRepositorioDistribuicaoCotas repositorioDistribuicaoCotas;
        public IRepositorioDistribuicaoParticipante repositorioDistribuicaoParticipante;
        #endregion

        #region Construtor
        public TesteEmprestimos()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets(this.GetType().Assembly);
            var configuracao = builder.Build();

            var dbContext = new MicroserviceMensalidadesDbContext(configuracao["MensalidadesDbConnectionString"]);

            repositorioMensalidade = new RepositorioMensalidade(dbContext);
            repositorioAdesao = new RepositorioAdesao(dbContext);
            repositorioPeriodo = new RepositorioPeriodo(dbContext);
            repositorioEmprestimo = new RepositorioEmprestimo(dbContext);
            repositorioPrevisaoPagamento = new RepositorioPrevisaoPagamentoEmprestimo(dbContext);
            repositorioPagamentoParcial = new RepositorioPagamentoParcialEmprestimo(dbContext);
            repositorioPagamento = new RepositorioPagamento(dbContext);
            repositorioItemPagamento = new RepositorioItemPagamento(dbContext);
            repositorioDistribuicaoCotas = new RepositorioDistribuicaoCotas(dbContext);
            repositorioDistribuicaoParticipante = new RepositorioDistribuicaoParticipante(dbContext);

            servicoMensalidade = new ServicoMensalidade(repositorioMensalidade, repositorioPeriodo);
            servicoAdesao = new ServicoAdesao(repositorioAdesao, repositorioPeriodo);
            servicoEmprestimo = new ServicoEmprestimo(repositorioPeriodo, repositorioAdesao, repositorioEmprestimo, repositorioPrevisaoPagamento);
            servicoPagamento = new ServicoPagamento(repositorioPagamento, repositorioMensalidade, repositorioEmprestimo,
                repositorioPagamentoParcial, repositorioPrevisaoPagamento, repositorioPeriodo, repositorioItemPagamento);
            servicoPeriodo = new ServicoPeriodo(repositorioPeriodo, repositorioMensalidade, repositorioEmprestimo, repositorioAdesao,
                repositorioDistribuicaoCotas, repositorioPrevisaoPagamento, servicoEmprestimo);
        }
        #endregion

        #region Testes
        [TestMethod]
        public void AtualizarPrevisaoPagamentoEmprestimo()
        {
            var emprestimos = servicoEmprestimo.ListarTodosEmprestimos();
            var emprestimo = emprestimos.First(emp => emp.Id.Equals(1));

            //var dataReferencia = DateTime.Today;
            var dataReferencia = new DateTime(2020, 6, 15);

            var result = servicoEmprestimo.AtualizarPrevisaoPagamentoEmprestimo(emprestimo, dataReferencia);

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }

        [TestMethod]
        public void RealizarPagamento()
        {
            var adesao = servicoAdesao.ObterAdesaoPorId(9);
            //var mensalidades = servicoMensalidade.ListarMensalidadesAdesao(adesao);
            var emprestimos = servicoEmprestimo.ListarEmprestimosParticipante(adesao.Participante);

            var fatura = new Fatura(DateTime.Today);

            //fatura.AdicionarMensalidade(mensalidades.ElementAt(0));
            //fatura.AdicionarMensalidade(mensalidades.ElementAt(1));
            //fatura.AdicionarMensalidade(mensalidades.ElementAt(2));

            var emprestimo = emprestimos.ElementAt(0);
            servicoEmprestimo.AtualizarPrevisaoPagamentoEmprestimo(emprestimo, DateTime.Today);

            var previsoesPagamento = servicoEmprestimo.ListarPrevisoesPagamentoEmprestimo(emprestimo, true);
            fatura.AdicionarPrevisaoPagamentoEmprestimo(previsoesPagamento.ElementAt(0), previsoesPagamento.ElementAt(0).ValorDevido);
            fatura.AdicionarPrevisaoPagamentoEmprestimo(previsoesPagamento.ElementAt(1), previsoesPagamento.ElementAt(0).ValorDevido);
            fatura.AdicionarPrevisaoPagamentoEmprestimo(previsoesPagamento.ElementAt(2), previsoesPagamento.ElementAt(0).ValorDevido);
            fatura.AdicionarPrevisaoPagamentoEmprestimo(previsoesPagamento.ElementAt(3), 920);

            var result = servicoPagamento.RealizarPagamento(fatura);

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }
        #endregion
    }
}
