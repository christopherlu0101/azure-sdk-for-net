// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.ApiManagement.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Diagnostic settings for incoming/outgoing HTTP messages to the Gateway.
    /// </summary>
    public partial class PipelineDiagnosticSettings
    {
        /// <summary>
        /// Initializes a new instance of the PipelineDiagnosticSettings class.
        /// </summary>
        public PipelineDiagnosticSettings()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PipelineDiagnosticSettings class.
        /// </summary>
        /// <param name="request">Diagnostic settings for request.</param>
        /// <param name="response">Diagnostic settings for response.</param>
        public PipelineDiagnosticSettings(HttpMessageDiagnostic request = default(HttpMessageDiagnostic), HttpMessageDiagnostic response = default(HttpMessageDiagnostic))
        {
            Request = request;
            Response = response;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets diagnostic settings for request.
        /// </summary>
        [JsonProperty(PropertyName = "request")]
        public HttpMessageDiagnostic Request { get; set; }

        /// <summary>
        /// Gets or sets diagnostic settings for response.
        /// </summary>
        [JsonProperty(PropertyName = "response")]
        public HttpMessageDiagnostic Response { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Request != null)
            {
                Request.Validate();
            }
            if (Response != null)
            {
                Response.Validate();
            }
        }
    }
}
