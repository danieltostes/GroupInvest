using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.API.Helpers;
using GroupInvest.Microservices.Participantes.API.V1.Dtos;
using GroupInvest.Microservices.Participantes.Application.Interfaces;
using GroupInvest.Microservices.Participantes.Application.Models.Dtos;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GroupInvest.Microservices.Participantes.API.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Policy = "Admin")]
    public class ParticipantesController : ControllerBase
    {
        #region Injeção de dependência
        private readonly IParticipanteApi participanteAPI;
        #endregion

        #region Construtor
        public ParticipantesController(IParticipanteApi participanteAPI)
        {
            this.participanteAPI = participanteAPI;
        }
        #endregion

        #region Serviços
        [MapToApiVersion("1.0")]
        [HttpGet("ambiente")]
        public ActionResult ObterDadosAmbiente()
        {
            var dados = new
            {
                UserSecretsHelper.ParticipantesDbConnectionString,
                UserSecretsHelper.ServiceBusConnectionString,
                UserSecretsHelper.APIClientId,
                UserSecretsHelper.APIClientSecret,
                UserSecretsHelper.APIName,
                UserSecretsHelper.SenhaPadraoUsuarioAplicativo,
                UserSecretsHelper.SwaggerUIClientId,
                UserSecretsHelper.UrlAuthority,
                UserSecretsHelper.UrlGetToken,
                UserSecretsHelper.UrlRegisterAppUser
            };

            return Ok(dados);
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public ActionResult IncluirParticipante([FromBody] ParticipanteDto participante)
        {
            var result = participanteAPI.IncluirParticipante(participante);

            if (result.StatusCode == StatusCodeEnum.OK)
            {
                var participanteIncluido = participanteAPI.ObterParticipantePorEmail(participante.Email);
                return Created(nameof(IncluirParticipante), participanteIncluido);
            }
            else return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("RealizarAdesao")]
        public ActionResult RealizarAdesaoParticipante([FromBody] RealizarAdesaoDto dto)
        {
            var result = participanteAPI.RealizarAdesaoParticipantePeriodoAtivo(dto.ParticipanteId, dto.NumeroCotas);

            if (result.StatusCode == StatusCodeEnum.OK) return Ok("Adesão realizada com sucesso.");
            else return NotFound("Participante não encontrado ou nenhum período ativo para realização da adesão");
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        public ActionResult<ParticipanteDto> ObterParticipante(int id)
        {
            var participante = participanteAPI.ObterParticipantePorId(id);

            if (participante != null) return participante;
            else return NotFound("Participante não encontrado");
        }

        [MapToApiVersion("1.0")]
        [HttpGet("usuario/{userId}")]
        public ActionResult<ParticipanteDto> ObterParticipantePorUsuarioAplicativo(string userId)
        {
            var participante = participanteAPI.ObterParticipantePorUsuarioAplicativo(userId);
            if (participante != null) return participante;
            else return NotFound("Participante não encontrado");
        }

        [MapToApiVersion("1.0")]
        [HttpPost("CriarUsuarioAplicativo/{id}")]
        public ActionResult CriarUsuarioAplicativo(int id)
        {
            var participante = participanteAPI.ObterParticipantePorId(id);
            if (participante == null)
                return NotFound("Participante não encontrado");

            var result = CriarUsuarioAppApi(participante);
            if (result.StatusCode == StatusCodeEnum.OK)
                return Ok("Usuário criado com sucesso");
            else
                return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public ActionResult<IReadOnlyCollection<ParticipanteDto>> ListarTodosParticipantes()
        {
            return Ok(participanteAPI.ListarTodosParticipantes());
        }

        [MapToApiVersion("1.0")]
        [HttpGet("Ativos")]
        public ActionResult<IReadOnlyCollection<ParticipanteDto>> ListarParticipantesAtivos()
        {
            return Ok(participanteAPI.ListarParticipantesAtivos());
        }
        #endregion

        #region Métodos Privados
        private string GetToken()
        {
            UserSecretsHelper.Load();

            // Na configuração do client no IAm é necessário cadastrar a claim Role na aba Token com o valor Admin pois a API do IAm precisa dessa role para executar os recursos
            var client = new HttpClient();
            var response = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = UserSecretsHelper.UrlGetToken,
                ClientId = UserSecretsHelper.APIClientId,
                ClientSecret = UserSecretsHelper.APIClientSecret
            }).Result;

            return response.AccessToken;
        }

        private OperationResult CriarUsuarioAppApi(ParticipanteDto participante)
        {
            var token = GetToken();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", string.Format("bearer {0}", token));

            UserSecretsHelper.Load();

            var dto = new RegisterAppUserDto
            {
                User = new RegisterAppUserDtoUser
                {
                    Username = participante.Email,
                    Email = participante.Email,
                    EmailConfirmed = true
                },
                Password = new RegisterAppUserDtoPassword
                {
                    Password = UserSecretsHelper.SenhaPadraoUsuarioAplicativo,
                    ConfirmPassword = UserSecretsHelper.SenhaPadraoUsuarioAplicativo
                }
            };

            var serializedDto = JsonConvert.SerializeObject(dto);
            var httpContent = new StringContent(serializedDto, Encoding.UTF8, "application/json");

            var response = client.PostAsync(UserSecretsHelper.UrlRegisterAppUser, httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                // Associa o usuário do AuthGateway ao participante
                var responseString = response.Content.ReadAsStringAsync().Result;
                participante.UsuarioAplicativoId = responseString.Replace("\"", "");

                // Atualiza os dados do participante
                var result = participanteAPI.AlterarParticipante(participante);
                return result;
            }
            else return new OperationResult(StatusCodeEnum.Error, response.ReasonPhrase);
        }
        #endregion
    }
}
