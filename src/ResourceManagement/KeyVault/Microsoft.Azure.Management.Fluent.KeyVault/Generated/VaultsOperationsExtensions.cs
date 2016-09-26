// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.KeyVault
{
    using System.Threading.Tasks;
   using Microsoft.Rest.Azure;
   using Models;

    /// <summary>
    /// Extension methods for VaultsOperations.
    /// </summary>
    public static partial class VaultsOperationsExtensions
    {
            /// <summary>
            /// Create or update a key vault in the specified subscription.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the Resource Group to which the server belongs.
            /// </param>
            /// <param name='vaultName'>
            /// Name of the vault
            /// </param>
            /// <param name='parameters'>
            /// Parameters to create or update the vault
            /// </param>
            public static VaultInner CreateOrUpdate(this IVaultsOperations operations, string resourceGroupName, string vaultName, VaultCreateOrUpdateParametersInner parameters)
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((IVaultsOperations)s).CreateOrUpdateAsync(resourceGroupName, vaultName, parameters), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Create or update a key vault in the specified subscription.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the Resource Group to which the server belongs.
            /// </param>
            /// <param name='vaultName'>
            /// Name of the vault
            /// </param>
            /// <param name='parameters'>
            /// Parameters to create or update the vault
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<VaultInner> CreateOrUpdateAsync(this IVaultsOperations operations, string resourceGroupName, string vaultName, VaultCreateOrUpdateParametersInner parameters, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.CreateOrUpdateWithHttpMessagesAsync(resourceGroupName, vaultName, parameters, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Deletes the specified Azure key vault.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the Resource Group to which the vault belongs.
            /// </param>
            /// <param name='vaultName'>
            /// The name of the vault to delete
            /// </param>
            public static void Delete(this IVaultsOperations operations, string resourceGroupName, string vaultName)
            {
                System.Threading.Tasks.Task.Factory.StartNew(s => ((IVaultsOperations)s).DeleteAsync(resourceGroupName, vaultName), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None,  System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Deletes the specified Azure key vault.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the Resource Group to which the vault belongs.
            /// </param>
            /// <param name='vaultName'>
            /// The name of the vault to delete
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task DeleteAsync(this IVaultsOperations operations, string resourceGroupName, string vaultName, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                await operations.DeleteWithHttpMessagesAsync(resourceGroupName, vaultName, null, cancellationToken).ConfigureAwait(false);
            }

            /// <summary>
            /// Gets the specified Azure key vault.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the Resource Group to which the vault belongs.
            /// </param>
            /// <param name='vaultName'>
            /// The name of the vault.
            /// </param>
            public static VaultInner Get(this IVaultsOperations operations, string resourceGroupName, string vaultName)
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((IVaultsOperations)s).GetAsync(resourceGroupName, vaultName), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Gets the specified Azure key vault.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the Resource Group to which the vault belongs.
            /// </param>
            /// <param name='vaultName'>
            /// The name of the vault.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<VaultInner> GetAsync(this IVaultsOperations operations, string resourceGroupName, string vaultName, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(resourceGroupName, vaultName, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// The List operation gets information about the vaults associated with the
            /// subscription and within the specified resource group.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the Resource Group to which the vault belongs.
            /// </param>
            /// <param name='top'>
            /// Maximum number of results to return.
            /// </param>
            public static Microsoft.Rest.Azure.IPage<VaultInner> ListByResourceGroup(this IVaultsOperations operations, string resourceGroupName, int? top = default(int?))
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((IVaultsOperations)s).ListByResourceGroupAsync(resourceGroupName, top), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The List operation gets information about the vaults associated with the
            /// subscription and within the specified resource group.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='resourceGroupName'>
            /// The name of the Resource Group to which the vault belongs.
            /// </param>
            /// <param name='top'>
            /// Maximum number of results to return.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Microsoft.Rest.Azure.IPage<VaultInner>> ListByResourceGroupAsync(this IVaultsOperations operations, string resourceGroupName, int? top = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ListByResourceGroupWithHttpMessagesAsync(resourceGroupName, top, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// The List operation gets information about the vaults associated with the
            /// subscription.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='top'>
            /// Maximum number of results to return.
            /// </param>
            public static Microsoft.Rest.Azure.IPage<VaultInner> List(this IVaultsOperations operations, int? top = default(int?))
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((IVaultsOperations)s).ListAsync(top), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The List operation gets information about the vaults associated with the
            /// subscription.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='top'>
            /// Maximum number of results to return.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Microsoft.Rest.Azure.IPage<VaultInner>> ListAsync(this IVaultsOperations operations, int? top = default(int?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ListWithHttpMessagesAsync(top, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// The List operation gets information about the vaults associated with the
            /// subscription and within the specified resource group.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nextPageLink'>
            /// The NextLink from the previous successful call to List operation.
            /// </param>
            public static Microsoft.Rest.Azure.IPage<VaultInner> ListByResourceGroupNext(this IVaultsOperations operations, string nextPageLink)
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((IVaultsOperations)s).ListByResourceGroupNextAsync(nextPageLink), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The List operation gets information about the vaults associated with the
            /// subscription and within the specified resource group.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nextPageLink'>
            /// The NextLink from the previous successful call to List operation.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Microsoft.Rest.Azure.IPage<VaultInner>> ListByResourceGroupNextAsync(this IVaultsOperations operations, string nextPageLink, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ListByResourceGroupNextWithHttpMessagesAsync(nextPageLink, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// The List operation gets information about the vaults associated with the
            /// subscription.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nextPageLink'>
            /// The NextLink from the previous successful call to List operation.
            /// </param>
            public static Microsoft.Rest.Azure.IPage<VaultInner> ListNext(this IVaultsOperations operations, string nextPageLink)
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((IVaultsOperations)s).ListNextAsync(nextPageLink), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// The List operation gets information about the vaults associated with the
            /// subscription.
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nextPageLink'>
            /// The NextLink from the previous successful call to List operation.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Microsoft.Rest.Azure.IPage<VaultInner>> ListNextAsync(this IVaultsOperations operations, string nextPageLink, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ListNextWithHttpMessagesAsync(nextPageLink, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
