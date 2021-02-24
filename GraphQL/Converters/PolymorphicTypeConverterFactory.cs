using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraphQL.Converters
{
    public class PolymorphicTypesConverterFactory : JsonConverterFactory
    {
        private readonly IEnumerable<Type> types;

        public PolymorphicTypesConverterFactory(IEnumerable<Type> types)
        {
            this.types = types;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            if (this.types?.DefaultIfEmpty() is null)
            {
                return false;
            }

            foreach (var type in this.types)
            {
                if (typeToConvert.IsAssignableFrom(type))
                {
                    return true;
                }
            }

            return false;
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            foreach (var type in this.types)
            {
                if (typeToConvert.IsAssignableFrom(type))
                {
                    JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                       typeof(PolymorphicTypeConverter<>).MakeGenericType(typeToConvert),
                       BindingFlags.Instance | BindingFlags.Public,
                       binder: null,
                       args: new[] { this.types },
                       culture: null)!;

                    return converter;
                }
            }

            return null;
        }
    }
}
