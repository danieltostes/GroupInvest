using GroupInvest.App.Domain.Entidades;
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
    public partial class DetalhamentoEmprestimo : ContentPage
    {
        #region Propriedades
        private Emprestimo emprestimo;
        #endregion

        #region Construtor
        public DetalhamentoEmprestimo(Emprestimo emprestimo)
        {
            InitializeComponent();
            this.emprestimo = emprestimo;
            this.BindingContext = this.emprestimo;
            this.Title = "Detalhamento de Empréstimo";
        }
        #endregion
    }
}