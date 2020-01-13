// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Status and result of the queued analyze operation.
    /// </summary>
    public class AnalyzeOperationResult
    {
        /// <summary>
        /// Operation status.
        /// </summary>
        public OperationStatus Status { get; set; }

        /// <summary>
        /// Date and time (UTC) when the analyze operation was submitted.
        /// </summary>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Date and time (UTC) when the status was last updated.
        /// </summary>
        public DateTime LastUpdatedDateTime { get; set; }

        /// <summary>
        /// Results of the analyze operation.
        /// </summary>
        public AnalyzeResult AnalyzeResult { get; set; }
    }
}
