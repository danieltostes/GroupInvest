using GroupInvest.App.Application.Models.Dtos;
using GroupInvest.App.Domain.Entidades;
using GroupInvest.App.Domain.Interfaces.Servicos;
using System.Collections.Generic;
using System.Net.Http;
using IdentityModel.Client;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GroupInvest.App.Application
{
    public class AppParticipanteService : IAppParticipanteService
    {
        private string token;

        #region Configurações
        private readonly string ClientId = "AppParticipantes";
        private readonly string ClientSecret = "123456";
        private readonly string UrlAuthorization = "https://groupinvest-identity.azurewebsites.net/connect/token";
        private readonly string UrlGetParticipante = "https://groupinvest-participantes-api.azurewebsites.net/api/v1.0/participantes/usuario";
        #endregion

        #region Injeção de dependência
        private readonly IServicoMensalidade servicoMensalidade;
        private readonly IServicoEmprestimo servicoEmprestimo;
        private readonly IServicoDashboard servicoDashboard;
        #endregion

        #region Construtor
        public AppParticipanteService(IServicoMensalidade servicoMensalidade, IServicoEmprestimo servicoEmprestimo, IServicoDashboard servicoDashboard)
        {
            this.servicoMensalidade = servicoMensalidade;
            this.servicoEmprestimo = servicoEmprestimo;
            this.servicoDashboard = servicoDashboard;
        }
        #endregion

        #region IAppParticipanteService
        public async Task<AutenticacaoDto> Autenticar(string email, string senha)
        {
            var client = new HttpClient();
            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = UrlAuthorization,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                UserName = email,
                Password = senha
            });

            if (!string.IsNullOrEmpty(response.AccessToken))
            {
                var autenticacao = new AutenticacaoDto { Autenticado = true };

                // verifica se usuário pode acessar o aplicativo através da role "AppUser"
                // para que as roles venham no token é necessário cadastrar no ApiScope > User Claims o valor "role" para o escopo que será usado pelo client do Auth
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenStream = tokenHandler.ReadToken(response.AccessToken) as JwtSecurityToken;

                var roleClaims = tokenStream.Claims.Where(c => c.Type == "role").Select(c => c.Value);
                if (roleClaims.Contains("AppUser"))
                {
                    token = response.AccessToken;

                    var userId = tokenStream.Claims.FirstOrDefault(c => c.Type == "sub").Value;

                    // acessa o microsserviço de participantes para recuperar o identificador do usuário
                    var clientToken = await GetClientToken();

                    //autenticacao.Autorizado = false;
                    //return autenticacao;

                    if (!string.IsNullOrEmpty(clientToken))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", $"bearer {clientToken}");
                        var clientResponse = await client.GetAsync($"{UrlGetParticipante}/{userId}");

                        if (clientResponse.IsSuccessStatusCode && clientResponse.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var content = clientResponse.Content.ReadAsStringAsync().Result;
                            var participanteDto = JsonConvert.DeserializeObject<ParticipanteDto>(content);

                            autenticacao.Autorizado = true;
                            autenticacao.ParticipanteId = participanteDto.Id;
                            autenticacao.Informacao = participanteDto.Nome;
                        }
                        else
                            autenticacao.MensagemErro = clientResponse.ReasonPhrase;
                    }
                }
                else
                    autenticacao.Autorizado = false;

                return autenticacao;
            }
            
            return new AutenticacaoDto { Autenticado = false };
        }

        public async Task<IReadOnlyCollection<Mensalidade>> ListarMensalidadesParticipante(int participanteId)
        {
            return await Task.Run(() =>
            {
                servicoMensalidade.Token = token;
                var mensalidades = servicoMensalidade.ListarMensalidadesParticipante(participanteId);
                return mensalidades.OrderBy(mens => mens.DataVencimento).ToList();
            });
        }

        public async Task<IReadOnlyCollection<Emprestimo>> ListarEmprestimosParticipante(int participanteId)
        {
            return await Task.Run(() =>
            {
                servicoEmprestimo.Token = token;
                var emprestimos = servicoEmprestimo.ListarEmprestimosParticipante(participanteId);
                return emprestimos.OrderByDescending(emp => emp.Data).ToList();
            });
        }

        public async Task<Dashboard> ObterDashboardParticipante(int participanteId)
        {
            return await Task.Run(() =>
            {
                servicoDashboard.Token = token;
                return servicoDashboard.ObterDashboardParticipante(participanteId);
            });
        }
#endregion

#region Métodos Privados
        private async Task<string> GetClientToken()
        {
            // Na configuração do client no IAm é necessário cadastrar a claim Role na aba Token com o valor Admin pois a API do IAm precisa dessa role para executar os recursos
            var client = new HttpClient();
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = UrlAuthorization,
                ClientId = ClientId,
                ClientSecret = ClientSecret
            });

            return response.AccessToken;
        }
#endregion
    }
}
