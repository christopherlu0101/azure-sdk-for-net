// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extracts information from forms and images into structured data.
    /// </summary>
    public partial class FormRecognizerClient : ServiceClient<FormRecognizerClient>, IFormRecognizerClient
    {
        public FormRecognizerClient(string apiKey, string endpoint)
        {
            if (apiKey != null)
            {
                Credentials = new FormClientCredentials(apiKey);
            }
            Endpoint = endpoint;
            Initialize();
        }

        public async Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            var resp = await AnalyzeReceiptAsyncWithHttpMessagesAsync(uri, null, null, cancellationToken).ConfigureAwait(false);
            return resp.Headers;
        }

        public async Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            var resp = await AnalyzeReceiptAsyncWithHttpMessagesAsync(fileStream as Stream, contentType.ToString(), null, cancellationToken).ConfigureAwait(false);
            return resp.Headers;
        }

        public async Task<AnalyzeOperationResult> AnalyzeReceiptAsync(Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await AnalyzeReceiptAsyncWithHttpMessagesAsync(uri as object))
            {                
                var header = _result.Headers;
                var guid = GetGuid(header.OperationLocation);
                return await PollingResultAsync(guid, AnalyzeType.Receipt, cancellationToken);                
            }
        }

        public async Task<AnalyzeOperationResult> AnalyzeReceiptAsync(Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await AnalyzeReceiptAsyncWithHttpMessagesAsync(fileStream as object, contentType.ToString()))
            {
                var header = _result.Headers;
                var guid = GetGuid(header.OperationLocation);
                return await PollingResultAsync(guid, AnalyzeType.Receipt, cancellationToken);
            }
        }

        public async Task<AnalyzeOperationResult> GetAnalyzeReceiptResultAsync(System.Guid resultId, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await GetAnalyzeReceiptResultWithHttpMessagesAsync(resultId, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }

        public async Task<AnalyzeLayoutAsyncHeaders> StartAnalyzeLayoutAsync(string language, Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            var resp = await AnalyzeLayoutAsyncWithHttpMessagesAsync(language, uri, null, null, cancellationToken).ConfigureAwait(false);
            return resp.Headers;
        }

        public async Task<AnalyzeLayoutAsyncHeaders> StartAnalyzeLayoutAsync(string language, Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            var resp = await AnalyzeLayoutAsyncWithHttpMessagesAsync(language, fileStream as Stream, contentType.ToString(), null, cancellationToken).ConfigureAwait(false);
            return resp.Headers;
        }

        public async Task<AnalyzeOperationResult> AnalyzeLayoutAsync(string language, Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await AnalyzeLayoutAsyncWithHttpMessagesAsync(language, uri as object, null, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var guid = GetGuid(header.OperationLocation);
                return await PollingResultAsync(guid, AnalyzeType.Layout, cancellationToken);
            }
        }

        public async Task<AnalyzeOperationResult> AnalyzeLayoutAsync(string language, Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await AnalyzeLayoutAsyncWithHttpMessagesAsync(language, fileStream as Stream, contentType.ToString(), null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var guid = GetGuid(header.OperationLocation);
                return await PollingResultAsync(guid, AnalyzeType.Layout, cancellationToken);
            }
        }

        public async Task<AnalyzeOperationResult> GetAnalyzeLayoutResultAsync(System.Guid resultId, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await GetAnalyzeLayoutResultWithHttpMessagesAsync(resultId, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }

        public async Task<AnalyzeOperationResult> PollingResultAsync(Guid resultid, AnalyzeType type, CancellationToken cancellationToken = default(CancellationToken))
        {
            int retryTimeframe = 1;
            while (true)
            {
                AnalyzeOperationResult body;
                switch (type)
                {
                    case (AnalyzeType.Layout):
                        body = await GetAnalyzeLayoutResultAsync(resultid, cancellationToken);
                        break;
                    case (AnalyzeType.Receipt):
                        body = await GetAnalyzeReceiptResultAsync(resultid, cancellationToken);
                        break;
                    default:
                        throw new ArgumentException("Not supported analyze type");
                }
                if (body.Status.ToSerializedValue() == "succeeded")
                {
                    return body;
                }
                await Task.Delay(TimeSpan.FromSeconds(retryTimeframe));
                retryTimeframe = (retryTimeframe >= 32) ? retryTimeframe : retryTimeframe * 2;
            }
            throw new ErrorResponseException($"Guid : {resultid.ToString()}, Timeout.");
        }


        public enum AnalyzeType { Layout, Receipt };
        public static Guid GetGuid(string uri, int order = 1)
        {
            var match = Regex.Match(uri, @"([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})");
            if (match.Success)
            {
                return new Guid(match.Groups[order].ToString());
            }
            throw new ArgumentException("Invalid URL.");
        }
    }
}
