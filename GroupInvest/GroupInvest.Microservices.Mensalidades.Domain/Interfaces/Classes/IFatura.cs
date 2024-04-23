using GroupInvest.Microservices.Mensalidades.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.Mensalidades.Domain.Interfaces.Classes
{
    public interface IFatura
    {
        DateTime DataPagamento { get; }
        decimal ValorTotal { get; }
        IReadOnlyCollection<ItemPagamento> Itens { get; }

        void AdicionarMensalidade(Mensalidade mensalidade);
        void AdicionarPrevisaoPagamentoEmprestimo(PrevisaoPagamentoEmprestimo previsaoPagamento, decimal valorPagamento);
    }
}
