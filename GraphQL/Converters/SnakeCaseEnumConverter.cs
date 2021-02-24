using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraphQL.Converters
{
    internal class SnakeCaseEnumConverter<T> : JsonConverter<T>
        where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (Enum.TryParse<T>(value, true, out T theConverted) == false)
            {
                var attemptTwo = value.Replace("_", string.Empty);
                if (Enum.TryParse<T>(attemptTwo, true, out theConverted) == false)
                {
                    return default(T);
                }
            }

            return theConverted;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
