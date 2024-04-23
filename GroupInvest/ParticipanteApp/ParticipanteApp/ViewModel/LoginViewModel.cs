using GroupInvest.App.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParticipanteApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Métodos
        public async Task<AutenticacaoDto> AutenticarUsuario(string email, string senha)
        {
            IsLoading = true;
            var dto = await App.AppService.Autenticar(email, senha);
            IsLoading = false;
            return dto;
        }
        #endregion
    }
}
