using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Interface.Schema
{
    public record PostalAddress(
        string? Street = null,
        string? City = null,
        string? State = null,
        string? PostalCode = null,
        string? Country = null,
        string? Description = null
    );    
}
