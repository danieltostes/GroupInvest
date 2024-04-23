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
        [HttpGet("participante/{id}")]
        public ActionResult<IReadOnlyCollection<EmprestimoDto>> ListarEmprestimosParticipante(int id)
        {
            var emprestimos = emprestimoAPI.ListarEmprestimosParticipante(id);
            return Ok(emprestimos);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        public ActionResult ObterEmprestimo(int id)
        {
            var emprestimo = emprestimoAPI.ObterEmprestimo(id);
            return Ok(emprestimo);
        }
        #endregion
    }
}
