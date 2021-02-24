using GraphQL;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;

namespace Services.Core.Accounts.Tests.Services
{
    public class GraphQLErrorException : NotNullException
    {
        private readonly string message;

        public GraphQLErrorException(IEnumerable<GraphQLError> errors)
        {
            var sb = new StringBuilder();
            foreach (var error in errors)
            {
                sb.Append($"{Environment.NewLine}{error.Message}");
            }

            this.message = sb.ToString();
        }

        public override string Message => this.message;
    }
}
