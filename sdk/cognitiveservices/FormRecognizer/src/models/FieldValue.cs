using Microsoft.Rest;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    public class FieldValue
    {
        public FieldValueType Type { get; set; }
        public string ValueString { get; set; }
        public string ValueDate { get; set; }
        public string ValueTime { get; set; }
        public string ValuePhoneNumber { get; set; }
        public double ValueNumber { get; set; }
        public int ValueInteger { get; set; }
        public IList<FieldValue> ValueArray { get; set; }
        public IDictionary<string, FieldValue> ValueObject { get; set; }
        public string Text { get; set; }
        public IList<double> BoundingBox { get; set; }
        public double? Confidence { get; set; }
        public IList<string> Elements { get; set; }
        public int Page { get; set; }
    }
}
