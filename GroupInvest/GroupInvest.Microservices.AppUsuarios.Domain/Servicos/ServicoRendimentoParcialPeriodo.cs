using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Domain.Entidades;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Repositorios;
using GroupInvest.Microservices.AppUsuarios.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Microservices.AppUsuarios.Domain.Servicos
{
    public class ServicoRendimentoParcialPeriodo : IServicoRendimentoParcialPeriodo
    {
        #region Injeção de dependência
        private readonly IRepositorioRendimentoParcialPeriodo repositorioRendimentoParcial;
        #endregion

        #region Construtor
        public ServicoRendimentoParcialPeriodo(IRepositorioRendimentoParcialPeriodo repositorioRendimentoParcial)
        {
            this.repositorioRendimentoParcial = repositorioRendimentoParcial;
        }
        #endregion

        #region IServicoRendimentoParcialPeriodo
        public OperationResult RegistrarRendimentoParcial(RendimentoParcialPeriodo rendimento)
        {
            // verifica se existe um rendimento na data de referencia
            try
            {
                var rendimentoReferencia = repositorioRendimentoParcial.ObterRendimentoParcialPeriodoDataReferencia(rendimento.DataReferencia);
                if (rendimentoReferencia != null)
                {
                    rendimentoReferencia.DataAtualizacao = rendimento.DataAtualizacao;
                    rendimentoReferencia.PercentualRendimento = rendimento.PercentualRendimento;
                    repositorioRendimentoParcial.Alterar(rendimentoReferencia);
                }
                else
                    repositorioRendimentoParcial.Incluir(rendimento);

                return OperationResult.OK;
            }
            catch(Exception ex)
            {
                return new OperationResult(StatusCodeEnum.Error, ex.Message);
            }
        }

        public IReadOnlyCollection<RendimentoParcialPeriodo> ListarRendimentosParciais()
        {
            return repositorioRendimentoParcial.ListarRendimentosParciais();
        }
        #endregion
    }
}
