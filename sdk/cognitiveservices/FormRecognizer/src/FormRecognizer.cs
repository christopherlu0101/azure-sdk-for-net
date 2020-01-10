/*
BUGS:
- LRO examples missing virtual async
- LRO example.  Should we return Response<OperationStatus> or OperationStatus directly?  What should the RawResponse be?
- Why doesn't DeleteKeyOperation.cs implement GetStatus?  It doesn't appear to follow the pattern.
- WriteNumberValue() doesn't support roundtrip format.
*/

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Net.Mime;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models;

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

    public class FormRecognizerClient
    {
        private readonly string _subscriptionKey;
        private readonly FormRecognizerHttpPipeline _formRecognizerPipeline;
        private readonly Uri _baseUri;
        private readonly FormRecognizerClientOptions _options;
        private readonly string _apiVersion;        

        public FormRecognizerClient(Uri baseUri, string subscriptionKey, FormRecognizerClientOptions options = null)
        {
            _subscriptionKey = subscriptionKey;
            _options = options ?? new FormRecognizerClientOptions();
            _apiVersion = _options.GetVersionString();
            _baseUri = baseUri;
            _formRecognizerPipeline = new FormRecognizerHttpPipeline(_subscriptionKey, _options);
        }

        public async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(Stream fileStream, ContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {            
            var request = _formRecognizerPipeline.CreateRequest();
            ProcessContentType(request, contentType);
            request.Content = RequestContent.Create(fileStream);
            return await StartAnalyzeReceiptAsync(request, cancellationToken);                          
        }

        public async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(Uri imageUri, CancellationToken cancellationToken = default(CancellationToken))
        {            
            var request = _formRecognizerPipeline.CreateRequest();
            ProcessContentType(request, ContentType.Json);
            var jsonString = JsonSerializer.Serialize<AnalyzeUrlRequest>(new AnalyzeUrlRequest() { source = imageUri });
            request.Content = RequestContent.Create(Encoding.UTF8.GetBytes(jsonString));
            return await StartAnalyzeReceiptAsync(request, cancellationToken);                
        }

        private async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(Request request, CancellationToken cancellationToken)
        {
            request.Method = RequestMethod.Post;
            ProcessUri(request, RouteNameScope.AnalyzeReceiptRoute);
            var response = await _formRecognizerPipeline.SendRequestAsync(request, cancellationToken);            
            switch (response.Status)
            {
                // See LRO implementation.
                // - https://github.com/Azure/azure-sdk-for-net/blob/0be76a78b6e8cb86f1316d158fa63b3595763f64/sdk/keyvault/Azure.Security.KeyVault.Keys/src/DeleteKeyOperation.cs
                // - https://github.com/Azure/azure-sdk-for-net/blob/0be76a78b6e8cb86f1316d158fa63b3595763f64/sdk/keyvault/Azure.Security.KeyVault.Keys/src/KeyClient.cs#L557

                case 202:
                    if (response.Headers.TryGetValue("Operation-Location", out string locationUri))
                    {
                        var getResultRequest  =_formRecognizerPipeline.CreateRequest();
                        getResultRequest.Uri.Reset(new Uri(locationUri));
                        getResultRequest.Method = RequestMethod.Get;
                        return new AnalyzeReceiptOperation(_formRecognizerPipeline, getResultRequest);                     
                    }
                    else
                    {
                        throw await response.CreateRequestFailedExceptionAsync();
                    }
                default:
                    throw await response.CreateRequestFailedExceptionAsync();
            }
        }


        private void ProcessContentType(Request request, ContentType contentType)
        {
            request.Headers.Add("Content-Type", GetContentTypeString(contentType));
        }

        private void ProcessUri(Request request, string uriRoute)
        {
            request.Uri.Reset(_baseUri);
            request.Uri.AppendPath(RouteNameScope.FormRecognizerRoute, escape: false);
            request.Uri.AppendPath(_apiVersion, escape: false);
            request.Uri.AppendPath(uriRoute, escape: false);
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
        private class AnalyzeUrlRequest
        {
            public Uri source { get; set; }
        }
    }  
}
