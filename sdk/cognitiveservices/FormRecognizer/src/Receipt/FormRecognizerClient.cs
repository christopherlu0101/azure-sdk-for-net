using System;
using System.IO;
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
    public partial class FormRecognizerClient
    {
        public virtual async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(Stream fileStream, ContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope("Azure.AI.CognitiveServices.FormRecognizerClient.StartAnalyzeReceipt");
            scope.Start();
            try
            {
                var request = _formRecognizerPipeline.CreateRequest();
                ProcessContentType(request, contentType);
                request.Content = RequestContent.Create(fileStream);
                return await StartAnalyzeReceiptAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public virtual async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(Uri imageUri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope("Azure.AI.CognitiveServices.FormRecognizerClient.StartAnalyzeReceipt");
            scope.Start();
            try
            {
                var request = _formRecognizerPipeline.CreateRequest();
                ProcessContentType(request, ContentType.Json);
                var jsonString = JsonSerializer.Serialize<AnalyzeUrlRequest>(new AnalyzeUrlRequest() { source = imageUri });
                request.Content = RequestContent.Create(Encoding.UTF8.GetBytes(jsonString));
                return await StartAnalyzeReceiptAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public virtual Operation<AnalyzeResult> StartAnalyzeReceipt(string locationId)
        {
            return GetAnalyzeReceiptResult(new Uri(locationId));
        }

        private async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(Request request, CancellationToken cancellationToken)
        {
            request.Method = RequestMethod.Post;
            ProcessAnalyzeReceiptUri(request, RouteNameScope.AnalyzeReceiptRoute);
            var response = await _formRecognizerPipeline.SendRequestAsync(request, cancellationToken);
            switch (response.Status)
            {
                // See LRO implementation.
                // - https://github.com/Azure/azure-sdk-for-net/blob/0be76a78b6e8cb86f1316d158fa63b3595763f64/sdk/keyvault/Azure.Security.KeyVault.Keys/src/DeleteKeyOperation.cs
                // - https://github.com/Azure/azure-sdk-for-net/blob/0be76a78b6e8cb86f1316d158fa63b3595763f64/sdk/keyvault/Azure.Security.KeyVault.Keys/src/KeyClient.cs#L557

                case 202:
                    if (response.Headers.TryGetValue("Operation-Location", out string locationUri))
                    {
                        return GetAnalyzeReceiptResult(new Uri(locationUri));
                    }
                    else
                    {
                        throw await response.CreateRequestFailedExceptionAsync("Invalid header : Operation-Location not found.");
                    }
                default:
                    throw await response.CreateRequestFailedExceptionAsync($"Bad request {response.Status}");
            }
        }

        private Operation<AnalyzeResult> GetAnalyzeReceiptResult(Uri location)
        {
            var getResultRequest = _formRecognizerPipeline.CreateRequest();
            getResultRequest.Uri.Reset(location);
            getResultRequest.Method = RequestMethod.Get;
            return new AnalyzeReceiptOperation(_formRecognizerPipeline, getResultRequest);
        }

        private void ProcessAnalyzeReceiptUri(Request request, string uriRoute)
        {
            request.Uri.Reset(_baseUri);
            request.Uri.AppendPath(RouteNameScope.FormRecognizerRoute, escape: false);
            request.Uri.AppendPath(_apiVersion, escape: false);
            request.Uri.AppendPath(uriRoute, escape: false);

            if (_options.IncludeTextDetails)
            {
                request.Uri.AppendQuery("includeTextDetails", "true");
            }
        }
    }
}
