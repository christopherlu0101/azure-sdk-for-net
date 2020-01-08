using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models
{
    [JsonConverter(typeof(FieldValueTypeConverter))]
    public enum FieldValueType
    {
        String,        
        Date,
        Time,
        PhoneNumber,
        Number,
        Integer,
        Array,
        Object
    }

    public class FieldValueTypeConverter : JsonConverter<FieldValueType>
    {
        public override FieldValueType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                Enum.TryParse(reader.GetString(), true, out FieldValueType fieldValueType);
                return fieldValueType;
            }                            
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, FieldValueType fieldValueType, JsonSerializerOptions options)
        {
            writer.WriteStringValue(CasingHelper.ToLowerCamelCasing(fieldValueType.ToString()));
        }
    }
}
