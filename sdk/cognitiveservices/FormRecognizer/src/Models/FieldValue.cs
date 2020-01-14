// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Recognized field value.
    /// </summary>
    [JsonConverter(typeof(FieldValueConverter))]
    public class FieldValue
    {
        /// <summary>
        /// Type of field value.
        /// </summary>
        public FieldValueType Type { get; set; }

        /// <summary>
        /// String value.
        /// </summary>
        public string ValueString { get; set; }

        /// <summary>
        /// Data value.
        /// </summary>
        public string ValueDate { get; set; }

        /// <summary>
        /// Time value.
        /// </summary>
        public string ValueTime { get; set; }

        /// <summary>
        /// Phone number value.
        /// </summary>
        public string ValuePhoneNumber { get; set; }

        /// <summary>
        /// Floating point value.
        /// </summary>
        public double ValueNumber { get; set; }

        /// <summary>
        /// Integer value.
        /// </summary>
        public int ValueInteger { get; set; }

        /// <summary>
        /// Array of field values.
        /// </summary>
        public IList<FieldValue> ValueArray { get; set; }

        /// <summary>
        /// Dictionary of named field values.
        /// </summary>
        public IDictionary<string, FieldValue> ValueObject { get; set; }

        /// <summary>
        /// Text content of the extracted field.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Bounding box of the field value, if appropriate.
        /// </summary>
        public IList<double> BoundingBox { get; set; }

        /// <summary>
        /// Confidence score.
        /// </summary>
        public double? Confidence { get; set; }

        /// <summary>
        /// When includeTextDetails is set to true, a list of references to the text elements constituting this field.
        /// </summary>
        public IList<string> Elements { get; set; }

        /// <summary>
        /// The 1-based page number in the input document.
        /// </summary>
        [IgnoreDefault(default(int))]
        public int Page { get; set; }
    }

#pragma warning disable SA1402 // File may only contain a single type
    internal class FieldValueConverter : JsonConverter<FieldValue>
#pragma warning restore SA1402 // File may only contain a single type
    {
        private static IList<PropertyInfo> _properties = JsonConverterHelper.GetProperties(typeof(FieldValue));
        public override FieldValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonConverterHelper.Read<FieldValue>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, FieldValue fieldValue, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var property in _properties)
            {
                var propertyValue = property.GetValue(fieldValue);
                if (propertyValue != null)
                {
                    if (!property.Name.StartsWith("Value", StringComparison.Ordinal) || property.Name == GetValuePropertyName(fieldValue.Type))
                    {
                        var attr = property.GetCustomAttribute(JsonConverterHelper._ignoreAttribute);
                        if (attr == null || !((IgnoreDefaultAttribute)attr).IsDefault(propertyValue))
                        {
                            var namingPolicy = options.PropertyNamingPolicy ?? JsonConverterHelper._defaultNamingPolicy;
                            writer.WritePropertyName(namingPolicy.ConvertName(property.Name));
                            JsonSerializer.Serialize(writer, propertyValue, options);
                        }
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
