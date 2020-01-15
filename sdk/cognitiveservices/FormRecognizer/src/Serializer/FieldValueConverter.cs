using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer
{
    internal class FieldValueConverter : JsonConverter<FieldValue>
    {
        public override FieldValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonConverterHelper.Read<FieldValue>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, FieldValue fieldValue, JsonSerializerOptions options)
        {
            JsonConverterHelper.Write<FieldValue>(writer, fieldValue, options);
        }
    }
}
