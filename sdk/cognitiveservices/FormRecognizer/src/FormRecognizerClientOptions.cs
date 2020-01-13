// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.Core;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// FormRecognizerClient advanced options
    /// </summary>
    public class FormRecognizerClientOptions : ClientOptions
    {
        /// <summary>
        /// All FormRecognizer API version
        /// </summary>
        public enum ServiceVersion
        {
#pragma warning disable CA1707 // Identifiers should not contain underscores
            /// <summary>
            /// v2.0-preview
            /// </summary>
            v2_0_preview = 1
#pragma warning restore CA1707 // Identifiers should not contain underscores
        }

        /// <summary>
        /// Constructor for FormRecognizerClientOptions
        /// </summary>
        /// <param name="version"></param>
        public FormRecognizerClientOptions(ServiceVersion version = LatestVersion)
        {
            Version = version;
        }

        /// <summary>
        /// FormRecognizer API version current client using.
        /// </summary>
        public ServiceVersion Version { get; }

        internal const ServiceVersion LatestVersion = ServiceVersion.v2_0_preview;
        internal string GetVersionString()
        {
            switch (Version)
            {
                case ServiceVersion.v2_0_preview:
                    return "v2.0-preview";

                default:
                    throw new ArgumentException(Version.ToString());
            }
        }
    }
}
