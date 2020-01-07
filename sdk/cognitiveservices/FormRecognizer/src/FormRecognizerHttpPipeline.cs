using System;
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

        public FormRecognizerHttpPipeline(HttpPipeline httpPipeline, Uri baseUri, string subscriptionKey, string apiVersion, FormRecognizerClientOptions options)
        {
            _httpPipeline = httpPipeline;
            _subscriptionKey = subscriptionKey;            
            _apiVersion = apiVersion;
            _baseUri = baseUri;
            _options = options;
        }

        public Request CreateRequest(RequestMethod method, ContentType contentType, string route)
        {
            Request request = _httpPipeline.CreateRequest();
            ProcessHeader(request, contentType, route);
            request.Method = method;
            return request;
        }

        public Response GetResponse(Request request, CancellationToken cancellationToken = default)
        {
            return GetResponseAsync(request, cancellationToken).Result;
        }

        public async ValueTask<Response> GetResponseAsync(Request request, CancellationToken cancellationToken = default)
        {
            return await _httpPipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private void ProcessHeader(Request request, ContentType contentType, string route)
        {
            request.Headers.Add("Content-Type", GetContentTypeString(contentType));
            request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
            BuildUriForRoute(route, request.Uri);
        }

        private void BuildUriForRoute(string route, RequestUriBuilder builder)
        {
            builder.Reset(_baseUri);
            builder.AppendPath(RouteNameScope.FormRecognizerRoute, escape: false);
            builder.AppendPath(_apiVersion, escape: false);
            builder.AppendPath(route, escape: false);
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
