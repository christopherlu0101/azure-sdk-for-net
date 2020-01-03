using System.Collections.Generic;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    public  class AnalyzeResult
    {
        public string Version { get; set; }
        public IList<ReadResult> ReadResults { get; set; }
        public IList<PageResult> PageResults { get; set; }
        public IList<DocumentResult> DocumentResults { get; set; }
    }
}
