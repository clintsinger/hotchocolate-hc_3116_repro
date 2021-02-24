using AutoMapper;
using Services.Core.Accounts.Data.Entities;
using Services.Core.Accounts.GraphQL.Payloads;
using Services.Core.Accounts.Interface.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Core.Accounts.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper mapper;
        
        public UserRepository(IMapper mapper)
        {
            this.mapper = mapper;            
        }

        public async Task<CreateUserPayloadInternal> CreateUserAsync(CreateUserInput input, CancellationToken cancellationToken)
        {
            var entity = this.mapper.Map<CreateUserInput, UserAccountEntity>(input);

            return new CreateUserPayloadInternal(
                User: entity,
                Password: null
            );
        }
    }
}
