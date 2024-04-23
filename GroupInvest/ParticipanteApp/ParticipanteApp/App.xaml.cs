using GroupInvest.App.Application;
using GroupInvest.App.Domain.Servicos;
using GroupInvest.App.Infra.DataAccess.Repositorios;
using ParticipanteApp.Views;
using Xamarin.Forms;

namespace ParticipanteApp
{
    public partial class App : Application
    {
        #region Atributos
        public static int? ParticipanteId;
        #endregion

        #region Injeção de dependência
        public static IAppParticipanteService AppService;
        #endregion

        public App()
        {
            InitializeComponent();

            var urlBase = "https://groupinvest-app-api.azurewebsites.net/api/v1.0";
            var repositorioMensalidade = new RepositorioMensalidade(urlBase);
            var repositorioEmprestimo = new RepositorioEmprestimo(urlBase);
            var repositorioDashboard = new RepositorioDashboard(urlBase);

            var servicoMensalidade = new ServicoMensalidade(repositorioMensalidade);
            var servicoEmprestimo = new ServicoEmprestimo(repositorioEmprestimo);
            var servicoDashboard = new ServicoDashboard(repositorioDashboard);

            AppService = new AppParticipanteService(servicoMensalidade, servicoEmprestimo, servicoDashboard);

            MainPage = new NavigationPage(new Login());
            //MainPage = new PageTeste();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
