using GroupInvest.App.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ParticipanteApp.ViewModel
{
    public class EmprestimoViewModel : ViewModelBase
    {
        #region Propriedades
        private ObservableCollection<Emprestimo> emprestimos;
        public ObservableCollection<Emprestimo> Emprestimos
        {
            get { return emprestimos; }
            set
            {
                emprestimos = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Construtor
        public EmprestimoViewModel()
        {
            CarregarDados();
        }
        #endregion

        #region Métodos Privados
        private async void CarregarDados()
        {
            IsLoading = true;

            var listaEmprestimos = await App.AppService.ListarEmprestimosParticipante(App.ParticipanteId.GetValueOrDefault());
            var lista = new ObservableCollection<Emprestimo>();
            foreach (var emprestimo in listaEmprestimos)
                lista.Add(emprestimo);
            Emprestimos = lista;

            IsLoading = false;
        }
        #endregion
    }
}
