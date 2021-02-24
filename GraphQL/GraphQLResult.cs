using System.Collections.Generic;

namespace GraphQL
{
    public class GraphQLResult<T>
    {
        public IEnumerable<GraphQLError>? Errors { get; set; }
        public T? Data { get; set; }
    }
}
