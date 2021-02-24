using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Interface.Schema
{
    public interface IAccount
    {
        Guid Id { get; }
        AccountStatus Status { get; }
        IEnumerable<PostalAddress>? Addresses { get; }
        IEnumerable<PhoneNumber>? Phones { get; }
        string? RefLink { get; }
        string? Notes { get; }
    }
}
