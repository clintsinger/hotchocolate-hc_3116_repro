using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Interface.Schema
{
    public record UserAccount(
        Guid Id,
        AccountStatus Status,        
        string Username,
        string Email,        
        string FirstName,
        string LastName,
        IEnumerable<PostalAddress>? Addresses = null,
        IEnumerable<PhoneNumber>? Phones = null,
        string? RefLink = null,
        string? Notes = null
    ) : AccountBase(
        Id,
        Status,        
        Addresses,
        Phones,
        RefLink,
        Notes
    );
}
