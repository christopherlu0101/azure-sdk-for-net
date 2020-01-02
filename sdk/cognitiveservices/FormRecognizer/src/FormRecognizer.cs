using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{
    public class FormRecognizerClient
    {
        private FormRecognizerCredential _formRecognizerCredential;
        private HttpPipeline _httpPipeline;
        private RequestUriBuilder _uriBuilder;
        private string _endpoint;

        public FormRecognizerClient(string endpoint, string subscriptionKey)
        {
            _formRecognizerCredential = new FormRecognizerCredential(subscriptionKey);
            _httpPipeline = new HttpPipeline(new HttpClientTransport());
            _endpoint = endpoint;
            _uriBuilder = new RequestUriBuilder();
        }

        public async Task<Response<string>> StartAnalyzeReceiptAsync(Stream fileStream, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var request = _httpPipeline.CreateRequest())
            {
                request.Content = RequestContent.Create(fileStream);
                request.Headers.Add("Content-Type", "application/octet-stream");
                return await StartAnalyzeReceiptAsync(request, cancellationToken);
            }                
        }

        public async Task<Response<string>> StartAnalyzeReceiptAsync(string imageUri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var request = _httpPipeline.CreateRequest())
            {
                var json = JsonSerializer.Serialize<AnalyzeUrlRequest>(new AnalyzeUrlRequest() { source = new Uri(imageUri) });
                request.Content = RequestContent.Create(new MemoryStream(Encoding.UTF8.GetBytes(json)));
                request.Headers.Add("Content-Type", "application/json");
                return await StartAnalyzeReceiptAsync(request, cancellationToken);
            }
        }

        private async Task<Response<string>> StartAnalyzeReceiptAsync(Request request, CancellationToken cancellationToken)
        {
            _uriBuilder.Reset(new Uri((_endpoint + (_endpoint.EndsWith("/") ? "" : "/") + "prebuilt/receipt/analyze")));
            await _formRecognizerCredential.ProcessHttpRequestAsync(request, cancellationToken);
            request.Method = RequestMethod.Post;
            request.Uri = _uriBuilder;     
            
            var response = await _httpPipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.Status == 202)
            {
                string location;
                response.Headers.TryGetValue("Operation-Location", out location);
                return Response.FromValue(GetOperationId(location), response);
            }
            else
            {
                throw await response.CreateRequestFailedExceptionAsync();
            }
        }

        public async Task<Response<string>> GetReceiptAnalyzeResultAsync(string guid, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var request = _httpPipeline.CreateRequest())
            {
                _uriBuilder.Reset(new Uri((_endpoint + (_endpoint.EndsWith("/") ? "" : "/") + "prebuilt/receipt/analyzeResults/" + guid)));
                await _formRecognizerCredential.ProcessHttpRequestAsync(request, cancellationToken);
                request.Method = RequestMethod.Get;
                request.Uri = _uriBuilder;                

                var response = await _httpPipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                if (response.Status == 200)
                {             
                    using (var reader = new StreamReader(response.ContentStream))
                    {                        
                        var content = reader.ReadToEnd();

                        //using (var json = JsonDocument.Parse(content, default))
                        //{
                        //    var root = json.RootElement;
                        //    if (root.TryGetProperty("analyzeResult", out JsonElement documentsValue))
                        //    {                                
                        //        foreach (var documentElement in documentsValue.EnumerateObject())
                        //        {
                        //            Console.WriteLine(documentElement);
                        //        }
                        //    }
                        //}


                        return Response.FromValue(content, response);
                    }
                }
                else
                {
                    throw await response.CreateRequestFailedExceptionAsync();
                }
            }
        }

        public static string GetOperationId(string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var parts = uri.Trim(new[] { '/' }).Split('/');
            if (parts.Length == 0)
            {
                throw new ArgumentException("Invalid Operation URL.");
            }
            return parts[parts.Length - 1];
        }

    }

    public class AnalyzeResult
    {

    }

    public class AnalyzeUrlRequest
    {
        public Uri source { get; set; }
    }

    public class FormRecognizerCredential
    {
        private readonly string _subscriptionKey;
        private const string ApiKeyHeader = "Ocp-Apim-Subscription-Key";

        public FormRecognizerCredential(string subscriptionKey)
        {
            _subscriptionKey = subscriptionKey;
        }

        public Task ProcessHttpRequestAsync(Request request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Headers.Add(ApiKeyHeader, this._subscriptionKey);
            return Task.FromResult<object>(null);
        }
    }
}
