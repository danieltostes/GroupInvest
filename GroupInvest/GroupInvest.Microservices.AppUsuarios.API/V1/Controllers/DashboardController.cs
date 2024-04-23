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
    public class DashboardController : ControllerBase
    {
        #region Injeção de dependência
        private readonly IDashboardApi dashboardAPI;
        #endregion

        #region Construtor
        public DashboardController(IDashboardApi dashboardAPI)
        {
            this.dashboardAPI = dashboardAPI;
        }
        #endregion

        #region Serviços
        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        public ActionResult<DashboardDto> ObterDashboardParticipante(int id)
        {
            var dto = dashboardAPI.ObterDashboardParticipante(id);
            if (dto != null) return Ok(dto);
            else return NotFound("Dashboard para o participante não encontrado.");
        }
        #endregion
    }
}
