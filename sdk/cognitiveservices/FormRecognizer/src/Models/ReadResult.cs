using System.Collections.Generic;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models
{
    public class ReadResult
    {
        public int Page { get; set; }
        public double Angle { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }    
        public LengthUnit Unit { get; set; }
        public string Language { get; set; }
        public IList<TextLine> Lines { get; set; }
    }
}
