using ParticipanteApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParticipanteApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private LoginViewModel ViewModel;

        #region Construtor
        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            ViewModel = new LoginViewModel();
            this.BindingContext = ViewModel;
        }
        #endregion

        #region Eventos
        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            var email = entryEmail.Text;
            var senha = entrySenha.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                await DisplayAlert("Login", "E-mail e senha devem ser preenchidos", "OK");
                return;
            }

            var autenticacao = await ViewModel.AutenticarUsuario(email, senha);


            if (!autenticacao.Autenticado)
            {
                await DisplayAlert("Login", "Usuário não encontrado", "OK");
                return;
            }

            if (!autenticacao.Autorizado)
            {
                await DisplayAlert("Login", "Usuário não possui acesso ao aplicativo. Entre em contato com o administrador.", "OK");
                return;
            }

            if (autenticacao.Autenticado && autenticacao.Autorizado)
            {
                ButtonLogin.IsEnabled = false;
                App.ParticipanteId = autenticacao.ParticipanteId;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync().ConfigureAwait(false);
            }
            else
                ButtonLogin.IsEnabled = true;
        }
        #endregion
    }
}