using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{
    public class AnalyzeReceiptOperation : Operation<AnalyzeResult>
    {
        public override string Id => _location;
        public override AnalyzeResult Value => _value;
        public override bool HasCompleted => _completed;
        public override bool HasValue => (Value != null);

        private readonly FormRecognizerHttpPipeline _formRecognizerPipeline;
        private readonly string _location;
        private static readonly TimeSpan s_defaultPollingInterval = TimeSpan.FromSeconds(2);

        private Request _request;
        private Response _response;
        private bool _completed;
        private AnalyzeResult _value;

        internal AnalyzeReceiptOperation(FormRecognizerHttpPipeline formRecognizerPipeline, Request request)
        {
            _formRecognizerPipeline = formRecognizerPipeline;
            _request = request;
            _location = request.Uri.ToString().Split('/').Last();
            _response = null;
            _value = null;
            _completed = false;
        }

        public override Response GetRawResponse()
        {
            if (_response == null)
            {
                _response = _formRecognizerPipeline.GetResponse(_request);
                return _response;
            }
            else
            {
                return _response;
            }
        }

        public override ValueTask<Response<AnalyzeResult>> WaitForCompletionAsync(CancellationToken cancellationToken = default) =>
            this.DefaultWaitForCompletionAsync(s_defaultPollingInterval, cancellationToken);

        public override ValueTask<Response<AnalyzeResult>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken) =>
            this.DefaultWaitForCompletionAsync(pollingInterval, cancellationToken);

        public override Response UpdateStatus(CancellationToken cancellationToken = default)
        {
            return UpdateStatusAsync().Result;
        }

        public override async ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default)
        {
            if (!_completed)
            {
                try
                {
                    _response = await _formRecognizerPipeline.GetResponseAsync(_request, cancellationToken);
                    _completed = CheckCompleted(_response);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return GetRawResponse();
        }

        private bool CheckCompleted(Response response)
        {
            switch (response.Status)
            {
                case 200:
                    using (var reader = new StreamReader(response.ContentStream))
                    {
                        // TODO: Can we deserialize JSON directly from stream?
                        var content = reader.ReadToEnd();
                        _value = JsonSerializer.Deserialize<AnalyzeResult>(content);
                    }
                    return true;
                case 403: // Access denied but proof the key was deleted.
                    return true;
                case 404:
                    return false;
                default:
                    throw response.CreateRequestFailedException();
            }
        }
    }
}
