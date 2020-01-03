using System.Collections.Generic;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{

    public class TextWord
    {
        public string Text { get; set; }
        public IList<double> BoundingBox { get; set; }
        public double Confidence { get; set; }
    }
}
