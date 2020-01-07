using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using System.Net.Mime;
using Azure;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{
    internal class FormRecognizerHttpPipeline
    {
        private readonly HttpPipeline _httpPipeline;
        private readonly string _subscriptionKey;        
        private readonly Uri _baseUri;
        private readonly FormRecognizerClientOptions _options;
        private readonly string _apiVersion;

        public FormRecognizerHttpPipeline(Uri baseUri, string subscriptionKey, string apiVersion, FormRecognizerClientOptions options)
        {            
            _subscriptionKey = subscriptionKey;            
            _apiVersion = apiVersion;
            _baseUri = baseUri;
            _options = options;
            _httpPipeline = HttpPipelineBuilder.Build(_options);
        }

        public Request CreateRequest(Stream fileStream, ContentType contentType, RequestMethod method, string route)
        {
            var request = CreateRequest(contentType, method, route);
            request.Content = RequestContent.Create(fileStream);
            return request;
        }

        public Request CreateRequest(Uri imageUri, RequestMethod method, string route)
        {
            var request = CreateRequest(ContentType.Json, method, route);
            var jsonString = JsonSerializer.Serialize<AnalyzeUrlRequest>(new AnalyzeUrlRequest() { source = imageUri });
            request.Content = RequestContent.Create(Encoding.UTF8.GetBytes(jsonString));
            return request;
        }

        public Request CreateRequest(ContentType contentType, RequestMethod method, string uriRoute)
        {
            Request request = _httpPipeline.CreateRequest();
            ProcessApiKey(request);
            ProcessContentType(request, contentType);
            ProcessUri(request, uriRoute);
            request.Method = method;
            return request;
        }

        public Request CreateRequest(Uri uri, RequestMethod method)
        {
            var request = CreateRequest();
            request.Uri.Reset(uri);
            request.Method = method;
            return request;
        }

        public Request CreateRequest()
        {
            Request request = _httpPipeline.CreateRequest();
            ProcessApiKey(request);
            return request;
        }

        public Response GetResponse(Request request, CancellationToken cancellationToken = default)
        {
            return GetResponseAsync(request, cancellationToken).Result;
        }

        public async Task<Response> GetResponseAsync(Request request, CancellationToken cancellationToken = default)
        {
            return await _httpPipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private void ProcessContentType(Request request, ContentType contentType)
        {
            request.Headers.Add("Content-Type", GetContentTypeString(contentType));
        }

        private void ProcessApiKey(Request request)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
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
    }
}
