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
        public static async Task<AnalyzeLayoutAsyncHeaders> StartAnalyzeLayoutAsync(this IFormRecognizerClient operations, string language, Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var resp = await operations.AnalyzeLayoutWithHttpMessagesAsync(language, uri, null, cancellationToken).ConfigureAwait(false))
            {
                return resp.Headers;
            }
        }

        public static async Task<AnalyzeLayoutAsyncHeaders> StartAnalyzeLayoutAsync(this IFormRecognizerClient operations, string language, Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var resp = await operations.AnalyzeLayoutWithHttpMessagesAsync(language, fileStream, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                return resp.Headers;
            }
        }

        public static async Task<AnalyzeLayoutAsyncHeaders> StartAnalyzeLayoutAsync(this IFormRecognizerClient operations, string language, byte[] byteArray, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var resp = await operations.AnalyzeLayoutWithHttpMessagesAsync(language, byteArray, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                return resp.Headers;
            }
        }

        public static async Task<AnalyzeOperationResult> AnalyzeLayoutAsync(this IFormRecognizerClient operations, string language, Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeLayoutWithHttpMessagesAsync(language, uri, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var operationId = GetOperationId(header.OperationLocation);
                return await operations.WaitForOperation((ct) => operations.GetAnalyzeLayoutResultAsync(operationId, ct), cancellationToken);
            }
        }

        public static async Task<AnalyzeOperationResult> AnalyzeLayoutAsync(this IFormRecognizerClient operations, string language, Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeLayoutWithHttpMessagesAsync(language, fileStream, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var operationId = GetOperationId(header.OperationLocation);
                return await operations.WaitForOperation((ct) => operations.GetAnalyzeLayoutResultAsync(operationId, ct), cancellationToken);
            }
        }

        public static async Task<AnalyzeOperationResult> AnalyzeLayoutAsync(this IFormRecognizerClient operations, string language, byte[] byteArray, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeLayoutWithHttpMessagesAsync(language, byteArray, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var operationId = GetOperationId(header.OperationLocation);
                return await operations.WaitForOperation((ct) => operations.GetAnalyzeLayoutResultAsync(operationId, ct), cancellationToken);
            }
        }

        /// <summary>
        /// Get Analyze Layout Result
        /// </summary>
        /// <remarks>
        /// Track the progress and obtain the result of the analyze layout operation
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
        public static async Task<AnalyzeOperationResult> GetAnalyzeLayoutResultAsync(this IFormRecognizerClient operations, System.Guid resultId, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.GetAnalyzeLayoutResultWithHttpMessagesAsync(resultId, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }

    }
}
