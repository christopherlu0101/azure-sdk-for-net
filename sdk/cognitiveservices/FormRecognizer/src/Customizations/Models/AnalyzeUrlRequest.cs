// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using Newtonsoft.Json;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    public class AnalyzeUrlRequest
    {
        [JsonProperty(PropertyName = "source")]
        public Uri Source { get; set; }
    }
}