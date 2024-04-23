using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace GroupInvest.Common.Domain.Results
{
    public class OperationResult
    {
        private readonly List<string> messages;
        private readonly object objectResult;

        public IReadOnlyCollection<string> Messages { get => messages; }
        public object ObjectResult { get => objectResult; }
        public StatusCodeEnum StatusCode { get; }

        #region Construtor
        public OperationResult(StatusCodeEnum statusCode) :
            this(statusCode, "")
        {
        }

        public OperationResult(StatusCodeEnum statusCode, string message) :
            this(statusCode, new List<string>{ message })
        {
        }

        public OperationResult(StatusCodeEnum statusCode, IEnumerable<string> messages)
        {
            this.messages = new List<string>(messages);
            this.StatusCode = statusCode;
        }

        public OperationResult(object objectResult)
        {
            this.StatusCode = StatusCodeEnum.OK;
            this.messages = new List<string>();
            this.objectResult = objectResult;
        }
        #endregion

        #region Métodos
        public void AddMessage(string message)
        {
            messages.Add(message);
        }

        public void AddMessages(IEnumerable<string> messages)
        {
            this.messages.AddRange(messages);
        }

        public static OperationResult OK
        {
            get { return new OperationResult(StatusCodeEnum.OK); }
        }
        #endregion
    }
}
