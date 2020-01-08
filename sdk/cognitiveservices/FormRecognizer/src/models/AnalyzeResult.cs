using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models
{
    public class AnalyzeResult
    {
        public string Version { get; set; }
        public IList<ReadResult> ReadResults { get; set; }
        public IList<PageResult> PageResults { get; set; }
        public IList<DocumentResult> DocumentResults { get; set; }
    }
}