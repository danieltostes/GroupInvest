using GroupInvest.App.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticipanteApp.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        #region Propriedades
        private Dashboard dashboard;
        public Dashboard Dashboard
        {
            get { return dashboard; }
            set
            {
                dashboard = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Construtor
        public DashboardViewModel()
        {
            CarregarDados();
        }
        #endregion

        #region Métodos Privados
        private async void CarregarDados()
        {
            IsLoading = true;

            var dashboard = await App.AppService.ObterDashboardParticipante(App.ParticipanteId.GetValueOrDefault());
            Dashboard = dashboard;

            if (dashboard != null && PreencherGrafico != null)
                PreencherGrafico(this, new EventArgs());

            IsLoading = false;
        }
        #endregion

        #region Eventos
        public event EventHandler PreencherGrafico;
        #endregion
    }
}
