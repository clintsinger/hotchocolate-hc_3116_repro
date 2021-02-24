using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Interface.Schema
{
    public record CreateUserInput(
        string FirstName,
        string LastName,
        string Username,
        string Email,
        Guid OrganizationId,
        PasswordInput? Password = null,
        IEnumerable<PostalAddress>? Addresses = null,
        IEnumerable<PhoneNumber>? Phones = null,
        string? RefLink = null,
        string? Notes = null,
        ProviderInput? Provider = null
    );
}
