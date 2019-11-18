using Microsoft.Azure.CognitiveServices.FormRecognizer;
using System.Net.Http;
using System.IO;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using System;

namespace FormRecognizerSDK.Tests
{
    public abstract class BaseTests
    {
        private static readonly string FormRecognizerSubscriptionKey;

        protected const string ContentTypePdf = "Pdf";

        static BaseTests()
        {
            // Retrieve the configuration information.
            FormRecognizerSubscriptionKey = "00000000000000000000000000000000";
        }

        protected IFormRecognizerClient GetFormRecognizerClient(params DelegatingHandler[] handler)
        {
            IFormRecognizerClient client = new FormRecognizerClient(new ApiKeyServiceClientCredentials(FormRecognizerSubscriptionKey), handlers: handler)
            {
                // Endpoint = "https://westus.api.cognitive.microsoft.com"
                Endpoint = "https://example.org"
            };

            (client as FormRecognizerClient)
                .HttpClient
                .DefaultRequestHeaders
                .Add("apim-subscription-id", FormRecognizerSubscriptionKey);

            return client;
        }
        protected string GetTestImagePath(string name)
        {
            return Path.Combine("TestImages", name);
        }

        protected AnalysisContentType ParseContentType(string contentType)
        {
            switch (contentType)
            {
                case ContentTypePdf:
                    return AnalysisContentType.Pdf;

                default:
                    throw new NotImplementedException("Cannot parse content type " + contentType);
            }
        }
    }
}
