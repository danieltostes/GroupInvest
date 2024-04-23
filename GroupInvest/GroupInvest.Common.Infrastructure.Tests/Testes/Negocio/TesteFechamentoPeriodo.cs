using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Domain.Classes;
using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Servicos;
using GroupInvest.Microservices.Mensalidades.Domain.Servicos;
using GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Contextos;
using GroupInvest.Microservices.Mensalidades.Infra.DataAccess.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupInvest.Common.Infrastructure.Tests.Testes.Negocio
{
    [TestClass]
    public class TesteFechamentoPeriodo
    {
        #region Repositórios e Serviços
        private IServicoPeriodo servicoPeriodo;
        private IServicoParticipante servicoParticipante;
        private IServicoAdesao servicoAdesao;
        private IServicoMensalidade servicoMensalidade;
        private IServicoEmprestimo servicoEmprestimo;
        private IServicoPagamento servicoPagamento;

        private IRepositorioPeriodo repositorioPeriodo;
        private IRepositorioParticipante repositorioParticipante;
        private IRepositorioAdesao repositorioAdesao;
        private IRepositorioMensalidade repositorioMensalidade;
        private IRepositorioEmprestimo repositorioEmprestimo;
        private IRepositorioPrevisaoPagamentoEmprestimo repositorioPrevisaoPagamentoEmprestimo;
        private IRepositorioPagamento repositorioPagamento;
        private IRepositorioPagamentoParcialEmprestimo repositorioPagamentoParcialEmprestimo;
        private IRepositorioItemPagamento repositorioItemPagamento;
        private IRepositorioDistribuicaoCotas repositorioDistribuicaoCotas;
        private IRepositorioDistribuicaoParticipante repositorioDistribuicaoParticipante;
        #endregion

        #region Construtor
        public TesteFechamentoPeriodo()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets(this.GetType().Assembly);
            var configuracao = builder.Build();

            var dbContext = new MicroserviceMensalidadesDbContext(configuracao["MensalidadesDbConnectionString"]);

            repositorioAdesao = new RepositorioAdesao(dbContext);
            repositorioEmprestimo = new RepositorioEmprestimo(dbContext);
            repositorioItemPagamento = new RepositorioItemPagamento(dbContext);
            repositorioMensalidade = new RepositorioMensalidade(dbContext);
            repositorioPagamento = new RepositorioPagamento(dbContext);
            repositorioPagamentoParcialEmprestimo = new RepositorioPagamentoParcialEmprestimo(dbContext);
            repositorioParticipante = new RepositorioParticipante(dbContext);
            repositorioPeriodo = new RepositorioPeriodo(dbContext);
            repositorioPrevisaoPagamentoEmprestimo = new RepositorioPrevisaoPagamentoEmprestimo(dbContext);
            repositorioDistribuicaoCotas = new RepositorioDistribuicaoCotas(dbContext);
            repositorioDistribuicaoParticipante = new RepositorioDistribuicaoParticipante(dbContext);

            servicoAdesao = new ServicoAdesao(repositorioAdesao, repositorioPeriodo);
            servicoEmprestimo = new ServicoEmprestimo(repositorioPeriodo, repositorioAdesao, repositorioEmprestimo, repositorioPrevisaoPagamentoEmprestimo);
            servicoMensalidade = new ServicoMensalidade(repositorioMensalidade, repositorioPeriodo);
            servicoParticipante = new ServicoParticipante(repositorioParticipante);

            servicoPagamento = new ServicoPagamento(repositorioPagamento, repositorioMensalidade, repositorioEmprestimo,
                repositorioPagamentoParcialEmprestimo, repositorioPrevisaoPagamentoEmprestimo, repositorioPeriodo, repositorioItemPagamento);

            servicoPeriodo = new ServicoPeriodo(repositorioPeriodo, repositorioMensalidade, repositorioEmprestimo, repositorioAdesao,
                repositorioDistribuicaoCotas, repositorioPrevisaoPagamentoEmprestimo, servicoEmprestimo);

            
        }
        #endregion

        [TestMethod]
        [TestCategory("FechamentoPeriodo")]
        public void SimularPeriodoParaEncerramento()
        {
            #region Criação do período
            var periodo = new Periodo
            {
                Id = 1,
                AnoReferencia = 2020,
                DataInicioPeriodo = new DateTime(2020, 1, 1),
                DataTerminoPeriodo = new DateTime(2020, 12, 31),
                Ativo = true
            };
            servicoPeriodo.IncluirPeriodo(periodo);
            #endregion

            #region Criação dos participantes
            var participantes = new List<Participante>();

            participantes.Add(new Participante { Id = 1, Nome = "Daniel Tostes" });
            participantes.Add(new Participante { Id = 2, Nome = "Michelle Lopes" });
            participantes.Add(new Participante { Id = 3, Nome = "Sheila Malheiros" });
            participantes.Add(new Participante { Id = 4, Nome = "Jorge Santana" });

            participantes.ForEach(p => repositorioParticipante.Incluir(p));
            #endregion

            #region Adesões
            var adesoes = new List<Adesao>();

            adesoes.Add(new Adesao { Id = 1, Participante = participantes[0], Periodo = periodo, NumeroCotas = 80 });
            adesoes.Add(new Adesao { Id = 2, Participante = participantes[1], Periodo = periodo, NumeroCotas = 150 });
            adesoes.Add(new Adesao { Id = 3, Participante = participantes[2], Periodo = periodo, NumeroCotas = 50 });
            adesoes.Add(new Adesao { Id = 4, Participante = participantes[3], Periodo = periodo, NumeroCotas = 120 });

            adesoes.ForEach(a => repositorioAdesao.Incluir(a));
            #endregion

            #region Mensalidades
            adesoes.ForEach(a => servicoMensalidade.GerarMensalidades(a));
            #endregion

            #region Pagamento das Mensalidades
            // participante 1 atrasou algumas mensalidades
            var mesesAtrasoPagamento = new int[] { 1, 4, 6, 8, 10 };
            var mensalidades = servicoMensalidade.ListarMensalidadesAdesao(adesoes[0]);
            foreach (var mensalidade in mensalidades)
            {
                var dataVencimento = mensalidade.DataVencimento.GetValueOrDefault();
                var mesAtraso = mesesAtrasoPagamento.Contains(dataVencimento.Month);
                var dataPagamento = mesAtraso ? new DateTime(dataVencimento.Year, dataVencimento.Month, 20) : dataVencimento;

                var fatura = new Fatura(dataPagamento);
                fatura.AdicionarMensalidade(mensalidade);

                servicoPagamento.RealizarPagamento(fatura);
            }

            // participante 2 nunca atrasou
            mensalidades = servicoMensalidade.ListarMensalidadesAdesao(adesoes[1]);
            foreach (var mensalidade in mensalidades)
            {
                var fatura = new Fatura(mensalidade.DataVencimento.GetValueOrDefault());
                fatura.AdicionarMensalidade(mensalidade);

                servicoPagamento.RealizarPagamento(fatura);
            }

            // participante 3 atrasou algumas mensalidades
            mesesAtrasoPagamento = new int[] { 5, 6, 12 };
            mensalidades = servicoMensalidade.ListarMensalidadesAdesao(adesoes[2]);
            foreach (var mensalidade in mensalidades)
            {
                var dataVencimento = mensalidade.DataVencimento.GetValueOrDefault();
                var mesAtraso = mesesAtrasoPagamento.Contains(dataVencimento.Month);
                var dataPagamento = mesAtraso ? new DateTime(dataVencimento.Year, dataVencimento.Month, 20) : dataVencimento;

                var fatura = new Fatura(dataPagamento);
                fatura.AdicionarMensalidade(mensalidade);

                servicoPagamento.RealizarPagamento(fatura);
            }

            // participante 4 nunca atrasou
            mensalidades = servicoMensalidade.ListarMensalidadesAdesao(adesoes[3]);
            foreach (var mensalidade in mensalidades)
            {
                var fatura = new Fatura(mensalidade.DataVencimento.GetValueOrDefault());
                fatura.AdicionarMensalidade(mensalidade);

                servicoPagamento.RealizarPagamento(fatura);
            }
            #endregion

            #region Empréstimos
            // participante 1 pegou 2 empréstimos
            var emprestimo1 = servicoEmprestimo.ConcederEmprestimo(participantes[0], new DateTime(2020, 2, 10), 1200).ObjectResult as Emprestimo;
            var emprestimo2 = servicoEmprestimo.ConcederEmprestimo(participantes[0], new DateTime(2020, 6, 18), 2000).ObjectResult as Emprestimo;

            // participante 3 pegou 1 empréstimo
            var emprestimo3 = servicoEmprestimo.ConcederEmprestimo(participantes[2], new DateTime(2020, 5, 3), 600).ObjectResult as Emprestimo;

            // pagamento do empréstimo 1
            var dataPagamentoEmprestimo = new DateTime(2020, 7, 10);
            servicoEmprestimo.AtualizarPrevisaoPagamentoEmprestimo(emprestimo1, dataPagamentoEmprestimo);
            var previsoesPagamento = servicoEmprestimo.ListarPrevisoesPagamentoEmprestimo(emprestimo1, true);
            var valorQuitacaoEmprestimo = previsoesPagamento.Sum(prev => prev.ValorDevido);

            var faturaEmprestimo = new Fatura(dataPagamentoEmprestimo);
            previsoesPagamento.ToList().ForEach(prev => faturaEmprestimo.AdicionarPrevisaoPagamentoEmprestimo(prev, prev.ValorDevido));
            servicoPagamento.RealizarPagamento(faturaEmprestimo);

            // pagamento do empréstimo 2
            dataPagamentoEmprestimo = new DateTime(2020, 7, 20);
            servicoEmprestimo.AtualizarPrevisaoPagamentoEmprestimo(emprestimo2, dataPagamentoEmprestimo);
            previsoesPagamento = servicoEmprestimo.ListarPrevisoesPagamentoEmprestimo(emprestimo2, true);
            valorQuitacaoEmprestimo = previsoesPagamento.Sum(prev => prev.ValorDevido);

            faturaEmprestimo = new Fatura(dataPagamentoEmprestimo);
            previsoesPagamento.ToList().ForEach(prev => faturaEmprestimo.AdicionarPrevisaoPagamentoEmprestimo(prev, prev.ValorDevido));
            servicoPagamento.RealizarPagamento(faturaEmprestimo);

            // pagamento do empréstimo 3
            dataPagamentoEmprestimo = new DateTime(2020, 8, 3);
            servicoEmprestimo.AtualizarPrevisaoPagamentoEmprestimo(emprestimo3, dataPagamentoEmprestimo);
            previsoesPagamento = servicoEmprestimo.ListarPrevisoesPagamentoEmprestimo(emprestimo3, true);
            valorQuitacaoEmprestimo = previsoesPagamento.Sum(prev => prev.ValorDevido);

            faturaEmprestimo = new Fatura(dataPagamentoEmprestimo);
            previsoesPagamento.ToList().ForEach(prev => faturaEmprestimo.AdicionarPrevisaoPagamentoEmprestimo(prev, prev.ValorDevido));
            servicoPagamento.RealizarPagamento(faturaEmprestimo);
            #endregion
        }

        [TestMethod]
        public void ObterPrevisaoRecebimentoMensalidadesPeriodo()
        {
            var dataReferencia = new DateTime(2020, 9, 15);
            var numeroMeses = 9;
            var arrecadacaoEsperada = 4000M * numeroMeses;

            var arrecadacaoPrevista = servicoMensalidade.ObterPrevisaoRecebimentoMensalidades(dataReferencia);
            Assert.AreEqual(arrecadacaoEsperada, arrecadacaoPrevista);
        }

        [TestMethod]
        public void ObterTotalRecebidoMensalidades()
        {
            var dataReferencia = new DateTime(2020, 9, 15);
            var valorTotalRecebido = servicoPagamento.ObterValorTotalRecebidoMensalidades(dataReferencia);
            var valorEsperado = 36420M;

            Assert.AreEqual(valorEsperado, valorTotalRecebido);
        }

        [TestMethod]
        public void ObterTotalRecebidoEmprestimos()
        {
            var dataReferencia = new DateTime(2020, 9, 20);
            var valorTotalRecebido = servicoPagamento.ObterValorTotalRecebidoEmprestimos(dataReferencia);
            var valorEsperado = 5170;

            Assert.AreEqual(valorEsperado, valorTotalRecebido);
        }

        [TestMethod]
        public void ObterTotalDevidoEmprestimosPeriodo()
        {
            var dataReferencia = new DateTime(2020, 12, 31);
            var result = servicoEmprestimo.ObterValorTotalReceberEmprestimos(dataReferencia);

            var valorEsperado = 5170;

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
            Assert.AreEqual(valorEsperado, (decimal)result.ObjectResult);
        }

        [TestMethod]
        public void EncerrarPeriodo()
        {
            var periodo = servicoPeriodo.ObterPeriodoAtivo();
            var result = servicoPeriodo.EncerrarPeriodo(periodo);

            Assert.AreEqual(StatusCodeEnum.OK, result.StatusCode);
        }
    }
}
