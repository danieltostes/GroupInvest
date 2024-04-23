using GroupInvest.Common.Infrastructure.Tests.Configuracao;
using GroupInvest.Microservices.Participantes.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.Participantes.Infra.DataAccess.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GroupInvest.Common.Infrastructure.Tests.Testes
{
    public class TesteBase
    {
        protected IRepositorioPeriodo repositorioPeriodo;
        protected IRepositorioParticipante repositorioParticipante;
        protected IRepositorioAdesao repositorioAdesao;

        public TestContext TestContext { get; set; }

        public TesteBase()
        {
            var dbContext = InMemoryBusTest.Instance().GetDbContext(typeof(IRepositorioAdesao).FullName);

            repositorioPeriodo = new RepositorioPeriodo(dbContext);
            repositorioParticipante = new RepositorioParticipante(dbContext);
            repositorioAdesao = new RepositorioAdesao(dbContext);
        }

        protected virtual void ExcluirRegistros()
        {
            // remove as adesões
            var adesoes = repositorioAdesao.ListarTodos();
            adesoes.ToList().ForEach(a => repositorioAdesao.Excluir(a));
            repositorioAdesao.Commit();

            // remove os participantes da base
            var participantes = repositorioParticipante.ListarTodos();
            participantes.ToList().ForEach(p => repositorioParticipante.Excluir(p));
            repositorioParticipante.Commit();

            // remove os periodos da base
            var periodos = repositorioPeriodo.ListarTodos();
            periodos.ToList().ForEach(p => repositorioPeriodo.Excluir(p));
            repositorioPeriodo.Commit();
        }
    }
}
