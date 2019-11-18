// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;
    using System;

    /// <summary>
    /// Extension methods for FormRecognizerClient.
    /// </summary>
    public static partial class FormRecognizerClientExtensions
    {
        public static Guid GetOperationId(string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var parts = uri.Trim(new[] { '/' }).Split('/');
            if (parts.Length == 0)
            {
                throw new ArgumentException("Invalid Operation URL.");
            }

            Guid operationId;
            var operationIdText = parts[parts.Length - 1];
            if (!Guid.TryParse(operationIdText, out operationId))
            {
                throw new ArgumentException("Invalid Operation URL.");
            }

            return operationId;
        }

        private static async Task<AnalyzeOperationResult> WaitForOperation(this IFormRecognizerClient operations, Func<CancellationToken, Task<AnalyzeOperationResult>> resultFunc, CancellationToken cancellationToken = default(CancellationToken))
        {
            AnalyzeOperationResult result = null;
            do
            {
                cancellationToken.ThrowIfCancellationRequested();
                result = await resultFunc(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                if (result.Status == OperationStatus.NotStarted || result.Status == OperationStatus.Running)
                {
                    await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
                }
            }
            while (result.Status == OperationStatus.NotStarted || result.Status == OperationStatus.Running);

            if (result == null || result.Status != OperationStatus.Succeeded)
            {
                var status = result == null ? "Unknown Error" : result.Status.ToString();
                throw new ErrorResponseException(status);
            }

            return result;
        }

        private static async Task<Model> WaitForTraining(this IFormRecognizerClient operations, Func<CancellationToken, Task<Model>> resultFunc, CancellationToken cancellationToken = default(CancellationToken))
        {
            Model result = null;
            do
            {
                cancellationToken.ThrowIfCancellationRequested();
                result = await resultFunc(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                if (result.ModelInfo.Status == ModelStatus.Creating)
                {
                    await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
                }
            }
            while (result.ModelInfo.Status == ModelStatus.Creating);

            if (result == null || result.ModelInfo.Status != ModelStatus.Ready)
            {
                var status = result == null ? "Unknown Error" : result.ModelInfo.Status.ToString();
                throw new ErrorResponseException(status);
            }

            return result;
        }
    }
}
