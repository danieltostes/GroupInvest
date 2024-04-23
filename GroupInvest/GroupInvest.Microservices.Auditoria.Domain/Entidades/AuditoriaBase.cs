using GroupInvest.Common.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Auditoria.Domain.Entidades
{
    /// <summary>
    /// Classe base para auditoria das entidades
    /// </summary>
    public class AuditoriaBase : Entidade<int>
    {
        /// <summary>
        /// Data e hora do registro
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Indentificador do agregado
        /// </summary>
        public int AgregadoId { get; set; }

        /// <summary>
        /// Descrição do agregado
        /// </summary>
        public string Agregado { get; set; }

        /// <summary>
        /// Operação realizada
        /// </summary>
        public string Operacao { get; set; }

        /// <summary>
        /// Conteúdo do objeto auditado
        /// </summary>
        public string Conteudo { get; set; }
    }
}
