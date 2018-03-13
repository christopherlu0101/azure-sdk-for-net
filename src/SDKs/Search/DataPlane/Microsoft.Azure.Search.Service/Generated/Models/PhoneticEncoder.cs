// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Search.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines values for PhoneticEncoder.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PhoneticEncoder
    {
        [EnumMember(Value = "metaphone")]
        Metaphone,
        [EnumMember(Value = "doubleMetaphone")]
        DoubleMetaphone,
        [EnumMember(Value = "soundex")]
        Soundex,
        [EnumMember(Value = "refinedSoundex")]
        RefinedSoundex,
        [EnumMember(Value = "caverphone1")]
        Caverphone1,
        [EnumMember(Value = "caverphone2")]
        Caverphone2,
        [EnumMember(Value = "cologne")]
        Cologne,
        [EnumMember(Value = "nysiis")]
        Nysiis,
        [EnumMember(Value = "koelnerPhonetik")]
        KoelnerPhonetik,
        [EnumMember(Value = "haasePhonetik")]
        HaasePhonetik,
        [EnumMember(Value = "beiderMorse")]
        BeiderMorse
    }
    internal static class PhoneticEncoderEnumExtension
    {
        internal static string ToSerializedValue(this PhoneticEncoder? value)
        {
            return value == null ? null : ((PhoneticEncoder)value).ToSerializedValue();
        }

        internal static string ToSerializedValue(this PhoneticEncoder value)
        {
            switch( value )
            {
                case PhoneticEncoder.Metaphone:
                    return "metaphone";
                case PhoneticEncoder.DoubleMetaphone:
                    return "doubleMetaphone";
                case PhoneticEncoder.Soundex:
                    return "soundex";
                case PhoneticEncoder.RefinedSoundex:
                    return "refinedSoundex";
                case PhoneticEncoder.Caverphone1:
                    return "caverphone1";
                case PhoneticEncoder.Caverphone2:
                    return "caverphone2";
                case PhoneticEncoder.Cologne:
                    return "cologne";
                case PhoneticEncoder.Nysiis:
                    return "nysiis";
                case PhoneticEncoder.KoelnerPhonetik:
                    return "koelnerPhonetik";
                case PhoneticEncoder.HaasePhonetik:
                    return "haasePhonetik";
                case PhoneticEncoder.BeiderMorse:
                    return "beiderMorse";
            }
            return null;
        }

        internal static PhoneticEncoder? ParsePhoneticEncoder(this string value)
        {
            switch( value )
            {
                case "metaphone":
                    return PhoneticEncoder.Metaphone;
                case "doubleMetaphone":
                    return PhoneticEncoder.DoubleMetaphone;
                case "soundex":
                    return PhoneticEncoder.Soundex;
                case "refinedSoundex":
                    return PhoneticEncoder.RefinedSoundex;
                case "caverphone1":
                    return PhoneticEncoder.Caverphone1;
                case "caverphone2":
                    return PhoneticEncoder.Caverphone2;
                case "cologne":
                    return PhoneticEncoder.Cologne;
                case "nysiis":
                    return PhoneticEncoder.Nysiis;
                case "koelnerPhonetik":
                    return PhoneticEncoder.KoelnerPhonetik;
                case "haasePhonetik":
                    return PhoneticEncoder.HaasePhonetik;
                case "beiderMorse":
                    return PhoneticEncoder.BeiderMorse;
            }
            return null;
        }
    }
}