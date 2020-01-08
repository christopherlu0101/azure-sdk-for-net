using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models
{
    [JsonConverter(typeof(LengthUnitConverter))]
    public enum LengthUnit
    {
        Pixel,
        Inch
    }

    public class LengthUnitConverter : JsonConverter<LengthUnit>
    {
        public override LengthUnit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                Enum.TryParse(reader.GetString(), true, out LengthUnit lengthUnit);
                return lengthUnit;
            }
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, LengthUnit lengthUnit, JsonSerializerOptions options)
        {
            writer.WriteStringValue(CasingHelper.ToLowerCamelCasing(lengthUnit.ToString()));
        }
    }
}
