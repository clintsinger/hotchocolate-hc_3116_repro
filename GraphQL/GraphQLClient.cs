using HotChocolate.Execution;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using HotChocolate;
using System.Collections.Generic;
using HotChocolate.Execution.Configuration;
using GraphQL.Converters;
using System.Text.Json.Serialization;

namespace GraphQL
{
    public abstract class GraphQLClient<TResponse>
    {
        private readonly IRequestExecutorBuilder executorBuilder;
        private readonly List<Type> types;

        public GraphQLClient(IRequestExecutorBuilder builder)
        {
            this.executorBuilder = builder;
            this.types = new List<Type>();
        }

        public abstract string Query { get; }

        public abstract string[]? VariableNames { get; }

        public GraphQLClient<TResponse> AddType<Type>()
        {
            this.AddType(typeof(Type));
            return this;
        }

        public GraphQLClient<TResponse> AddType(Type type)
        {
            this.types.Add(type);
            return this;
        }


        public Task<GraphQLResult<TResponse>?> ExecuteAsync(bool throwErrors = true)
        {
            var variables = new Dictionary<string, object?>();

            return this.ExecuteAsyncInternal(variables, throwErrors);
        }

        public Task<GraphQLResult<TResponse>?> ExecuteAsync<T1>(T1 first, bool throwErrors = true)
        {
            var variables = new Dictionary<string, object?>();

            if (this.VariableNames != null)
            {
                if (this.VariableNames.Length > 0)
                {
                    variables[this.VariableNames[0]] = ConvertType(first);
                }
            }

            return this.ExecuteAsyncInternal(variables, throwErrors);
        }

        public Task<GraphQLResult<TResponse>?> ExecuteAsync<T1, T2>(T1 first, T2 second, bool throwErrors = true)
        {
            var variables = new Dictionary<string, object?>();

            if (this.VariableNames != null)
            {
                if (this.VariableNames.Length > 0)
                {
                    variables[this.VariableNames[0]] = ConvertType(first);
                }

                if (this.VariableNames.Length > 1)
                {
                    variables[this.VariableNames[1]] = ConvertType(second);
                }
            }

            return this.ExecuteAsyncInternal(variables, throwErrors);
        }

        private async Task<GraphQLResult<TResponse>?> ExecuteAsyncInternal(IReadOnlyDictionary<string, object?> variableValues, bool throwErrors)
        {
            var executor = await this.executorBuilder.BuildRequestExecutorAsync();

            var result = await executor.ExecuteAsync(builder =>
            {
                builder.SetQuery(this.Query);

                if (variableValues.Count > 0)
                {
                    builder.SetVariableValues(variableValues);
                }
            });

            if (result is null)
            {
                throw new Exception("Failed to execute query.");
            }

            var json = await result.ToJsonAsync();
            if (json is null)
            {
                throw new Exception("Failed to extract json response.");
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new UuidGuidConverter(),
                    new SnakeCaseStringEnumConverterFactory(),
                    new PolymorphicTypesConverterFactory(this.types)
                }
            };

            var obj = JsonSerializer.Deserialize<GraphQLResult<TResponse>>(json, options);
            if (throwErrors && obj?.Errors != null)
            {
                throw new GraphQLErrorException(obj.Errors, "Error occurred during GraphQL execution");
            }

            return obj;
        }

        /// <summary>
        /// Converts the incoming type to a specialized GraphQL type (like ID) 
        /// or passes the value directly if no conversion can be made.
        /// </summary>
        /// <typeparam name="T">Type of object to be converted</typeparam>
        /// <param name="value">Value of type to be converted</param>
        /// <returns>Converte type or original passed in value.</returns>
        private object? ConvertType<T>(T value)
        {
            object? result = value;

            if (value is Guid g)
            {
                // Convert the Guid to a ID type string.
                result = g.ToString("N");
            }

            return result;
        }
    }
}