// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;
    using System;
    using System.IO;

    /// <summary>
    /// Extension methods for FormRecognizerClient.
    /// </summary>
    public static partial class FormRecognizerClientExtensions
    {
        public static async Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(this IFormRecognizerClient operations, Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var resp = await operations.AnalyzeReceiptWithHttpMessagesAsync(uri, null, cancellationToken).ConfigureAwait(false))
            {
                return resp.Headers;
            }
        }

        public static async Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(this IFormRecognizerClient operations, Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var resp = await operations.AnalyzeReceiptWithHttpMessagesAsync(fileStream, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                return resp.Headers;
            }
        }

        public static async Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(this IFormRecognizerClient operations, byte[] byteArray, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var resp = await operations.AnalyzeReceiptWithHttpMessagesAsync(byteArray, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                return resp.Headers;
            }
        }

        public static async Task<AnalyzeOperationResult> AnalyzeReceiptAsync(this IFormRecognizerClient operations, Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeReceiptWithHttpMessagesAsync(uri, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var operationId = GetOperationId(header.OperationLocation);
                return await operations.WaitForOperation((ct) => operations.GetAnalyzeReceiptResultAsync(operationId, ct), cancellationToken);
            }
        }

        public static async Task<AnalyzeOperationResult> AnalyzeReceiptAsync(this IFormRecognizerClient operations, Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeReceiptWithHttpMessagesAsync(fileStream, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var operationId = GetOperationId(header.OperationLocation);
                return await operations.WaitForOperation((ct) => operations.GetAnalyzeReceiptResultAsync(operationId, ct), cancellationToken);
            }
        }

        public static async Task<AnalyzeOperationResult> AnalyzeReceiptAsync(this IFormRecognizerClient operations, byte[] byteArray, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeReceiptWithHttpMessagesAsync(byteArray, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var operationId = GetOperationId(header.OperationLocation);
                return await operations.WaitForOperation((ct) => operations.GetAnalyzeReceiptResultAsync(operationId, ct), cancellationToken);
            }
        }

        /// <summary>
        /// Get Analyze Receipt Result
        /// </summary>
        /// <remarks>
        /// Track the progress and obtain the result of the analyze receipt operation.
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resultId'>
        /// Analyze operation result identifier.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async Task<AnalyzeOperationResult> GetAnalyzeReceiptResultAsync(this IFormRecognizerClient operations, System.Guid resultId, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.GetAnalyzeReceiptResultWithHttpMessagesAsync(resultId, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
    }
}
