using System;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models
{
    public class ResponseBody
    {
        public string Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public AnalyzeResult AnalyzeResult { get; set; }
    }
}
