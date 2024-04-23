using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.APIs;
using GroupInvest.Microservices.AppUsuarios.Application.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupInvest.Microservices.AppUsuarios.API.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Policy = "Admin")]
    public class MensalidadesController : ControllerBase
    {
        #region Injeção de dependência
        private readonly IMensalidadeApi mensalidadeApi;
        #endregion

        #region Construtor
        public MensalidadesController(IMensalidadeApi mensalidadeApi)
        {
            this.mensalidadeApi = mensalidadeApi;
        }
        #endregion

        #region Serviços
        [MapToApiVersion("1.0")]
        [HttpGet("participante/{id}")]
        public ActionResult<IReadOnlyCollection<MensalidadeDto>> ListarMensalidadesParticipante(int id)
        {
            var mensalidades = mensalidadeApi.ListarMensalidadesParticipante(id);
            return Ok(mensalidades);
        }
        #endregion
    }
}
