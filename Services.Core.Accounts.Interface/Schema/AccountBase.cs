using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Interface.Schema
{
    public abstract record AccountBase(
        Guid Id,
        AccountStatus Status,        
        IEnumerable<PostalAddress>? Addresses = null,
        IEnumerable<PhoneNumber>? Phones = null,
        string? RefLink = null,
        string? Notes = null
    ) : IAccount;    
}
