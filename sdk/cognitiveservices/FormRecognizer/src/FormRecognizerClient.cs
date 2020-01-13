// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

/*
DISCUSSIONS:
- Constructor pattern for using API/subscription key
- Best practices for MIME type representation
- Namespace?
- Should operation ID include the full Url or just the GUID?
- Why do we need Start<OperationName>Async(string operationId) when it is instantaneous?
- Does CreateRequestFailedExceptionAsync() support unified error message handling?
- TextAnalyticsClient class summary is incorrect.
- Why doesn't Operation.WaitForCompletionAsync(polling, token) have default for token?
- Latest Operation interface doesn't have a good way inform client that an operation has failed.
  - KeyVault returns not completed on 404, which may lead to infinite loop?
    https://github.com/Azure/azure-sdk-for-net/blob/0be76a78b6e8cb86f1316d158fa63b3595763f64/sdk/keyvault/Azure.Security.KeyVault.Certificates/src/RecoverDeletedCertificateOperation.cs#L117

SERIALIZATION:
- System.Text.Json doesn't support camelCase policy for enum values via JsonStringEnumConverter because JsonConverterAttribute doesn't allow additional constructor parameters.
- Guideline suggests using internal setters.  But this breaks JsonSerialization.Deserialize().
- There is no way to set a default value for a property so that it is not serialized (and initialized from deserialization).

BUGS:
- LRO examples missing virtual async
- LRO example.  Should we return Response<OperationStatus> or OperationStatus directly?  What should the RawResponse be?
- Why doesn't DeleteKeyOperation.cs implement GetStatus?  It doesn't appear to follow the pattern.
- WriteNumberValue() doesn't support roundtrip format.
- "protected ConfigurationClient();" is invalid syntax
*/

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// The client to use for interacting with the Azure Form Recognizer service.
    /// </summary>
    public partial class FormRecognizerClient
    {
        private readonly FormRecognizerHttpPipeline _formRecognizerPipeline;
        private readonly ClientDiagnostics _clientDiagnostics;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="subscriptionKey"></param>
        public FormRecognizerClient(Uri baseUri, string subscriptionKey)
            : this(baseUri, subscriptionKey, new FormRecognizerClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="subscriptionKey"></param>
        /// <param name="options"></param>
        public FormRecognizerClient(Uri baseUri, string subscriptionKey, FormRecognizerClientOptions options)
        {
            Argument.AssertNotNull(baseUri, nameof(baseUri));
            Argument.AssertNotNull(subscriptionKey, nameof(subscriptionKey));
            options ??= new FormRecognizerClientOptions();

            _formRecognizerPipeline = new FormRecognizerHttpPipeline(baseUri, subscriptionKey, options);
            _clientDiagnostics = new ClientDiagnostics(options);
        }

        /// <summary>
        /// Mock support usage. <see cref="FormRecognizerClient"/> class.
        /// </summary>
        protected FormRecognizerClient()
        {
        }

        private async Task<Operation<AnalyzeResult>> StartAnalyzeDocumentAsync(Request request, CancellationToken cancellationToken)
        {
            using var response = await _formRecognizerPipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
            switch (response.Status)
            {
                case 202:
                    if (response.Headers.TryGetValue("Operation-Location", out string operationLocation))
                    {
                        return GetAnalyzeDocumentResult(new Uri(operationLocation));
                    }
                    else
                    {
                        throw await response.CreateRequestFailedExceptionAsync("Invalid header : Operation-Location not found.");
                    }
                default:
                    throw await response.CreateRequestFailedExceptionAsync();
            }
        }

        private Operation<AnalyzeResult> GetAnalyzeDocumentResult(Uri operationLocation)
        {
            return new AnalyzeResultOperation(_formRecognizerPipeline, operationLocation);
        }
    }
}
