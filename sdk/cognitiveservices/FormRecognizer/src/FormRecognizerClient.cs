/*
BUGS:
- LRO examples missing virtual async
- LRO example.  Should we return Response<OperationStatus> or OperationStatus directly?  What should the RawResponse be?
- Why doesn't DeleteKeyOperation.cs implement GetStatus?  It doesn't appear to follow the pattern.
- WriteNumberValue() doesn't support roundtrip format.
*/

using System;
using System.Net.Mime;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{

    // public virtual async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync();
    // public virtual asyncTask<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(string operationId);
    // public virtual Operation<AnalyzeResult> StartAnalyzeReceipt();
    // public virtual Operation<AnalyzeResult> StartAnalyzeReceipt(string operationId);

    // public virtual async Task<Operation<AnalyzeResult>> StartAnalyzeLayoutAsync();
    // public virtual async Task<Operation<AnalyzeResult>> StartAnalyzeLayoutAsync(string operationId);
    // public virtual Operation<AnalyzeResult> StartAnalyzeLayout();
    // public virtual Operation<AnalyzeResult> StartAnalyzeLayout(string operationId);

    internal static class RouteNameScope
    {
        public const string FormRecognizerRoute = "formrecognizer/";
        public const string AnalyzeReceiptRoute = "/prebuilt/receipt/analyze/";
        public const string GetReceiptRoute = "/prebuilt/receipt/analyzeResults/";
    }

    public enum ContentType { Jpeg, Tiff, Png, Pdf, Json }

    public partial class FormRecognizerClient
    {
        private readonly FormRecognizerHttpPipeline _formRecognizerPipeline;
        private readonly FormRecognizerClientOptions _options;
        private readonly ClientDiagnostics _clientDiagnostics;
        private readonly string _subscriptionKey;       
        private readonly Uri _baseUri;        
        private readonly string _apiVersion;        

        public FormRecognizerClient(Uri baseUri, string subscriptionKey)
        {
            Argument.AssertNotNull(baseUri, nameof(baseUri));
            Argument.AssertNotNull(subscriptionKey, nameof(subscriptionKey));

            _subscriptionKey = subscriptionKey;
            _options = new FormRecognizerClientOptions();
            _apiVersion = _options.GetVersionString();
            _baseUri = baseUri;
            _formRecognizerPipeline = new FormRecognizerHttpPipeline(_subscriptionKey, _options);
            _clientDiagnostics = new ClientDiagnostics(_options);
        }

        public FormRecognizerClient(Uri baseUri, string subscriptionKey, FormRecognizerClientOptions options)
        {
            Argument.AssertNotNull(baseUri, nameof(baseUri));
            Argument.AssertNotNull(subscriptionKey, nameof(subscriptionKey));
            Argument.AssertNotNull(options, nameof(options));

            _subscriptionKey = subscriptionKey;
            _options = options;
            _apiVersion = _options.GetVersionString();
            _baseUri = baseUri;
            _formRecognizerPipeline = new FormRecognizerHttpPipeline(_subscriptionKey, _options);
            _clientDiagnostics = new ClientDiagnostics(options);
        }

        public FormRecognizerClient(Uri baseUri, string subscriptionKey, HttpPipeline httpPipeline, FormRecognizerClientOptions options)
        {
            Argument.AssertNotNull(baseUri, nameof(baseUri));
            Argument.AssertNotNull(subscriptionKey, nameof(subscriptionKey));
            Argument.AssertNotNull(httpPipeline, nameof(httpPipeline));
            Argument.AssertNotNull(options, nameof(options));

            _subscriptionKey = subscriptionKey;
            _options = options;
            _apiVersion = _options.GetVersionString();
            _baseUri = baseUri;
            _formRecognizerPipeline = new FormRecognizerHttpPipeline(_subscriptionKey, httpPipeline);
            _clientDiagnostics = new ClientDiagnostics(options);
        }

        protected FormRecognizerClient()
        {            
        }

        private void ProcessContentType(Request request, ContentType contentType)
        {
            request.Headers.Add("Content-Type", GetContentTypeString(contentType));
        }

        private string GetContentTypeString(ContentType contentType)
        {
            switch (contentType)
            {
                case ContentType.Jpeg:
                    return MediaTypeNames.Image.Jpeg;
                case ContentType.Tiff:
                    return MediaTypeNames.Image.Tiff;
                case ContentType.Png:
                    return "image/png";
                case ContentType.Pdf:
                    return MediaTypeNames.Application.Pdf;
                case ContentType.Json:
                    return "application/json";
                default:
                    throw new Exception("Unsupported ContentType.");
            }
        }
    }  
}
