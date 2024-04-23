using GroupInvest.Common.Application.Commands;
using GroupInvest.Common.Application.Events;
using GroupInvest.Common.Domain.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GroupInvest.Common.Application.Interfaces
{
    /// <summary>
    /// Interface para o Bus
    /// </summary>
    public interface IMediatorHandler
    {
        OperationResult PublishEvent<T>(T evento) where T : Event;
        OperationResult SendCommand<T>(T command) where T : Command;
    }
}
