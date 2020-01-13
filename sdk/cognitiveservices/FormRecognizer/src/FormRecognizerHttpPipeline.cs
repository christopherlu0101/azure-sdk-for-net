// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer
{
    internal class FormRecognizerHttpPipeline
    {
        public const string FormRecognizerRoute = "formrecognizer/";
        public const string AnalyzeReceiptRoute = "/prebuilt/receipt/analyze";
        public const string GetReceiptRoute = "/prebuilt/receipt/analyzeResults";

        private readonly Uri _baseUri;
        private readonly HttpPipeline _httpPipeline;
        private readonly string _subscriptionKey;
        private readonly FormRecognizerClientOptions _options;

        public FormRecognizerHttpPipeline(Uri baseUri, string subscriptionKey, FormRecognizerClientOptions options)
        {
            _baseUri = baseUri;
            _subscriptionKey = subscriptionKey;
            _httpPipeline = HttpPipelineBuilder.Build(options);
            _options = options;
        }

        public Request CreateAnalyzeReceiptRequest(bool includeTextDetails = false)
        {
            var request = CreateRequest();
            request.Method = RequestMethod.Post;
            SetRequestUri(request, AnalyzeReceiptRoute);
            if (includeTextDetails)
            {
                request.Uri.AppendQuery("includeTextDetails", "true");
            }
            return request;
        }

        public Request CreateRequest()
        {
            Request request = _httpPipeline.CreateRequest();
            request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
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

        private void SetRequestUri(Request request, params string[] paths)
        {
            request.Uri.Reset(_baseUri);
            request.Uri.AppendPath(FormRecognizerRoute, escape: false);
            request.Uri.AppendPath(_options.GetVersionString(), escape: false);
            foreach (var path in paths)
            {
                request.Uri.AppendPath(path, escape: false);
            }
        }
    }
}
