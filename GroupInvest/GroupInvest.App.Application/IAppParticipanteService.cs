using GroupInvest.App.Application.Models.Dtos;
using GroupInvest.App.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroupInvest.App.Application
{
    /// <summary>
    /// Interface para os serviços do aplicativo de participantes
    /// </summary>
    public interface IAppParticipanteService
    {
        Task<AutenticacaoDto> Autenticar(string email, string senha);
        Task<IReadOnlyCollection<Mensalidade>> ListarMensalidadesParticipante(int participanteId);
        Task<IReadOnlyCollection<Emprestimo>> ListarEmprestimosParticipante(int participanteId);
        Task<Dashboard> ObterDashboardParticipante(int participanteId);
    }
}
