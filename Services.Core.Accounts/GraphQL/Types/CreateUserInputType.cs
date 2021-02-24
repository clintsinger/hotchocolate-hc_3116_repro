using HotChocolate.Types;
using Services.Core.Accounts.Interface.Schema;

namespace Services.Core.Accounts.GraphQL.Types
{
    public class CreateUserInputType : InputObjectType<CreateUserInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateUserInput> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Name(nameof(CreateUserInput));
        descriptor.Description("Input for creating a new User account.");

        descriptor.Ignore(p => p.Provider);
    }        
}
}
