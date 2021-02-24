using Services.Core.Accounts.Data.Entities;
using Services.Core.Accounts.GraphQL.Payloads;
using Services.Core.Accounts.Interface.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Services
{
    public interface IUserRepository
    {
        Task<CreateUserPayloadInternal> CreateUserAsync(CreateUserInput input, CancellationToken cancellationToken);        
    }
}
