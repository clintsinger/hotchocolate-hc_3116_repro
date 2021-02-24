using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Interface.Schema
{
    public record Password(
        string ClearText,
        bool Temporary,
        string? NotifyEmail = null
    );
}
