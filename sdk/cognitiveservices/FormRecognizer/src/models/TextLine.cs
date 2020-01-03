using System.Collections.Generic;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    public class TextLine
    {
        public string Text { get; set; }
        public IList<double> BoundingBox { get; set; }
        public string Language { get; set; }
        public IList<TextWord> Words { get; set; }
    }
}
