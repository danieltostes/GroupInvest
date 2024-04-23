using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupInvest.Common.Domain.Results;
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
    public class PagamentosController : ControllerBase
    {
        #region Injeção de dependência
        private readonly IPagamentoApi pagamentoAPI;
        #endregion

        #region Construtor
        public PagamentosController(IPagamentoApi pagamentoAPI)
        {
            this.pagamentoAPI = pagamentoAPI;
        }
        #endregion

        #region Serviços
        [MapToApiVersion("1.0")]
        [HttpPost]
        public ActionResult RealizarPagamento([FromBody]PagamentoDto pagamento)
        {
            var result = pagamentoAPI.RealizarPagamento(pagamento);

            if (result.StatusCode == StatusCodeEnum.OK) return Created(nameof(RealizarPagamento), result.ObjectResult);
            else return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("retroativo")]
        public ActionResult RealizarPagamentoRetroativo([FromBody] PagamentoDto pagamento)
        {
            var result = pagamentoAPI.RealizarPagamentoRetroativo(pagamento);

            if (result.StatusCode == StatusCodeEnum.OK) return Created(nameof(RealizarPagamentoRetroativo), result.ObjectResult);
            else return Ok(result);
        }
        #endregion
    }
}
