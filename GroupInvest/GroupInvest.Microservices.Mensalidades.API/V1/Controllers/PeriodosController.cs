using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupInvest.Microservices.Mensalidades.API.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Policy = "Admin")]
    public class PeriodosController : ControllerBase
    {
        #region Injeção de dependência
        private readonly IPeriodoApi periodoAPI;
        #endregion

        #region Construtor
        public PeriodosController(IPeriodoApi periodoAPI)
        {
            this.periodoAPI = periodoAPI;
        }
        #endregion

        #region Serviços
        [MapToApiVersion("1.0")]
        [HttpPost("Encerrar/{id}")]
        public ActionResult EncerrarPeriodo(int id)
        {
            var result = periodoAPI.EncerrarPeriodo(id);

            if (result.StatusCode == StatusCodeEnum.OK) return Ok(result.ObjectResult);
            else return Ok(result.Messages);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("CalcularRendimentoParcial")]
        public ActionResult CalcularRendimentoParcial([FromBody] DateTime dataReferencia)
        {
            var result = periodoAPI.CalcularRendimentoParcialPeriodo(dataReferencia);

            if (result.StatusCode == StatusCodeEnum.OK) return Ok(result.ObjectResult);
            else return Ok(result.Messages);
        }
        #endregion
    }
}
