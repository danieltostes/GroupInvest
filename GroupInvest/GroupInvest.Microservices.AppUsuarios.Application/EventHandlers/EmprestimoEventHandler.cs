using AutoMapper;
using GroupInvest.Common.Application.Interfaces;
using GroupInvest.Common.Domain.Results;
using GroupInvest.Microservices.AppUsuarios.Application.Commands.Emprestimos;
using GroupInvest.Microservices.AppUsuarios.Application.Events.Emprestimos;
using GroupInvest.Microservices.AppUsuarios.Application.Interfaces.Handlers;
using EventHandler = GroupInvest.Common.Application.EventHandlers.EventHandler;

namespace GroupInvest.Microservices.AppUsuarios.Application.EventHandlers
{
    public class EmprestimoEventHandler : EventHandler, IEmprestimoEventHandler
    {
        #region Injeção de dependência
        private readonly IMediatorHandler bus;
        private readonly IMapper mapper;
        #endregion

        #region Construtor
        public EmprestimoEventHandler(IMediatorHandler bus, IMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }
        #endregion

        #region IEmprestimoEventHandler
        public OperationResult Handle(IntegracaoEmprestimoConcedidoEvent evento)
        {
            var command = mapper.Map<IntegracaoEmprestimoConcedidoEvent, RegistrarEmprestimoConcedidoCommand>(evento);
            var result = bus.SendCommand(command);

            return result;
        }

        public OperationResult Handle(IntegracaoEmprestimoAtualizacaoSaldoEvent evento)
        {
            var command = new RegistrarAtualizacaoSaldoEmprestimosCommand(evento.Saldos);
            var result = bus.SendCommand(command);

            return result;
        }

        public OperationResult Handle(EmprestimoConcedidoRegistradoEvent evento)
        {
            return OperationResult.OK;
        }

        public OperationResult Handle(SaldosEmprestimosAtualizadosEvent evento)
        {
            return OperationResult.OK;
        }
        #endregion
    }
}
