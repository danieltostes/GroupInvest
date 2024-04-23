using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.App.Application.Models.Dtos
{
    public class AutenticacaoDto
    {
        public bool Autenticado { get; set; }
        public bool Autorizado { get; set; }
        public int ParticipanteId { get; set; }
        public string Informacao { get; set; }
        public string MensagemErro { get; set; }
    }
}
