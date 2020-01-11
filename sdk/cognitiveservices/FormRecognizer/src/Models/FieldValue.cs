using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.CognitiveServices.Vision.FormRecognizer;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models
{    
    [JsonConverter(typeof(FieldValueConverter))]
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

    public class FieldValueConverter : JsonConverter<FieldValue>
    {
        private static IList<PropertyInfo> _properties = JsonConverterHelper.GetProperties(typeof(FieldValue));

        public override FieldValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonConverterHelper.Read<FieldValue>(ref reader, options);
        }

        // TODO : Serialiezed Property order should be as same as order of class property
        public override void Write(Utf8JsonWriter writer, FieldValue fieldValue, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var property in _properties)
            {
                var propertyValue = property.GetValue(fieldValue);
                if (propertyValue != null)
                {
                    if (!property.Name.StartsWith("Value") || property.Name == GetValuePropertyName(fieldValue.Type))
                    {
                        writer.WritePropertyName(options.PropertyNamingPolicy.ConvertName(property.Name));
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
