using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{
    public class FormRecognizerClient
    {
        public Credential Credentials { get; set; }
        private HttpPipeline _httpPipeline;
        private RequestUriBuilder _uriBuilder;

        public FormRecognizerClient(string endpoint, string APIKey)
        {
            Credentials = new Credential(APIKey);
            _httpPipeline = new HttpPipeline(new HttpClientTransport());
            _uriBuilder = new RequestUriBuilder();
            _uriBuilder.Reset(new Uri(endpoint));
        }

        public async Task<string> StartAnalyzeReceiptAsync(Stream fileStream, CancellationToken cancellationToken = default(CancellationToken))
        {
            // _uriBuilder.AppendPath("prebuilt/receipt/analyze", false);
            using (var request = _httpPipeline.CreateRequest())
            {
                request.Method = RequestMethod.Post;
                request.Uri = _uriBuilder;
                if (Credentials != null)
                {
                    await Credentials.ProcessHttpRequestAsync(request, cancellationToken);
                }
                request.
                request.Content = RequestContent.Create(fileStream);
                var response = await _httpPipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                if (response.Status == 202)
                {
                    string id;
                    response.Headers.TryGetValue("Operation-Location", out id);
                    return id;
                }
                else
                {
                    throw await response.CreateRequestFailedExceptionAsync();
                }
            }
        }

        //public async Task<Response<AnalyzeResult>> GetReceiptAnalyzeResultAsync()
        //{

        //}


    }

    public class AnalyzeResult
    {

    }

    public class Credential
    {
        private readonly string _subscriptionKey;
        private const string ApiKeyHeader = "Ocp-Apim-Subscription-Key";

        public Credential(string subscriptionKey)
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
