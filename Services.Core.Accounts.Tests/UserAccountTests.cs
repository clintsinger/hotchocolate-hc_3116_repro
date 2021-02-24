using System;
using System.Threading.Tasks;
using Xunit;
using Services.Core.Accounts.Tests.Types;
using Services.Core.Accounts.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using KellermanSoftware.CompareNetObjects;
using Xunit.Asserts.Compare;
using Services.Core.Accounts.Interface.Schema;
using Services.Core.Accounts.Tests.Services;
using Services.Core.Accounts.Tests.Asserts;
using Services.Core.Accounts.Interface;

namespace Services.Core.Accounts.Tests
{
    public class UserAccountTests : AccountTestsBase
    {

        public UserAccountTests(ServicesFixture services)
            : base(services)
        {
        }

        /// <summary>
        /// Does a simple User creation.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task create_user()
        {
            var orgId = Guid.NewGuid();

            var input = new CreateUserInput(
                "Clint",
                "Singer",
                "clintsinger",
                "clint@example.com",
                Guid.NewGuid());

            var actual = await this.CreateUserAsync(input);

            var expected = new UserAccount(
                Guid.NewGuid(),
                AccountStatus.Active,
                "Clint",
                "Singer",
                "clintsinger",
                "clint@example.com"
            );

            AccountAssert.Equal(expected, actual);
        }
    }
}
