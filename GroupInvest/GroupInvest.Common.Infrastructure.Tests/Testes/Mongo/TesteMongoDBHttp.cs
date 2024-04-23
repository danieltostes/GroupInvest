using GroupInvest.App.Domain.Interfaces.Repositorios;
using GroupInvest.App.Infra.DataAccess.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Common.Infrastructure.Tests.Testes.Mongo
{
    [TestClass]
    public class TesteMongoDBHttp
    {
        private string token;
        private IRepositorioMensalidade repositorioMensalidade;

        #region Construtor
        public TesteMongoDBHttp()
        {
            string urlBase = "";
            token = "";

            repositorioMensalidade = new RepositorioMensalidade(urlBase);
        }
        #endregion

        #region Testes
        [TestMethod]
        public void ListarMensalidadesHttp()
        {
            repositorioMensalidade.Token = token;
            var mensalidades = repositorioMensalidade.ListarMensalidadesParticipante(2);

            Assert.AreEqual(12, mensalidades.Count);
        }
        #endregion
    }
}
