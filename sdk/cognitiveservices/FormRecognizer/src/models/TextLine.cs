using System.Collections.Generic;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models
{
    public class TextLine
    {
        public string Text { get; set; }
        public IList<double> BoundingBox { get; set; }
        public string Language { get; set; }
        public IList<TextWord> Words { get; set; }
    }
}
