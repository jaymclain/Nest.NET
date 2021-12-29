using System;
using System.Reflection;
using Nest.NET.Service.Model;
using Newtonsoft.Json;

namespace Nest.NET.Service.Infrastructure.Json
{
    public class ItemIdJsonConverter<T> : JsonConverter where T : Entity
    {
        private readonly Type _objectType;

        public ItemIdJsonConverter()
        {
            _objectType = typeof(T);
        }

        public override bool CanConvert(Type objectType)
        {
            return _objectType.GetTypeInfo().IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var result = Activator.CreateInstance(_objectType) as T;
            if (result == null)
                throw new InvalidOperationException();

            if (reader.TokenType == JsonToken.String)
                result.Id = (string?)reader.Value ?? string.Empty;

            return result;
        }

        public override void WriteJson(JsonWriter writer, object? value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
