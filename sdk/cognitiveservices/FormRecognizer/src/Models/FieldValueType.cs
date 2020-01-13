// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Semantic data type of the field value.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FieldValueType
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Date,
        String,
        Time,
        PhoneNumber,
        Number,
        Integer,
        Array,
        Object
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}