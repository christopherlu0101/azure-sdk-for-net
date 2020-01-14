// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text.Json;

namespace Azure.AI.FormRecognizer.Models
{
    internal static class JsonConverterHelper
    {

        public static Type _ignoreAttribute = typeof(IgnoreDefaultAttribute);
        public static JsonNamingPolicy _defaultNamingPolicy = JsonNamingPolicy.CamelCase;
        public static JsonSerializerOptions _defaultOptions = new JsonSerializerOptions
        {
            // UnsafeRelaxedJsonEscaping will maintain "+123" instead of something like "/00u.."
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true
        };

        public static T Read<T>(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            var value = Activator.CreateInstance(typeof(T));
            var typeOfT = typeof(T);
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return (T)value;
                }
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string propertyName = reader.GetString();
                var property = typeOfT.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                {
                    var propertyValue = JsonSerializer.Deserialize(ref reader, property.PropertyType, options);
                    property.SetValue(value, propertyValue);
                }
                // Ignore pass-in non-exist property.
            }
            throw new JsonException();
        }

        public static void Write<T>(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var property in GetProperties(typeof(T)))
            {
                var propertyValue = property.GetValue(value);
                if (propertyValue != null)
                {
                    var attr = property.GetCustomAttribute(_ignoreAttribute);
                    if (attr == null || !((IgnoreDefaultAttribute)attr).IsDefault(propertyValue))
                    {
                        var namingPolicy = options.PropertyNamingPolicy ?? _defaultNamingPolicy;
                        writer.WritePropertyName(namingPolicy.ConvertName(property.Name));
                        JsonSerializer.Serialize(writer, propertyValue, options);
                    }
                }
            }
            writer.WriteEndObject();
        }

        public static IList<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .OrderBy(property => property.MetadataToken).ToArray();
        }
    }

}
