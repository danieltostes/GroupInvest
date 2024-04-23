using GroupInvest.App.Domain.Entidades;
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
    public partial class Emprestimos : ContentPage
    {
        #region Atributos
        private EmprestimoViewModel ViewModel;
        private bool dadosCarregados = false;
        #endregion

        #region Construtor
        public Emprestimos()
        {
            InitializeComponent();
        }
        #endregion

        #region Overrides
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!dadosCarregados)
            {
                ViewModel = new EmprestimoViewModel();
                this.BindingContext = ViewModel;
                dadosCarregados = true;
            }
        }
        #endregion

        #region Eventos
        private void listviewEmprestimos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var emprestimo = e.Item as Emprestimo;
            DetalhamentoEmprestimo pageDetalhamento = new DetalhamentoEmprestimo(emprestimo);
            Navigation.PushAsync(pageDetalhamento);
        }
        #endregion
    }
}