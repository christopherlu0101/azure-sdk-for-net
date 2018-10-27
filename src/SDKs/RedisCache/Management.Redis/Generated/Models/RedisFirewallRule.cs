// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Redis.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// A firewall rule on a redis cache has a name, and describes a contiguous
    /// range of IP addresses permitted to connect
    /// </summary>
    [Rest.Serialization.JsonTransformation]
    public partial class RedisFirewallRule : ProxyResource
    {
        /// <summary>
        /// Initializes a new instance of the RedisFirewallRule class.
        /// </summary>
        public RedisFirewallRule()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the RedisFirewallRule class.
        /// </summary>
        /// <param name="startIP">lowest IP address included in the
        /// range</param>
        /// <param name="endIP">highest IP address included in the
        /// range</param>
        /// <param name="id">Resource ID.</param>
        /// <param name="name">Resource name.</param>
        /// <param name="type">Resource type.</param>
        public RedisFirewallRule(string startIP, string endIP, string id = default(string), string name = default(string), string type = default(string))
            : base(id, name, type)
        {
            StartIP = startIP;
            EndIP = endIP;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets lowest IP address included in the range
        /// </summary>
        [JsonProperty(PropertyName = "properties.startIP")]
        public string StartIP { get; set; }

        /// <summary>
        /// Gets or sets highest IP address included in the range
        /// </summary>
        [JsonProperty(PropertyName = "properties.endIP")]
        public string EndIP { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (StartIP == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "StartIP");
            }
            if (EndIP == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "EndIP");
            }
        }
    }
}