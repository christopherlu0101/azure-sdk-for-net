// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Microsoft.Rest;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extracts information from forms and images into structured data.
    /// </summary>
    public partial interface IFormRecognizerClient : System.IDisposable
    {
        /// <summary>
        /// Train Custom Model
        /// </summary>
        /// <remarks>
        /// Create and train a custom model. The request must include a source
        /// parameter that is either an externally accessible Azure storage
        /// blob container Uri (preferably a Shared Access Signature Uri) or
        /// valid path to a data folder in a locally mounted drive. When local
        /// paths are specified, they must follow the Linux/Unix path format
        /// and be an absolute path rooted to the input mount configuration
        /// setting value e.g., if '{Mounts:Input}' configuration setting value
        /// is '/input' then a valid source path would be
        /// '/input/contosodataset'. All data to be trained is expected to be
        /// under the source folder or sub folders under it. Models are trained
        /// using documents that are of the following content type -
        /// 'application/pdf', 'image/jpeg', 'image/png', 'image/tiff'. Other
        /// type of content is ignored.
        /// </remarks>
        /// <param name='trainRequest'>
        /// Training request parameters.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationHeaderResponse<TrainCustomModelAsyncHeaders>> TrainCustomModelWithHttpMessagesAsync(TrainRequest trainRequest, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// List Custom Models with pagination.
        /// </summary>
        /// <remarks>
        /// Get information about all custom models
        /// </remarks>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<ModelsModel>> GetCustomModelsWithHttpMessagesAsync(string nextLink = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get summary of custom models.
        /// </summary>
        /// <remarks>
        /// Get information about all custom models.
        /// </remarks>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<ModelsModel>> GetCustomModelsSummaryWithHttpMessagesAsync(Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get Custom Model
        /// </summary>
        /// <remarks>
        /// Get detailed information about a custom model.
        /// </remarks>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='includeKeys'>
        /// Include list of extracted keys in model information.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<Model>> GetCustomModelWithHttpMessagesAsync(System.Guid modelId, bool? includeKeys = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete Custom Model
        /// </summary>
        /// <remarks>
        /// Mark model for deletion. Model artifacts will be permanently
        /// removed within a predetermined period.
        /// </remarks>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> DeleteCustomModelWithHttpMessagesAsync(System.Guid modelId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Analyze Form from stream
        /// </summary>
        /// <remarks>
        /// Extract key-value pairs, tables, and semantic values from a given
        /// document. The input document must be of one of the supported
        /// content types - 'application/pdf', 'image/jpeg', 'image/png' or
        /// 'image/tiff'. Alternatively, use 'application/json' type to specify
        /// the location (Uri or local path) of the document to be analyzed.
        /// </remarks>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='fileStream'>
        /// Stream to analyze.
        /// </param>
        /// <param name='contentType'>
        /// Content-Type of the stream.
        /// </param>
        /// <param name='includeTextDetails'>
        /// Include text lines and element references in the result.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationHeaderResponse<AnalyzeWithCustomModelHeaders>> AnalyzeWithCustomModelWithHttpMessagesAsync(System.Guid modelId, Stream fileStream, AnalysisContentType contentType, bool? includeTextDetails = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Analyze Form from URI
        /// </summary>
        /// <remarks>
        /// Extract key-value pairs, tables, and semantic values from a given
        /// document. The input document must be of one of the supported
        /// content types - 'application/pdf', 'image/jpeg', 'image/png' or
        /// 'image/tiff'. Alternatively, use 'application/json' type to specify
        /// the location (Uri or local path) of the document to be analyzed.
        /// </remarks>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='uri'>
        /// Remote URL to analyze.
        /// </param>
        /// <param name='includeTextDetails'>
        /// Include text lines and element references in the result.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationHeaderResponse<AnalyzeWithCustomModelHeaders>> AnalyzeWithCustomModelWithHttpMessagesAsync(System.Guid modelId, Uri uri, bool? includeTextDetails = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get Analyze Form Result
        /// </summary>
        /// <remarks>
        /// Obtain current status and the result of the analyze form operation.
        /// </remarks>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='resultId'>
        /// Analyze operation result identifier.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<AnalyzeOperationResult>> GetAnalyzeFormResultWithHttpMessagesAsync(System.Guid modelId, System.Guid resultId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
