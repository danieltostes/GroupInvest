using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.APIs;
using GroupInvest.Microservices.AppUsuarios.Application.Models.Dtos;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System.Collections.Generic;

namespace GroupInvest.Microservices.AppUsuarios.Application
{
    public class MensalidadeApi : IMensalidadeApi
    {
        #region Injeção de dependência
        private readonly IServicoMensalidade servicoMensalidade;
        #endregion

        #region Construtor
        public MensalidadeApi(IServicoMensalidade servicoMensalidade)
        {
            this.servicoMensalidade = servicoMensalidade;
        }
        #endregion

        #region IMensalidadeApi
        public IReadOnlyCollection<MensalidadeDto> ListarMensalidadesParticipante(int participanteId)
        {
            var mensalidades = servicoMensalidade.ListarMensalidadesParticipante(participanteId);
            var lista = new List<MensalidadeDto>();

            foreach (var mensalidade in mensalidades)
            {
                var dto = new MensalidadeDto
                {
                    DataVencimento = mensalidade.DataVencimento,
                    ValorDevido = mensalidade.ValorDevido
                };

                if (mensalidade.Pagamento != null)
                {
                    dto.DataPagamento = mensalidade.Pagamento.DataPagamento;
                    dto.ValorPago = mensalidade.Pagamento.ValorPago;

                    if (mensalidade.Pagamento.PercentualJuros > 0)
                        dto.PercentualJuros = mensalidade.Pagamento.PercentualJuros;
                }

                lista.Add(dto);
            }

            return lista;
        }
        #endregion
    }
}
