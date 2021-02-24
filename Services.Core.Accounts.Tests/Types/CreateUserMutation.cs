

using GraphQL;
using HotChocolate.Execution.Configuration;
using Services.Core.Accounts.Interface.Schema;

namespace Services.Core.Accounts.Tests.Types
{
    public class CreateUserResponse
    {
        public CreateUserPayload? UsersCreate { get; set; }
    }

    public class CreateUserMutation : GraphQLClient<CreateUserResponse>
    {
        public CreateUserMutation(IRequestExecutorBuilder builder)
            : base (builder)
        {

        }

        public override string Query => @"
        mutation CreateUser($input: CreateUserInput!) {
          usersCreate(input: $input) {
            user {
              id
              firstName
              lastName
              status              
              username
              email
              phones {
                description
                number
                sms
              }
              addresses {
                description
                street
                city
                state
                country
                postalCode        
              }
              refLink
              notes
            }            
          }
        }";

        public override string[]? VariableNames => new[] { "input" };
    }
}
