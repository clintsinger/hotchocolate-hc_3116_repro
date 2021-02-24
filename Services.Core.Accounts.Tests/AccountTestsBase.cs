using System;
using System.Threading.Tasks;
using Xunit;
using Services.Core.Accounts.Tests.Types;
using Services.Core.Accounts.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Asserts.Compare;
using Services.Core.Accounts.Interface.Schema;
using Services.Core.Accounts.Tests.Services;
using System.Collections.Generic;
using GraphQL;
using Microsoft.EntityFrameworkCore;
using Services.Core.Accounts.Data;

namespace Services.Core.Accounts.Tests
{
    public class AccountTestsBase : IClassFixture<ServicesFixture>
    {
        private readonly CreateUserMutation createUserMutation;
        
        public AccountTestsBase(ServicesFixture services)
        {
            System.Diagnostics.Debug.WriteLine("Creating");
            
            this.createUserMutation = services.ServiceProvider.GetService<CreateUserMutation>()
                ?? throw new ArgumentNullException(nameof(createUserMutation));
        }

        protected async Task<UserAccount?> CreateUserAsync(CreateUserInput input)
        {
            var response = await this.createUserMutation.ExecuteAsync(input);
            Assert.NotNull(response);
            GraphQLAssert.NoGraphQLErrors(response);

            var actual = response?.Data?.UsersCreate?.User;
            Assert.NotNull(actual);
            Assert.NotEqual(Guid.Empty, actual.Id);

            return actual;
        }
    }
}
