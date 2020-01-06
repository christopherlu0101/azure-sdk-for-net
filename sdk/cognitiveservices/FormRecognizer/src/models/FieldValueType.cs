using System.Runtime.Serialization;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models
{
    public enum FieldValueType
    {
        [EnumMember(Value = "string")]
        String,
        [EnumMember(Value = "date")]
        Date,
        [EnumMember(Value = "time")]
        Time,
        [EnumMember(Value = "phoneNumber")]
        PhoneNumber,
        [EnumMember(Value = "number")]
        Number,
        [EnumMember(Value = "integer")]
        Integer,
        [EnumMember(Value = "array")]
        Array,
        [EnumMember(Value = "object")]
        Object
    }
    internal static class FieldValueTypeEnumExtension
    {
        internal static string ToSerializedValue(this FieldValueType? value)
        {
            return value == null ? null : ((FieldValueType)value).ToSerializedValue();
        }

        internal static string ToSerializedValue(this FieldValueType value)
        {
            switch( value )
            {
                case FieldValueType.String:
                    return "string";
                case FieldValueType.Date:
                    return "date";
                case FieldValueType.Time:
                    return "time";
                case FieldValueType.PhoneNumber:
                    return "phoneNumber";
                case FieldValueType.Number:
                    return "number";
                case FieldValueType.Integer:
                    return "integer";
                case FieldValueType.Array:
                    return "array";
                case FieldValueType.Object:
                    return "object";
            }
            return null;
        }

        internal static FieldValueType? ParseFieldValueType(this string value)
        {
            switch( value )
            {
                case "string":
                    return FieldValueType.String;
                case "date":
                    return FieldValueType.Date;
                case "time":
                    return FieldValueType.Time;
                case "phoneNumber":
                    return FieldValueType.PhoneNumber;
                case "number":
                    return FieldValueType.Number;
                case "integer":
                    return FieldValueType.Integer;
                case "array":
                    return FieldValueType.Array;
                case "object":
                    return FieldValueType.Object;
            }
            return null;
        }
    }
}
