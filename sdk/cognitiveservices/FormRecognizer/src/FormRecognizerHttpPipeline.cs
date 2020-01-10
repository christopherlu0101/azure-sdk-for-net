using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{
    internal class FormRecognizerHttpPipeline
    {
        private readonly HttpPipeline _httpPipeline;
        private readonly string _subscriptionKey;        

        public FormRecognizerHttpPipeline(string subscriptionKey, FormRecognizerClientOptions options)
        {            
            _subscriptionKey = subscriptionKey;            
            _httpPipeline = HttpPipelineBuilder.Build(options);
        }

        public Request CreateRequest()
        {
            Request request = _httpPipeline.CreateRequest();
            ProcessApiKey(request);
            return request;
        }

        public Response SendRequest(Request request, CancellationToken cancellationToken = default)
        {
            return SendRequestAsync(request, cancellationToken).Result;
        }

        public async Task<Response> SendRequestAsync(Request request, CancellationToken cancellationToken = default)
        {
            return await _httpPipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private void ProcessApiKey(Request request)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
        }
    }
}
