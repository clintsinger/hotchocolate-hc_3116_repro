using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraphQL.Converters
{
    internal class PolymorphicTypeConverter<T> : JsonConverter<T> 
        where T : class
    {
        private readonly IEnumerable<Type> types;

        public PolymorphicTypeConverter(IEnumerable<Type> types)
        {
            this.types = types;
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var x = reader;
            using var jsonDocument = JsonDocument.ParseValue(ref x);
            var jsonObject = jsonDocument.RootElement;

            //var clone = new Utf8JsonReader(reader.);

            var type = this.GetObjectType(reader);

            var localOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new UuidGuidConverter(),
                    new SnakeCaseStringEnumConverterFactory()                    
                }
            };

            var theConverted = (T)JsonSerializer.Deserialize(ref reader, type ?? typeToConvert, localOptions)!;

            return theConverted;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private Type? GetObjectType(Utf8JsonReader reader)
        {
            // Match using __typename
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return default!;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();
                    if (propertyName == "__typename")
                    {
                        var expectedType = reader.GetString();
                        foreach (var type in types)
                        {
                            if (type.Name == expectedType)
                            {
                                return type;
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
