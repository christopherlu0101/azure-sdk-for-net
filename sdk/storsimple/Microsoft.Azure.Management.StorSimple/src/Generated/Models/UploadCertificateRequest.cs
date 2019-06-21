// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.StorSimple1200Series.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Upload Certificate Request to IDM
    /// </summary>
    [Rest.Serialization.JsonTransformation]
    public partial class UploadCertificateRequest
    {
        /// <summary>
        /// Initializes a new instance of the UploadCertificateRequest class.
        /// </summary>
        public UploadCertificateRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the UploadCertificateRequest class.
        /// </summary>
        /// <param name="certificate">Gets or sets the base64 encoded
        /// certificate raw data string</param>
        /// <param name="authType">Specify the Authentication type. Possible
        /// values include: 'Invalid', 'AccessControlService',
        /// 'AzureActiveDirectory'</param>
        /// <param name="contractVersion">Gets ContractVersion. Possible values
        /// include: 'InvalidVersion', 'V2011_09', 'V2012_02', 'V2012_05',
        /// 'V2012_12', 'V2013_04', 'V2013_10', 'V2013_11', 'V2014_04',
        /// 'V2014_06', 'V2014_07', 'V2014_09', 'V2014_10', 'V2014_12',
        /// 'V2015_01', 'V2015_02', 'V2015_04', 'V2015_05', 'V2015_06',
        /// 'V2015_07', 'V2015_08', 'V2015_10', 'V2015_12', 'V2016_01',
        /// 'V2016_02', 'V2016_04', 'V2016_05', 'V2016_07', 'V2016_08'</param>
        public UploadCertificateRequest(string certificate, AuthType? authType = default(AuthType?), ContractVersions? contractVersion = default(ContractVersions?))
        {
            AuthType = authType;
            Certificate = certificate;
            ContractVersion = contractVersion;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets specify the Authentication type. Possible values
        /// include: 'Invalid', 'AccessControlService', 'AzureActiveDirectory'
        /// </summary>
        [JsonProperty(PropertyName = "properties.authType")]
        public AuthType? AuthType { get; set; }

        /// <summary>
        /// Gets or sets the base64 encoded certificate raw data string
        /// </summary>
        [JsonProperty(PropertyName = "properties.certificate")]
        public string Certificate { get; set; }

        /// <summary>
        /// Gets ContractVersion. Possible values include: 'InvalidVersion',
        /// 'V2011_09', 'V2012_02', 'V2012_05', 'V2012_12', 'V2013_04',
        /// 'V2013_10', 'V2013_11', 'V2014_04', 'V2014_06', 'V2014_07',
        /// 'V2014_09', 'V2014_10', 'V2014_12', 'V2015_01', 'V2015_02',
        /// 'V2015_04', 'V2015_05', 'V2015_06', 'V2015_07', 'V2015_08',
        /// 'V2015_10', 'V2015_12', 'V2016_01', 'V2016_02', 'V2016_04',
        /// 'V2016_05', 'V2016_07', 'V2016_08'
        /// </summary>
        [JsonProperty(PropertyName = "contractVersion")]
        public ContractVersions? ContractVersion { get; private set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Certificate == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Certificate");
            }
        }
    }
}