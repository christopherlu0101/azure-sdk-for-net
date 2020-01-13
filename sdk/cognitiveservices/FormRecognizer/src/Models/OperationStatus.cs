// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Status of the queued operation.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OperationStatus
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        NotStarted,
        Running,
        Succeeded,
        Failed
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

}
