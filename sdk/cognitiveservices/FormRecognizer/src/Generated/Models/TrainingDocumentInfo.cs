// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Report for a custom model training document.
    /// </summary>
    public partial class TrainingDocumentInfo
    {
        /// <summary>
        /// Initializes a new instance of the TrainingDocumentInfo class.
        /// </summary>
        public TrainingDocumentInfo()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TrainingDocumentInfo class.
        /// </summary>
        /// <param name="documentName">Training document name.</param>
        /// <param name="pages">Total number of pages trained.</param>
        /// <param name="errors">List of errors.</param>
        /// <param name="status">Status of the training operation. Possible
        /// values include: 'succeeded', 'partiallySucceeded', 'failed'</param>
        public TrainingDocumentInfo(string documentName, int pages, IList<ErrorInformation> errors, TrainStatus status)
        {
            DocumentName = documentName;
            Pages = pages;
            Errors = errors;
            Status = status;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets training document name.
        /// </summary>
        [JsonProperty(PropertyName = "documentName")]
        public string DocumentName { get; set; }

        /// <summary>
        /// Gets or sets total number of pages trained.
        /// </summary>
        [JsonProperty(PropertyName = "pages")]
        public int Pages { get; set; }

        /// <summary>
        /// Gets or sets list of errors.
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public IList<ErrorInformation> Errors { get; set; }

        /// <summary>
        /// Gets or sets status of the training operation. Possible values
        /// include: 'succeeded', 'partiallySucceeded', 'failed'
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public TrainStatus Status { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (DocumentName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "DocumentName");
            }
            if (Errors == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Errors");
            }
        }
    }
}
