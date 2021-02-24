using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraphQL.Converters
{
    public class SnakeCaseStringEnumConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
               typeof(SnakeCaseEnumConverter<>).MakeGenericType(typeToConvert),
               BindingFlags.Instance | BindingFlags.Public,
               binder: null,
               null,
               culture: null)!;

            return converter;
        }
    }
}
