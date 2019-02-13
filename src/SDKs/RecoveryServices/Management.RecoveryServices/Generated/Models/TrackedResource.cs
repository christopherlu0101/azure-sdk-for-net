// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.RecoveryServices.Models
{
    using Microsoft.Azure;
    using Microsoft.Azure.Management;
    using Microsoft.Azure.Management.RecoveryServices;
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Tracked resource with location.
    /// </summary>
    public partial class TrackedResource : Resource
    {
        /// <summary>
        /// Initializes a new instance of the TrackedResource class.
        /// </summary>
        public TrackedResource() { }

        /// <summary>
        /// Initializes a new instance of the TrackedResource class.
        /// </summary>
        /// <param name="location">Resource location.</param>
        /// <param name="id">Resource Id represents the complete path to the
        /// resource.</param>
        /// <param name="name">Resource name associated with the
        /// resource.</param>
        /// <param name="type">Resource type represents the complete path of
        /// the form Namespace/ResourceType/ResourceType/...</param>
        /// <param name="eTag">Optional ETag.</param>
        /// <param name="tags">Resource tags.</param>
        public TrackedResource(string location, string id = default(string), string name = default(string), string type = default(string), string eTag = default(string), IDictionary<string, string> tags = default(IDictionary<string, string>))
            : base(id, name, type, eTag)
        {
            Location = location;
            Tags = tags;
        }

        /// <summary>
        /// Gets or sets resource location.
        /// </summary>
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets resource tags.
        /// </summary>
        [JsonProperty(PropertyName = "tags")]
        public IDictionary<string, string> Tags { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Location == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Location");
            }
        }
    }
}