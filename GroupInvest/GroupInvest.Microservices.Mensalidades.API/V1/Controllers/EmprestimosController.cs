using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.Mensalidades.Application.Interfaces;
using GroupInvest.Microservices.Mensalidades.Application.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GroupInvest.Microservices.Mensalidades.API.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Policy = "Admin")]
    public class EmprestimosController : ControllerBase
    {
        #region Injeção de dependência
        private readonly IEmprestimoApi emprestimoAPI;
        #endregion

        #region Construtor
        public EmprestimosController(IEmprestimoApi emprestimoAPI)
        {
            this.emprestimoAPI = emprestimoAPI;
        }
        #endregion

        #region Serviços
        [MapToApiVersion("1.0")]
        [HttpPost]
        public ActionResult ConcederEmprestimo([FromBody] ConcessaoEmprestimoDto dto)
        {
            var result = emprestimoAPI.ConcederEmprestimo(dto);

            if (result.StatusCode == StatusCodeEnum.OK)
            {
                var emprestimo = emprestimoAPI.ObterEmprestimoParticipante(dto.ParticipanteId, dto.DataEmprestimo);
                return Created(nameof(ConcederEmprestimo), emprestimo);
            }
            else
                return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("AtualizarSaldos")]
        public ActionResult AtualizarSaldosEmprestimosIntegracao([FromBody]DateTime? dataReferencia)
        {
            DateTime data = dataReferencia.HasValue ? dataReferencia.GetValueOrDefault() : DateTime.Today;
            var result = emprestimoAPI.AtualizarSaldoEmprestimosIntegracao(data);
            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public ActionResult<IReadOnlyCollection<EmprestimoDto>> ListarTodosEmprestimos()
        {
            return Ok(emprestimoAPI.ListarTodosEmprestimos());
        }

        /// <summary>
        /// Lista as previsões de pagamento do empréstimo
        /// </summary>
        /// <param name="id">Identificador do empréstimo</param>
        /// <returns>Previsões de pagamento</returns>
        [MapToApiVersion("1.0")]
        [HttpGet("{id}/debitos")]
        public ActionResult ListarPrevisoesPagamentoEmprestimo(int id)
        {
            return Ok(emprestimoAPI.ListarPrevisoesPagamentoEmprestimo(id));
        }
        #endregion
    }
}
