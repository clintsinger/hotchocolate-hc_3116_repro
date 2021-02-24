using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Interface.Schema
{
    public record PhoneNumber(
        string Number,
        bool Sms,
        string? Description = null
    );    
}
