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
            _formRecognizerPipeline = new FormRecognizerHttpPipeline(_baseUri, _subscriptionKey, _apiVersion, _options);
        }

        public async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(Stream fileStream, ContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {            
            var request = _formRecognizerPipeline.CreateRequest(fileStream, contentType, RequestMethod.Post, RouteNameScope.AnalyzeReceiptRoute);                                                    
            return await StartAnalyzeReceiptAsync(request, cancellationToken);                          
        }

        public async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(Uri imageUri, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = _formRecognizerPipeline.CreateRequest(imageUri, RequestMethod.Post, RouteNameScope.AnalyzeReceiptRoute);
            return await StartAnalyzeReceiptAsync(request, cancellationToken);                
        }


        private async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(Request request, CancellationToken cancellationToken)
        {
            var response = await _formRecognizerPipeline.GetResponseAsync(request, cancellationToken);            
            switch (response.Status)
            {
                // See LRO implementation.
                // - https://github.com/Azure/azure-sdk-for-net/blob/0be76a78b6e8cb86f1316d158fa63b3595763f64/sdk/keyvault/Azure.Security.KeyVault.Keys/src/DeleteKeyOperation.cs
                // - https://github.com/Azure/azure-sdk-for-net/blob/0be76a78b6e8cb86f1316d158fa63b3595763f64/sdk/keyvault/Azure.Security.KeyVault.Keys/src/KeyClient.cs#L557

                case 202:
                    if (response.Headers.TryGetValue("Operation-Location", out string locationUri))
                    {
                        var getResultRequest = _formRecognizerPipeline.CreateRequest(new Uri(locationUri), RequestMethod.Get);
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
    }
   
    //public async Task<Response<string>> GetReceiptAnalyzeResultAsync(string guid, CancellationToken cancellationToken = default(CancellationToken))
    //{
    //    // TODO: This code should live inside AnalyzeOperation.
    //    using (var request = _httpPipeline.CreateRequest())
    //    {
    //        // Rewrite
    //        _uriBuilder.Reset(new Uri((_endpoint + (_endpoint.EndsWith("/") ? "" : "/") + "prebuilt/receipt/analyzeResults/" + guid)));
    //        await _formRecognizerCredential.ProcessHttpRequestAsync(request, cancellationToken);
    //        request.Method = RequestMethod.Get;
    //        request.Uri = _uriBuilder;                

    //        var response = await _httpPipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
    //        if (response.Status == 200)
    //        {
    //            // TODO: Check for null ContentStream
    //            using (var reader = new StreamReader(response.ContentStream))
    //            {
    //                // TODO: Can we deserialize JSON directly from stream?
    //                var content = reader.ReadToEnd();
    //                FormRecognizerSerializer.Deserialize(content);
    //                // TODO: Replace with operation, passing in Operation-Location GUID.
    //                // TODO: Should we use GUID or URI as ID?
    //                return Response.FromValue(content, response);

    //                // TODO: How do we encode Status, CreatedDateTime, LastUpdatedDateTime?
    //            }
    //        }
    //    }
    //}


    public class AnalyzeUrlRequest
    {
        public Uri source { get; set; }
    }
}
