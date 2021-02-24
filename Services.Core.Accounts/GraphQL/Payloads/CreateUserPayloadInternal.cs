using Services.Core.Accounts.Data.Entities;
using Services.Core.Accounts.Interface.Schema;

namespace Services.Core.Accounts.GraphQL.Payloads
{
    public record CreateUserPayloadInternal(
        UserAccountEntity User,
        Password Password 
    );
}
