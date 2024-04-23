using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Application.Models.Dtos
{
    public class ParticipanteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        public string UsuarioAplicativoId { get; set; }
    }
}

