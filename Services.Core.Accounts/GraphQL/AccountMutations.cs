using HotChocolate;
using Services.Core.Accounts.GraphQL.Payloads;
using Services.Core.Accounts.Interface.Schema;
using Services.Core.Accounts.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Core.Accounts.GraphQL
{
    public class AccountMutations 
    {
        
        /// <summary>
        /// Creates User Account
        /// </summary>
        /// <param name="input"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        public Task<CreateUserPayloadInternal> UsersCreateAsync(
            CreateUserInput input,
            [Service] IUserRepository repository,
            CancellationToken cancellationToken) => repository.CreateUserAsync(input, cancellationToken);
    }
}