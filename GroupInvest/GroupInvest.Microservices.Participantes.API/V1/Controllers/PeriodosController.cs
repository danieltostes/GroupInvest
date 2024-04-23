using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Participantes.Application.Interfaces;
using GroupInvest.Microservices.Participantes.Application.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupInvest.Microservices.Participantes.API.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Policy = "Admin")]
    public class PeriodosController : ControllerBase
    {
        #region Injeção de Dependência
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
        [HttpPost]
        public ActionResult IncluirPeriodo([FromBody]PeriodoDto periodo)
        {
            var result = periodoAPI.IncluirPeriodo(periodo);

            if (result.StatusCode == StatusCodeEnum.OK)
            {
                var periodoIncluido = periodoAPI.ObterPeriodoPorAnoReferencia(periodo.AnoReferencia);
                return Created(nameof(IncluirPeriodo), periodoIncluido);
            }
            else
                return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{anoReferencia}")]
        public ActionResult<PeriodoDto> ObterPeriodoPorAnoReferencia(int anoReferencia)
        {
            var periodo = periodoAPI.ObterPeriodoPorAnoReferencia(anoReferencia);

            if (periodo != null) return periodo;
            else return NotFound("Período não encontrado");
        }
        #endregion
    }
}
