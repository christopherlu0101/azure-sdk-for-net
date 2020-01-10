using System;
using Azure.Core;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{
    public class FormRecognizerClientOptions : ClientOptions
    {
        public enum ServiceVersion
        {
            v2_0_preview = 1
        }

        public FormRecognizerClientOptions(ServiceVersion version = LatestVersion, bool includeTextDetails = false)
        {
            Version = version;
            IncludeTextDetails = includeTextDetails;
        }
        
        internal const ServiceVersion LatestVersion = ServiceVersion.v2_0_preview;
        public bool IncludeTextDetails;
        public ServiceVersion Version { get; }

        internal string GetVersionString()
        {
            switch (Version)
            {
                case ServiceVersion.v2_0_preview:
                    return "v2.0-preview";

                default:
                    throw new ArgumentException(Version.ToString());
            }
        }
    }
}
