using HotChocolate;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Accounts.GraphQL.Types;
using Services.Core.Accounts.Interface;
using System;
using System.Linq;
using System.Text.Json;

namespace Services.Core.Accounts.GraphQL.Extensions
{
    public static class AddTypesExtensions
    {

        public static IRequestExecutorBuilder AddAccountTypes(this IRequestExecutorBuilder builder)
        {
            builder.ModifyOptions(x => x.SortFieldsByName = true);

            builder
                .AddMutationType<AccountMutations>();


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            builder.UseField(next => async context =>
            {
                try
                {
                    await next(context);
                }
                catch (AccountException ex)
                {
                    context.ReportError(ErrorBuilder.New()
                                .SetCode(ex.Code)
                                .SetMessage(ex.Message)
                                .Build());
                }
            });

            
            builder
                .AddType<CreateUserInputType>();

            builder.BindRuntimeType<Guid, IdType>();

            return builder;
        }
    }
}
