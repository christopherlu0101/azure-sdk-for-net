using System;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.models
{
    public class ResponseBody
    {
        public string Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public AnalyzeResult AnalyzeResult { get; set; }
    }
}
