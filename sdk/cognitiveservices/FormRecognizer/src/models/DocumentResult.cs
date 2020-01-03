using Microsoft.Rest;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{

    public class DocumentResult
    {
        public string DocType { get; set; }
        public IList<int> PageRange { get; set; }
        public IDictionary<string, FieldValue> Fields { get; set; }
    }
}
