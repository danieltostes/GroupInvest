using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupInvest.Microservices.Mensalidades.API.Helpers;
using GroupInvest.Microservices.Mensalidades.Application.Interfaces;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupInvest.Microservices.Mensalidades.API.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Policy = "Admin")]
    public class MensalidadesController : ControllerBase
    {
        #region Injeção de dependência
        private readonly IMensalidadeApi mensalidadeAPI;
        #endregion

        #region Construtor
        public MensalidadesController(IMensalidadeApi mensalidadeAPI)
        {
            this.mensalidadeAPI = mensalidadeAPI;
        }
        #endregion

        #region Serviços
        [MapToApiVersion("1.0")]
        [HttpGet("ambiente")]
        public ActionResult ObterDadosAmbiente()
        {
            var dadosAmbiente = new
            {
                UserSecretsHelper.MensalidadesDbConnectionString,
                UserSecretsHelper.ServiceBusConnectionString,
                UserSecretsHelper.APIName,
                UserSecretsHelper.SwaggerUIClientId,
                UserSecretsHelper.UrlAuthority
            };
            return Ok(dadosAmbiente);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("participante/{id}")]
        public ActionResult<IReadOnlyCollection<MensalidadeDto>> ListarMensalidadesParticipante(int id)
        {
            var mensalidades = mensalidadeAPI.ListarMensalidadesParticipante(id);
            if (mensalidades == null) return NotFound("Participante não encontrado ou não ativo no período");
            else return Ok(mensalidades.OrderBy(mens => mens.DataReferencia));
        }
        #endregion
    }
}
