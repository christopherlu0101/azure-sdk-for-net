// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.KeyVault.Models
{
    using System.Linq;

    /// <summary>
    /// Resource information with extended details.
    /// </summary>
    public partial class VaultInner : Microsoft.Rest.Azure.Resource
    {
        /// <summary>
        /// Initializes a new instance of the VaultInner class.
        /// </summary>
        public VaultInner()
        {
            Properties = new VaultProperties();
        }

        /// <summary>
        /// Initializes a new instance of the VaultInner class.
        /// </summary>
        /// <param name="properties">Properties of the vault</param>
        public VaultInner(VaultProperties properties, string location = default(string), string id = default(string), string name = default(string), string type = default(string), System.Collections.Generic.IDictionary<string, string> tags = default(System.Collections.Generic.IDictionary<string, string>))
            : base(location, id, name, type, tags)
        {
            Properties = new VaultProperties();
            Properties = properties;
        }

        /// <summary>
        /// Gets or sets properties of the vault
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "properties")]
        public VaultProperties Properties { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Properties == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "Properties");
            }
            if (this.Properties != null)
            {
                this.Properties.Validate();
            }
        }
    }
}
