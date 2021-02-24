using System.Diagnostics;

namespace GraphQL
{
    [DebuggerDisplay("Line={Line}, Column={Column}")]
    public class GraphQLErrorLocation
    {
        public int Line { get; set; }
        public int Column { get; set; }
    }
}
