// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.RecoveryServices.SiteRecovery
{
    using Microsoft.Rest;
    using Microsoft.Rest.Azure;
    using Microsoft.Rest.Serialization;
    using Models;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    public partial class SiteRecoveryManagementClient : ServiceClient<SiteRecoveryManagementClient>, ISiteRecoveryManagementClient, IAzureClient
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        public System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        public JsonSerializerSettings SerializationSettings { get; private set; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        public JsonSerializerSettings DeserializationSettings { get; private set; }

        /// <summary>
        /// Credentials needed for the client to connect to Azure.
        /// </summary>
        public ServiceClientCredentials Credentials { get; private set; }

        /// <summary>
        /// The subscription Id.
        /// </summary>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The name of the resource group where the recovery services vault is
        /// present.
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// The name of the recovery services vault.
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Client Api Version.
        /// </summary>
        public string ApiVersion { get; private set; }

        /// <summary>
        /// Gets or sets the preferred language for the response.
        /// </summary>
        public string AcceptLanguage { get; set; }

        /// <summary>
        /// Gets or sets the retry timeout in seconds for Long Running Operations.
        /// Default value is 30.
        /// </summary>
        public int? LongRunningOperationRetryTimeout { get; set; }

        /// <summary>
        /// When set to true a unique x-ms-client-request-id value is generated and
        /// included in each request. Default is true.
        /// </summary>
        public bool? GenerateClientRequestId { get; set; }

        /// <summary>
        /// Gets the IOperations.
        /// </summary>
        public virtual IOperations Operations { get; private set; }

        /// <summary>
        /// Gets the IReplicationAlertSettingsOperations.
        /// </summary>
        public virtual IReplicationAlertSettingsOperations ReplicationAlertSettings { get; private set; }

        /// <summary>
        /// Gets the IReplicationEventsOperations.
        /// </summary>
        public virtual IReplicationEventsOperations ReplicationEvents { get; private set; }

        /// <summary>
        /// Gets the IReplicationFabricsOperations.
        /// </summary>
        public virtual IReplicationFabricsOperations ReplicationFabrics { get; private set; }

        /// <summary>
        /// Gets the IReplicationLogicalNetworksOperations.
        /// </summary>
        public virtual IReplicationLogicalNetworksOperations ReplicationLogicalNetworks { get; private set; }

        /// <summary>
        /// Gets the IReplicationNetworksOperations.
        /// </summary>
        public virtual IReplicationNetworksOperations ReplicationNetworks { get; private set; }

        /// <summary>
        /// Gets the IReplicationNetworkMappingsOperations.
        /// </summary>
        public virtual IReplicationNetworkMappingsOperations ReplicationNetworkMappings { get; private set; }

        /// <summary>
        /// Gets the IReplicationProtectionContainersOperations.
        /// </summary>
        public virtual IReplicationProtectionContainersOperations ReplicationProtectionContainers { get; private set; }

        /// <summary>
        /// Gets the IReplicationProtectableItemsOperations.
        /// </summary>
        public virtual IReplicationProtectableItemsOperations ReplicationProtectableItems { get; private set; }

        /// <summary>
        /// Gets the IReplicationProtectedItemsOperations.
        /// </summary>
        public virtual IReplicationProtectedItemsOperations ReplicationProtectedItems { get; private set; }

        /// <summary>
        /// Gets the IRecoveryPointsOperations.
        /// </summary>
        public virtual IRecoveryPointsOperations RecoveryPoints { get; private set; }

        /// <summary>
        /// Gets the ITargetComputeSizesOperations.
        /// </summary>
        public virtual ITargetComputeSizesOperations TargetComputeSizes { get; private set; }

        /// <summary>
        /// Gets the IReplicationProtectionContainerMappingsOperations.
        /// </summary>
        public virtual IReplicationProtectionContainerMappingsOperations ReplicationProtectionContainerMappings { get; private set; }

        /// <summary>
        /// Gets the IReplicationRecoveryServicesProvidersOperations.
        /// </summary>
        public virtual IReplicationRecoveryServicesProvidersOperations ReplicationRecoveryServicesProviders { get; private set; }

        /// <summary>
        /// Gets the IReplicationStorageClassificationsOperations.
        /// </summary>
        public virtual IReplicationStorageClassificationsOperations ReplicationStorageClassifications { get; private set; }

        /// <summary>
        /// Gets the IReplicationStorageClassificationMappingsOperations.
        /// </summary>
        public virtual IReplicationStorageClassificationMappingsOperations ReplicationStorageClassificationMappings { get; private set; }

        /// <summary>
        /// Gets the IReplicationvCentersOperations.
        /// </summary>
        public virtual IReplicationvCentersOperations ReplicationvCenters { get; private set; }

        /// <summary>
        /// Gets the IReplicationJobsOperations.
        /// </summary>
        public virtual IReplicationJobsOperations ReplicationJobs { get; private set; }

        /// <summary>
        /// Gets the IReplicationPoliciesOperations.
        /// </summary>
        public virtual IReplicationPoliciesOperations ReplicationPolicies { get; private set; }

        /// <summary>
        /// Gets the IReplicationRecoveryPlansOperations.
        /// </summary>
        public virtual IReplicationRecoveryPlansOperations ReplicationRecoveryPlans { get; private set; }

        /// <summary>
        /// Gets the IReplicationVaultHealthOperations.
        /// </summary>
        public virtual IReplicationVaultHealthOperations ReplicationVaultHealth { get; private set; }

        /// <summary>
        /// Initializes a new instance of the SiteRecoveryManagementClient class.
        /// </summary>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        protected SiteRecoveryManagementClient(params DelegatingHandler[] handlers) : base(handlers)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the SiteRecoveryManagementClient class.
        /// </summary>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        protected SiteRecoveryManagementClient(HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : base(rootHandler, handlers)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the SiteRecoveryManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        protected SiteRecoveryManagementClient(System.Uri baseUri, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            BaseUri = baseUri;
        }

        /// <summary>
        /// Initializes a new instance of the SiteRecoveryManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        protected SiteRecoveryManagementClient(System.Uri baseUri, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            BaseUri = baseUri;
        }

        /// <summary>
        /// Initializes a new instance of the SiteRecoveryManagementClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public SiteRecoveryManagementClient(ServiceClientCredentials credentials, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the SiteRecoveryManagementClient class.
        /// </summary>
        /// <param name='credentials'>
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public SiteRecoveryManagementClient(ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the SiteRecoveryManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='credentials'>
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public SiteRecoveryManagementClient(System.Uri baseUri, ServiceClientCredentials credentials, params DelegatingHandler[] handlers) : this(handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            BaseUri = baseUri;
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the SiteRecoveryManagementClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='credentials'>
        /// Required. Credentials needed for the client to connect to Azure.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public SiteRecoveryManagementClient(System.Uri baseUri, ServiceClientCredentials credentials, HttpClientHandler rootHandler, params DelegatingHandler[] handlers) : this(rootHandler, handlers)
        {
            if (baseUri == null)
            {
                throw new System.ArgumentNullException("baseUri");
            }
            if (credentials == null)
            {
                throw new System.ArgumentNullException("credentials");
            }
            BaseUri = baseUri;
            Credentials = credentials;
            if (Credentials != null)
            {
                Credentials.InitializeServiceClient(this);
            }
        }

        /// <summary>
        /// An optional partial-method to perform custom initialization.
        /// </summary>
        partial void CustomInitialize();
        /// <summary>
        /// Initializes client properties.
        /// </summary>
        private void Initialize()
        {
            Operations = new Operations(this);
            ReplicationAlertSettings = new ReplicationAlertSettingsOperations(this);
            ReplicationEvents = new ReplicationEventsOperations(this);
            ReplicationFabrics = new ReplicationFabricsOperations(this);
            ReplicationLogicalNetworks = new ReplicationLogicalNetworksOperations(this);
            ReplicationNetworks = new ReplicationNetworksOperations(this);
            ReplicationNetworkMappings = new ReplicationNetworkMappingsOperations(this);
            ReplicationProtectionContainers = new ReplicationProtectionContainersOperations(this);
            ReplicationProtectableItems = new ReplicationProtectableItemsOperations(this);
            ReplicationProtectedItems = new ReplicationProtectedItemsOperations(this);
            RecoveryPoints = new RecoveryPointsOperations(this);
            TargetComputeSizes = new TargetComputeSizesOperations(this);
            ReplicationProtectionContainerMappings = new ReplicationProtectionContainerMappingsOperations(this);
            ReplicationRecoveryServicesProviders = new ReplicationRecoveryServicesProvidersOperations(this);
            ReplicationStorageClassifications = new ReplicationStorageClassificationsOperations(this);
            ReplicationStorageClassificationMappings = new ReplicationStorageClassificationMappingsOperations(this);
            ReplicationvCenters = new ReplicationvCentersOperations(this);
            ReplicationJobs = new ReplicationJobsOperations(this);
            ReplicationPolicies = new ReplicationPoliciesOperations(this);
            ReplicationRecoveryPlans = new ReplicationRecoveryPlansOperations(this);
            ReplicationVaultHealth = new ReplicationVaultHealthOperations(this);
            BaseUri = new System.Uri("https://management.azure.com");
            ApiVersion = "2018-01-10";
            AcceptLanguage = "en-US";
            LongRunningOperationRetryTimeout = 30;
            GenerateClientRequestId = true;
            SerializationSettings = new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            DeserializationSettings = new JsonSerializerSettings
            {
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter>
                    {
                        new Iso8601TimeSpanConverter()
                    }
            };
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<ApplyRecoveryPointProviderSpecificInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<ApplyRecoveryPointProviderSpecificInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<TaskTypeDetails>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<TaskTypeDetails>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<GroupTaskDetails>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<GroupTaskDetails>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<ConfigurationSettings>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<ConfigurationSettings>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<FabricSpecificCreateNetworkMappingInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<FabricSpecificCreateNetworkMappingInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<PolicyProviderSpecificInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<PolicyProviderSpecificInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<ReplicationProviderSpecificContainerCreationInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<ReplicationProviderSpecificContainerCreationInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<ReplicationProviderSpecificContainerMappingInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<ReplicationProviderSpecificContainerMappingInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<RecoveryPlanActionDetails>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<RecoveryPlanActionDetails>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<DisableProtectionProviderSpecificInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<DisableProtectionProviderSpecificInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<EnableProtectionProviderSpecificInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<EnableProtectionProviderSpecificInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<EventProviderSpecificDetails>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<EventProviderSpecificDetails>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<EventSpecificDetails>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<EventSpecificDetails>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<FabricSpecificDetails>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<FabricSpecificDetails>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<FabricSpecificCreationInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<FabricSpecificCreationInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<FabricSpecificUpdateNetworkMappingInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<FabricSpecificUpdateNetworkMappingInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<JobDetails>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<JobDetails>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<NetworkMappingFabricSpecificSettings>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<NetworkMappingFabricSpecificSettings>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<ProviderSpecificFailoverInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<ProviderSpecificFailoverInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<PolicyProviderSpecificDetails>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<PolicyProviderSpecificDetails>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<ProtectionContainerMappingProviderSpecificDetails>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<ProtectionContainerMappingProviderSpecificDetails>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<ProviderSpecificRecoveryPointDetails>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<ProviderSpecificRecoveryPointDetails>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<RecoveryPlanProviderSpecificFailoverInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<RecoveryPlanProviderSpecificFailoverInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<ReplicationProviderSpecificSettings>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<ReplicationProviderSpecificSettings>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<ReplicationProviderSpecificUpdateContainerMappingInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<ReplicationProviderSpecificUpdateContainerMappingInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<ReverseReplicationProviderSpecificInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<ReverseReplicationProviderSpecificInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<SwitchProtectionProviderSpecificInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<SwitchProtectionProviderSpecificInput>("instanceType"));
            SerializationSettings.Converters.Add(new PolymorphicSerializeJsonConverter<UpdateReplicationProtectedItemProviderInput>("instanceType"));
            DeserializationSettings.Converters.Add(new PolymorphicDeserializeJsonConverter<UpdateReplicationProtectedItemProviderInput>("instanceType"));
            CustomInitialize();
            DeserializationSettings.Converters.Add(new CloudErrorJsonConverter());
        }
    }
}