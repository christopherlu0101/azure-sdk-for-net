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
    public static class FormRecoginzerSerializer2
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

        // Add property should modify this function.
        // TODO : same type should be factor out.
        private static object ParseProperty(ref Utf8JsonReader reader, Type type)
        {
            var name = type.FullName;
            if (name == typeof(string).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.String)
                {
                    return reader.GetString();
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if(name == typeof(DateTime).FullName)
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
            else if (name == typeof(double?).FullName)
            {
                if (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.Number)
                    {
                        return reader.GetDouble();
                    }
                    else if (reader.TokenType == JsonTokenType.Null)
                    {
                        return null;
                    }
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(LengthUnit).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.String)
                {
                    var value = reader.GetString();
                    Enum.TryParse(value, true, out LengthUnit enumValue);
                    return enumValue;
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(FieldValueType).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.String)
                {
                    var value = reader.GetString();
                    Enum.TryParse(value, true, out FieldValueType enumValue);
                    return enumValue;
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(AnalyzeResult).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.StartObject)
                {
                    return ParseObject(ref reader, typeof(AnalyzeResult));
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(IList<ReadResult>).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.StartArray)
                {
                    return ParseArray<ReadResult>(ref reader);
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(IList<PageResult>).FullName)
            {
                return null;
            }
            else if (name == typeof(IList<DocumentResult>).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.StartArray)
                {
                    return ParseArray<DocumentResult>(ref reader);
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(IList<int>).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.StartArray)
                {
                    return ParseArray<int>(ref reader);
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(IList<double>).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.StartArray)
                {
                    return ParseArray<double>(ref reader);
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(IList<FieldValue>).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.StartArray)
                {
                    return ParseArray<FieldValue>(ref reader);
                }
                throw new Exception($"Invalid {name} property.");
            }
            else if (name == typeof(IDictionary<string, FieldValue>).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.StartObject)
                {
                    return ParseStringDictionary<FieldValue>(ref reader);
                }
                throw new Exception($"Invalid {name} property.");
            }
            else
            {
                return null;
            }
        }

        private static object ParseStringDictionary<T>(ref Utf8JsonReader reader)
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

        private static object ParseArray<T>(ref Utf8JsonReader reader)
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

        private static object ParseObject(ref Utf8JsonReader reader, Type type)
        {
            var obj = Activator.CreateInstance(type);
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

        private static object CreateInstanceByType(Type type)
        {
            var name = type.FullName;
            if (name == typeof(String).FullName)
            {
                return "abc";
            }
            else if (name == typeof(IList<ReadResult>).FullName)
            {
                return new List<ReadResult>();
            }
            else
            {
                return null;
            }
        }
    }
}
