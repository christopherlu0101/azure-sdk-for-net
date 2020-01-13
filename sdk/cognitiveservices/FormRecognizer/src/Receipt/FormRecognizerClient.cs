// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer
{
    public partial class FormRecognizerClient
    {
        /// <summary>
        /// Start analyze receipt by stream content.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Operation<AnalyzeResult> StartAnalyzeReceipt(Stream content, ContentType contentType, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            return StartAnalyzeReceiptAsync(content, contentType, includeTextDetails, cancellationToken).Result;
        }

        /// <summary>
        /// Asynchronous start analyze receipt by stream content.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(Stream content, ContentType contentType, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            // TODO: Do we want to support auto-detecting content type?
            // TODO: Validate fileStream format consistency with contentType.
            Argument.AssertNotNull(content, nameof(content));
            Argument.AssertNotNull(contentType, nameof(contentType));

            using DiagnosticScope scope = _clientDiagnostics.CreateScope("Azure.AI.FormRecognizer.FormRecognizerClient.StartAnalyzeReceipt");
            scope.AddAttribute("contentType", contentType.MediaType);
            scope.Start();
            try
            {
                using var request = _formRecognizerPipeline.CreateAnalyzeReceiptRequest(includeTextDetails);
                request.Headers.Add("Content-Type", contentType.MediaType);
                request.Content = RequestContent.Create(content);
                return await StartAnalyzeDocumentAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Start analyze receipt by content uri
        /// </summary>
        /// <param name="contentUri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Operation<AnalyzeResult> StartAnalyzeReceipt(Uri contentUri, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            return StartAnalyzeReceiptAsync(contentUri, includeTextDetails, cancellationToken).Result;
        }

        /// <summary>
        /// Asynchronous start analyze receipt by content uri
        /// </summary>
        /// <param name="contentUri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<Operation<AnalyzeResult>> StartAnalyzeReceiptAsync(Uri contentUri, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNull(contentUri, nameof(contentUri));

            using DiagnosticScope scope = _clientDiagnostics.CreateScope("Azure.AI.FormRecognizer.FormRecognizerClient.StartAnalyzeReceipt");
            scope.AddAttribute("uri", contentUri);
            scope.Start();
            try
            {
                using var request = _formRecognizerPipeline.CreateAnalyzeReceiptRequest(includeTextDetails);
                // TODO: Factor serialization options
                var requestJson = JsonSerializer.Serialize(new AnalyzeRequest() { Source = contentUri }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
                request.Headers.Add("Content-Type", "application/json");
                request.Content = RequestContent.Create(Encoding.UTF8.GetBytes(requestJson));
                return await StartAnalyzeDocumentAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Start analyze receipt by an existed operation Location.
        /// </summary>
        /// <param name="operationLocation"></param>
        /// <returns></returns>
        public virtual Operation<AnalyzeResult> StartAnalyzeReceipt(string operationLocation)
        {
            Argument.AssertNotNull(operationLocation, nameof(operationLocation));

            using DiagnosticScope scope = _clientDiagnostics.CreateScope("Azure.AI.FormRecognizer.FormRecognizerClient.StartAnalyzeReceipt");
            scope.AddAttribute("operationLocation", operationLocation);
            scope.Start();
            try
            {
                return GetAnalyzeDocumentResult(new Uri(operationLocation));
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
    }
}
