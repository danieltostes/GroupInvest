using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Entidades
{
    public class Emprestimo
    {
        // Mapeamento do MongoDB
        public ObjectId _id { get; set; }

        public int EmprestimoId { get; set; }
        public int ParticipanteId { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public List<PagamentoEmprestimo> Pagamentos { get; set; }

        public string Situacao
        {
            get
            {
                return Pagamentos.Count.Equals(0) ? "Ativo" : "Quitado";
            }
        }

        public Emprestimo()
        {
            Pagamentos = new List<PagamentoEmprestimo>();
        }
    }
}
