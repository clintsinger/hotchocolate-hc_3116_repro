using Microsoft.Extensions.DependencyInjection;
using Services.Core.Accounts.Services;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Services.Core.Accounts.Tests.Types;
using Services.Core.Accounts.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Services.Core.Accounts.Tests.Services;
using Services.Core.Accounts.GraphQL.Extensions;

namespace Services.Core.Accounts.Tests.Fixtures
{
    public class ServicesFixture
    {
        public ServicesFixture()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(
                typeof(Startup).Assembly,
                typeof(ServicesFixture).Assembly
            );

            services.AddTransient<IUserRepository, UserRepository>();
            
            var executorBuilder = services.AddGraphQL()
                .AddAccountTypes();

            services.AddSingleton(executorBuilder);

            services.AddTransient<CreateUserMutation>();

            this.ServiceProvider = services.BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider { get; init; }
    }
}
