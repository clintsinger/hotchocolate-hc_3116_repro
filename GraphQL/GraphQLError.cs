using System.Collections.Generic;
using System.Diagnostics;

namespace GraphQL
{
    [DebuggerDisplay("{Message}")]
    public class GraphQLError
    {
        public string? Message { get; set; }
        public IEnumerable<GraphQLErrorLocation>? Locations { get; set; }
        public GraphQLExtension? Extensions { get; set; }
    }
}
