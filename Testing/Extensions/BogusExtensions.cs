using Bogus;
using System;
using System.Runtime.Serialization;

namespace Testing
{
    public static class BogusExtensions
    {
        public static Faker<T> WithRecord<T>(this Faker<T> faker) where T : class
        {
            faker.CustomInstantiator(_ => FormatterServices.GetUninitializedObject(typeof(T)) as T ?? throw new Exception("Unable to match type"));
            return faker;
        }
    }
}
