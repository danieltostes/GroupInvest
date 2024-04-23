using GroupInvest.App.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ParticipanteApp.ViewModel
{
    public class MensalidadesViewModel : ViewModelBase
    {
        #region Propriedades
        private ObservableCollection<Mensalidade> mensalidades;
        public ObservableCollection<Mensalidade> Mensalidades
        {
            get { return mensalidades; }
            set
            {
                mensalidades = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Construtor
        public MensalidadesViewModel()
        {
            CarregarDados();
        }
        #endregion

        #region Métodos Privados
        private async void CarregarDados()
        {
            IsLoading = true;

            var listaMensalidades = await App.AppService.ListarMensalidadesParticipante(App.ParticipanteId.GetValueOrDefault());            
            var lista = new ObservableCollection<Mensalidade>();
            foreach (var mensalidade in listaMensalidades)
                lista.Add(mensalidade);
            Mensalidades = lista;

            IsLoading = false;
        }
        #endregion
    }
}
