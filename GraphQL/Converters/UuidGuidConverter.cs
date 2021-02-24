using System;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GraphQL.Converters
{
    public class UuidGuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var uuid = reader.GetString();
            if (uuid != null)
            {
                if (Utf8Parser.TryParse(
                     Encoding.ASCII.GetBytes(uuid).AsSpan(), out Guid guid, out int _, 'N'))
                {
                    return guid;
                }
            }

            return Guid.Empty;
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
