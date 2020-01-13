// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Linq;
using System.Text.Json;

namespace Azure.AI.FormRecognizer.Models
{
    internal static class FormRecognizerSerializer
    {
        internal static JsonSerializerOptions _defaultOptions = new JsonSerializerOptions
            {
                // UnsafeRelaxedJsonEscaping will maintain "+123" instead of something like "/00u.."
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true
            };
    }

}
