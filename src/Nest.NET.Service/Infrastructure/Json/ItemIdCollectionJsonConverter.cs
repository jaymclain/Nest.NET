// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemIdCollectionJsonConverter.cs" company="HomeRun Software Systems">
//   Copyright (c) 2017 Jay McLain
//
//   Permission is hereby granted, free of charge, to any person 
//   obtaining a copy of this software and associated documentation
//   files (the "Software"), to deal in the Software without
//   restriction, including without limitation the rights to use,
//   copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the
//   Software is furnished to do so, subject to the following
//   conditions:
//
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//   OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//   HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//   WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//   FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//   OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary>
//   Defines the ItemIdCollectionJsonConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.NET.Service.Infrastructure.Json;

public class ItemIdCollectionJsonConverter<T> : ItemIdCollectionJsonConverter
    where T : IEnumerable
{
    public ItemIdCollectionJsonConverter()
        : base(typeof(T))
    {
    }
}

public class ItemIdCollectionJsonConverter : JsonConverter
{
    private readonly Type _collectionType;
    private readonly Type _collectionItemType;

    public ItemIdCollectionJsonConverter(Type type)
    {
        var genericArguments = type.GetTypeInfo().GetGenericArguments();
        if (!genericArguments.Any())
        {
            throw new InvalidOperationException("Expected generic enumerable.");
        }

        _collectionType = type;
        _collectionItemType = genericArguments[0];
    }

    public override bool CanConvert(Type objectType)
    {
        return _collectionType.GetTypeInfo().IsAssignableFrom(objectType);
    }

    public override void WriteJson(JsonWriter writer, object? value, Newtonsoft.Json.JsonSerializer serializer)
    {
        throw new NotSupportedException($"{GetType().Name} can only for deserializing.");
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        var collectionType = typeof(List<>).MakeGenericType(_collectionItemType);
            
        var result = Activator.CreateInstance(collectionType) as IList;
        if (result == null)
            throw new InvalidOperationException();

        if (reader.TokenType == JsonToken.StartObject)
        {
            var jsonObject = JObject.Load(reader);
            jsonObject.Properties().ForEach(p => result.Add(p.Value.ToObject(_collectionItemType, serializer)));
        }

        if (reader.TokenType == JsonToken.StartArray)
        {
            var jsonArray = JArray.Load(reader);
            jsonArray.ForEach(t => result.Add(t.ToObject(_collectionItemType, serializer)));
        }

        return result;
    }
}