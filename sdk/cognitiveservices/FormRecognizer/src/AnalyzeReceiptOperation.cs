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
    public enum OperationStatus
    {
        NotStarted,
        Running,
        Succeeded,
        Cancelled,
        Failed,
    }

    public class AnalyzeReceiptOperation : Operation<AnalyzeResult>
    {

        public override string Id => _location;
        public override AnalyzeResult Value => _value?.AnalyzeResult;
        public override bool HasCompleted => _completed;
        public override bool HasValue => (Value != null);

        private readonly FormRecognizerHttpPipeline _formRecognizerPipeline;
        private readonly string _location;
        private static readonly TimeSpan s_defaultPollingInterval = TimeSpan.FromSeconds(2);

        private Request _request;
        private Response _response;
        private bool _completed;
        private ResponseBody _value;

        internal AnalyzeReceiptOperation(FormRecognizerHttpPipeline formRecognizerPipeline, Request request)
        {
            _formRecognizerPipeline = formRecognizerPipeline;
            _request = request;
            // Id should be Uri or Guid?
            //_location = request.Uri.ToString().Split('/').Last();
            _location = request.Uri.ToString();
            _response = null;
            _value = null;
            _completed = false;
        }

        public override Response GetRawResponse()
        {
            return _response;        
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
                    _response = await _formRecognizerPipeline.SendRequestAsync(_request, cancellationToken);
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
                        _value = FormRecognizerSerializer.Deserialize(reader.ReadToEnd());
                    }
                    return _value.Status == "succeeded";
                default:
                    return true;
            }
        }
    }
}
