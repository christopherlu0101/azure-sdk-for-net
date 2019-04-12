// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Search.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Abstract base class for describing any cognitive service resource
    /// attached to the skillset.
    /// </summary>
    public partial class CognitiveServices
    {
        /// <summary>
        /// Initializes a new instance of the CognitiveServices class.
        /// </summary>
        public CognitiveServices()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the CognitiveServices class.
        /// </summary>
        public CognitiveServices(string description = default(string))
        {
            Description = description;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

    }
}