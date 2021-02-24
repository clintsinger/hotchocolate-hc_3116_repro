using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Interface
{
    public class AccountException : Exception
    {
        public AccountException()
        {
            this.Code = string.Empty;
        }

        public AccountException(string code, string? message) : base(message)
        {
            this.Code = code;
        }

        public AccountException(string code, string? message, Exception? innerException) : base(message, innerException)
        {
            this.Code = code;
        }

        protected AccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Code = string.Empty;
        }

        public string Code { get; init; }
    }
}
