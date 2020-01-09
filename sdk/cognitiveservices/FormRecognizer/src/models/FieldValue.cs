using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models
{    
    //[JsonConverter(typeof(FieldValueConverter))]
    public class FieldValue
    {        
        public FieldValueType Type { get; set; }
        public string ValueString { get; set; }
        public string ValueDate { get; set; }
        public string ValueTime { get; set; }
        public string ValuePhoneNumber { get; set; }
        public double ValueNumber { get; set; }
        public int ValueInteger { get; set; }
        public IList<FieldValue> ValueArray { get; set; }
        public IDictionary<string, FieldValue> ValueObject { get; set; }
        public string Text { get; set; }
        public IList<double> BoundingBox { get; set; }
        public double? Confidence { get; set; }
        public IList<string> Elements { get; set; }               
        public int? Page { get; set; }
    }

    internal static class JsonConverterHelper
    {
        public static T Read<T>(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            var value = new T();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return value;
                }
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string propertyName = reader.GetString();
                // Hard code : assume camel naming.
                var property = typeof(T).GetProperty(CasingHelper.ToUpperCamelCasing(propertyName));
                if (property != null)
                {
                    var propertyValue = JsonSerializer.Deserialize(ref reader, property.PropertyType, options);
                    property.SetValue(value, propertyValue);
                }
                // Ignore pass-in non-exist property.
            }
            throw new JsonException();
        }
    }

    public class FieldValueConverter : JsonConverter<FieldValue>
    {
        public override FieldValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonConverterHelper.Read<FieldValue>(ref reader, options);
        }

        // TODO : Serialiezed Property order should be as same as order of class property
        public override void Write(Utf8JsonWriter writer, FieldValue fieldValue, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(options.PropertyNamingPolicy.ConvertName("Type"));
            JsonSerializer.Serialize(writer, fieldValue.Type, options);
            foreach (var property in typeof(FieldValue).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                System.Diagnostics.Debug.WriteLine(property.Name);

                var propertyValue = property.GetValue(fieldValue);
                if (propertyValue != null)
                {
                    if (!property.Name.Contains("Value") || property.Name == GetValuePropertyName(fieldValue.Type))
                    {
                        // Hard code : assume camel naming.
                        writer.WritePropertyName(CasingHelper.ToLowerCamelCasing(property.Name));
                        JsonSerializer.Serialize(writer, propertyValue, options);
                    }
                }
            }
            writer.WriteEndObject();
        }

        private static string GetValuePropertyName(FieldValueType fieldValueType)
        {
            switch (fieldValueType)
            {
                // Hard code : assume camel naming.
                case FieldValueType.Array:
                    return "ValueArray";
                case FieldValueType.Object:
                    return "ValueObject";
                case FieldValueType.Integer:
                    return "ValueInteger";
                case FieldValueType.Number:
                    return "ValueNumber";
                case FieldValueType.Date:
                    return "ValueDate";
                case FieldValueType.Time:
                    return "ValueTime";
                case FieldValueType.PhoneNumber:
                    return "ValuePhoneNumber";
                case FieldValueType.String:
                    return "ValueString";
                default:
                    throw new JsonException();
            }
        }
    }

}
