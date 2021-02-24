using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Interface
{
    public static partial class ErrorCodes
    {
        public static partial  class Accounts
        {
            public const string NotFound = "ACCOUNT_NOT_FOUND";
            public const string InvalidAccount = "ACCOUNT_INVALID";
            public const string InvalidInput = "ACCOUNT_INVALID_INPUT";
            public const string InvalidUsername = "ACCOUNT_INVALID_USERNAME";
            public const string InvalidEmail = "ACCOUNT_INVALID_EMAIL";            
        }
    }
}
