// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Microsoft.Rest;
    using Models;
    using System.IO;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Customized interface
    /// </summary>
    public partial interface IFormRecognizerClient : System.IDisposable
    {
        Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(Uri uri, CancellationToken cancellationToken = default(CancellationToken));
        Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken));
        Task<AnalyzeOperationResult> AnalyzeReceiptAsync(Uri uri, CancellationToken cancellationToken = default(CancellationToken));
        Task<AnalyzeOperationResult> AnalyzeReceiptAsync(Stream fileStream, AnalysisContentType contentTyep, CancellationToken cancellationToken = default(CancellationToken));        
        Task<AnalyzeOperationResult> GetAnalyzeReceiptResultAsync(System.Guid resultId, CancellationToken cancellationToken = default(CancellationToken));

        Task<AnalyzeLayoutAsyncHeaders> StartAnalyzeLayoutAsync(string language, Uri uri, CancellationToken cancellationToken = default(CancellationToken));
        Task<AnalyzeLayoutAsyncHeaders> StartAnalyzeLayoutAsync(string language, Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken));
        Task<AnalyzeOperationResult> AnalyzeLayoutAsync(string language, Uri uri, CancellationToken cancellationToken = default(CancellationToken));
        Task<AnalyzeOperationResult> AnalyzeLayoutAsync(string language, Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken));
        Task<AnalyzeOperationResult> GetAnalyzeLayoutResultAsync(System.Guid resultId, CancellationToken cancellationToken = default(CancellationToken));



    }
}
