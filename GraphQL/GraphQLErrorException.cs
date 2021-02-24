using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL
{
    public class GraphQLErrorException : Exception
    {
        public GraphQLErrorException()
        {
            this.Errors = new List<GraphQLError>();
        }

        public GraphQLErrorException(IEnumerable<GraphQLError> errors, string? message) : base(CreateMessage(errors, message))
        {
            this.Errors = errors;
        }

        public GraphQLErrorException(IEnumerable<GraphQLError> errors, string? message, Exception? innerException) : base(CreateMessage(errors, message), innerException)
        {
            this.Errors = errors;
        }

        protected GraphQLErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Errors = new List<GraphQLError>();
        }

        public IEnumerable<GraphQLError> Errors { get; init; }

        public override string ToString()
        {
            return this.Message;
        }

        private static string CreateMessage(IEnumerable<GraphQLError> errors, string? message)
        {
            var sb = new StringBuilder();
            if (message != null)
            {
                sb.AppendLine(message);
            }

            foreach (var error in errors)
            {
                sb.Append($"{Environment.NewLine}{error.Message}");
            }

            return sb.ToString();
        }
    }
}
