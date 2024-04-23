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
    public partial class Mensalidades : ContentPage
    {
        #region Atributos
        private MensalidadesViewModel ViewModel;
        private bool dadosCarregados = false;
        #endregion

        #region Construtor
        public Mensalidades()
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
                ViewModel = new MensalidadesViewModel();
                this.BindingContext = ViewModel;
                dadosCarregados = true;
            }
        }
        #endregion
    }
}