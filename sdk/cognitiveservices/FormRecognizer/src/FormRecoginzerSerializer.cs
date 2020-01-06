using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.models;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{
    public class FormRecoginzerSerializer
    {
        public static ResponseBody Deserialize(string jsonString)
        {
            var result = new ResponseBody();
            ReadOnlySpan<byte> jsonReadOnlySpan = Encoding.UTF8.GetBytes(jsonString);
            var reader = new Utf8JsonReader(jsonReadOnlySpan, isFinalBlock: true, state: default);
            
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var name = reader.GetString();
                    var upperCamelName = Char.ToUpperInvariant(name[0]) + name.Substring(1);
                    var property = typeof(ResponseBody).GetProperty(upperCamelName);
                    if (property != null)
                    {
                        var propertyType = property.PropertyType;
                        var propertyValue = ParseProperty(ref reader, propertyType);
                        property.SetValue(result, propertyValue, null);
                    }                                     
                }                
            }
            return result;
        }

        public delegate object GenericDelegate(ref Utf8JsonReader reader);

        // Add property should check if type exist in this function.
        public static object ParseProperty(ref Utf8JsonReader reader, Type type)
        {
            var name = type.FullName;
            if (type.IsEnum)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.String)
                {

                    var genericParseEnum = typeof(FormRecoginzerSerializer)
                        .GetMethod("ParseEnum")
                        .MakeGenericMethod(type);
                    return genericParseEnum.Invoke(new FormRecoginzerSerializer(), new object[] { reader.GetString() });

                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (type.IsGenericType)
            {
                // Nullable
                if (type.Name == typeof(int?).Name)
                {
                    if (reader.Read())
                    {
                        if (reader.TokenType == JsonTokenType.Null)
                        {
                            return null;
                        }
                        else
                        {
                            return Convert.ChangeType(reader.GetDouble(), type.GetGenericArguments()[0]);
                        }
                    }
                    throw new Exception($"Invalid {name} property.");
                }
                // IList
                else if (type.Name == typeof(IList<int>).Name)
                {
                    var contentType = type.GetGenericArguments()[0];
                    if (reader.Read() && reader.TokenType == JsonTokenType.StartArray)
                    {
                        var genericParseArray = typeof(FormRecoginzerSerializer)
                            .GetMethod("ParseArray")
                            .MakeGenericMethod(contentType);                        
                        var delegator = (GenericDelegate)Delegate.CreateDelegate(typeof(GenericDelegate), genericParseArray);
                        return delegator.Invoke(ref reader);
                    }
                    throw new Exception($"Invalid {name} property.");
                }
                // IDictionary
                else if (type.Name == typeof(IDictionary<string, object>).Name)
                {
                    // first argments will be string
                    var contentType = type.GetGenericArguments()[1];
                    if (reader.Read() && reader.TokenType == JsonTokenType.StartObject)
                    {
                        var genericParseStringDictionary = typeof(FormRecoginzerSerializer)
                            .GetMethod("ParseStringDictionary")
                            .MakeGenericMethod(contentType);
                        var delegator = (GenericDelegate)Delegate.CreateDelegate(typeof(GenericDelegate), genericParseStringDictionary);
                        return delegator.Invoke(ref reader);
                    }
                    throw new Exception($"Invalid {name} property.");
                }
                else
                {
                    throw new Exception($"Unknown generic type {type.FullName}");
                }
            }
            else if (name == typeof(AnalyzeResult).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.StartObject)
                {
                    return ParseObject(ref reader, typeof(AnalyzeResult));
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(string).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.String)
                {
                    return reader.GetString();
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(DateTime).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.String)
                {
                    return reader.GetDateTime();
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(int).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.Number)
                {
                    return reader.GetInt32();
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(double).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.Number)
                {
                    return reader.GetDouble();
                }
                throw new Exception($"Invalid {name} property.");
            }
            else
            {
                return null;
            }
        }

        public static TEnum ParseEnum<TEnum>(string value)
            where TEnum : struct
        {
            Enum.TryParse(value, true, out TEnum enumValue);
            return enumValue;
        }

        public static object ParseStringDictionary<T>(ref Utf8JsonReader reader)
        {
            var obj = new Dictionary<string, T>();
            while(reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
            {
                var key = reader.GetString();
                if (reader.Read() && reader.TokenType == JsonTokenType.StartObject)
                {
                    var value = (T)ParseObject(ref reader, typeof(T));
                    obj.Add(key, value);
                }
                else
                {
                    throw new Exception($"Invalid Dictionary property.");
                }
            }
            return obj;
        }

        public static object ParseArray<T>(ref Utf8JsonReader reader)
        {
            var obj = new List<T>();
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    // After ParseObject position of reader should be EndObject
                    // One more Read will make position StartObject or EndArray
                    obj.Add((T)ParseObject(ref reader, typeof(T)));
                }
                return obj;
            }
            throw new Exception("Invalid array value");
        }

        public static object ParseObject(ref Utf8JsonReader reader, Type type)
        {
            // String has no parameterless constructor.
            var obj = (type.Name != typeof(string).Name) ? Activator.CreateInstance(type) : null;
            int cnt = 0;
            do
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.StartObject:
                        cnt++;
                        break;
                    case JsonTokenType.EndObject:
                        cnt--;
                        break;
                    case JsonTokenType.PropertyName:
                        var name = reader.GetString();
                        var upperCamelName = Char.ToUpperInvariant(name[0]) + name.Substring(1);
                        var property = type.GetProperty(upperCamelName);
                        if (property != null)
                        {
                            var propertyType = property.PropertyType;
                            var propertyValue = ParseProperty(ref reader, propertyType);
                            property.SetValue(obj, propertyValue, null);
                        }
                        break;
                    case JsonTokenType.Number:
                        if (type.FullName == typeof(int).FullName)
                        {
                            return reader.GetInt32();
                        }
                        else if (type.FullName == typeof(double).FullName)
                        {
                            return reader.GetDouble();
                        }
                        else
                        {
                            throw new Exception("Unsupported number type.");
                        }
                    case JsonTokenType.String:
                        return reader.GetString();
                    default:
                        break;
                }
            }
            while (cnt > 0 && reader.Read());
            return obj;
        }
    }
}
