using GraphQL;
using System.Linq;

namespace Services.Core.Accounts.Tests.Services
{
    public static class GraphQLAssert
    {
        public static void NoGraphQLErrors<T>(GraphQLResult<T>? obj)
        {
            if (obj is null)
            {
                return;
            }

            if (obj.Errors != null && obj.Errors.Any())
            {
                throw new GraphQLErrorException(obj.Errors);
            }
        }
    }
}
