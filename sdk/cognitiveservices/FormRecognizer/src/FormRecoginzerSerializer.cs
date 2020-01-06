using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{
    internal static class StringExtension
    {
        public static string ToUpperCamelCasing(this string str)
        {
            return Char.ToUpperInvariant(str[0]) + str.Substring(1);
        }

        public static string ToLowerCamelCasing(this string str)
        {
            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }

    public class FormRecoginzerSerializer
    {
        # region serialization        
        public static string Serialize(ResponseBody body)
        {
            var stream = new MemoryStream();
            var writer = new Utf8JsonWriter(stream);

            writer.WriteStartObject();
            SerializeProperty(ref writer, typeof(ResponseBody), body);
            writer.WriteEndObject();
            writer.Flush();                       
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        public static void SerializeProperty(ref Utf8JsonWriter writer, Type type, object obj)
        {
            foreach (var property in type.GetProperties())
            {
                var value = property.GetValue(obj, null);
                if (value != null)
                {
                    writer.WritePropertyName(property.Name.ToLowerCamelCasing());
                    SerializeObject(ref writer, property.PropertyType, value);
                }
            }
        }

        public static void SerializeObject(ref Utf8JsonWriter writer, Type type, object value)
        {
            if (type.Name == typeof(int).Name)
            {
                writer.WriteNumberValue((int)value);
            }
            else if (type.Name == typeof(double).Name)
            {
                writer.WriteNumberValue((double)value);
            }
            else if (type.Name == typeof(string).Name || type.IsEnum)
            {
                writer.WriteStringValue(value.ToString());
            }
            else if (type.Name == typeof(DateTime).Name)
            {
                writer.WriteStringValue((DateTime)value);
            }
            else if (type.IsGenericType)
            {
                // List
                if (type.Name == typeof(IList<int>).Name)
                {
                    writer.WriteStartArray();
                    var contentType = type.GetGenericArguments()[0];
                    foreach (var obj in (System.Collections.IEnumerable)value)
                    {
                        SerializeObject(ref writer, contentType, obj);
                    }                    
                    writer.WriteEndArray();
                }
                // Dictionary
                else if (type.Name == typeof(IDictionary<string, FieldValue>).Name)
                {
                    // TODO : generalize FieldValue
                    if (type.FullName == typeof(IDictionary<string, FieldValue>).FullName)
                    {
                        writer.WriteStartObject();
                        foreach (var kv in (Dictionary<string, FieldValue>)value)
                        {
                            writer.WritePropertyName(kv.Key.ToLowerCamelCasing());
                            var contentType = type.GetGenericArguments()[1];
                            SerializeObject(ref writer, contentType, kv.Value);
                        }
                        writer.WriteEndObject();
                    }
                    else
                    {
                        throw new Exception($"Unsupport dictionary type : {type.FullName}");
                    }                    
                }
                // Nullable
                else if (type.Name == typeof(int?).Name)
                {
                    var contentType = type.GetGenericArguments()[0];
                    if (contentType.Name == typeof(double).Name)
                    {
                        writer.WriteNumberValue(((double?)value).Value);
                    }
                    else if (contentType.Name == typeof(int).Name)
                    {
                        writer.WriteNumberValue(((int?)value).Value);
                    }
                    else
                    {
                        throw new Exception("Unsupported number type.");
                    }                    
                }
            }
            else if (type.Name == typeof(FieldValue).Name)
            {
                writer.WriteStartObject();
                SerializeFieldValue(ref writer, (FieldValue)value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteStartObject();
                SerializeProperty(ref writer, type, value);
                writer.WriteEndObject();

            }
        }

        public static void SerializeFieldValue(ref Utf8JsonWriter writer, FieldValue fieldValue)
        {
            foreach (var property in typeof(FieldValue).GetProperties().OrderBy(p => p.Name).ToArray())
            {
                if (!property.Name.Contains("Value"))
                {
                    var propertyValue = property.GetValue(fieldValue, null);
                    if (propertyValue != null)
                    {
                        writer.WritePropertyName(property.Name.ToLowerCamelCasing());
                        SerializeObject(ref writer, property.PropertyType, propertyValue);
                    }
                }
            }

            switch (fieldValue.Type)
            {
                case FieldValueType.Array:
                    writer.WritePropertyName("ValueArray".ToLowerCamelCasing());
                    SerializeObject(ref writer, fieldValue.ValueArray.GetType(), fieldValue.ValueArray);
                    break;
                case FieldValueType.Object:
                    writer.WritePropertyName("ValueObject".ToLowerCamelCasing());
                    SerializeObject(ref writer, fieldValue.ValueObject.GetType(), fieldValue.ValueObject);
                    break;
                case FieldValueType.Integer:
                    writer.WriteNumber("ValueInteger".ToLowerCamelCasing(), fieldValue.ValueInteger);
                    break;
                case FieldValueType.Number:
                    writer.WriteNumber("ValueNumber".ToLowerCamelCasing(), fieldValue.ValueNumber);
                    break;
                case FieldValueType.Date:
                    writer.WriteString("ValueDate".ToLowerCamelCasing(), fieldValue.ValueDate);
                    break;
                case FieldValueType.Time:
                    writer.WriteString("ValueTime".ToLowerCamelCasing(), fieldValue.ValueTime);
                    break;
                case FieldValueType.PhoneNumber:
                    writer.WriteString("ValuePhoneNumber".ToLowerCamelCasing(), fieldValue.ValuePhoneNumber);
                    break;
                case FieldValueType.String:
                    writer.WriteString("ValueString".ToLowerCamelCasing(), fieldValue.ValueString);
                    break;
            }
        }
        # endregion

        # region deserialization
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
                    var property = typeof(ResponseBody).GetProperty(name.ToUpperCamelCasing());
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
                        var property = type.GetProperty(name.ToUpperCamelCasing());
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
        # endregion
    }
}
