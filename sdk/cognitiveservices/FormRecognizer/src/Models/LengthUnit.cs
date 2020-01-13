// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Length unit.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LengthUnit
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Pixel,
        Inch
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
