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
    using System.Linq;

    /// <summary>
    /// Response to the get custom model operation.
    /// </summary>
    public partial class Model
    {
        /// <summary>
        /// Initializes a new instance of the Model class.
        /// </summary>
        public Model()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Model class.
        /// </summary>
        /// <param name="modelInfo">Basic custom model information.</param>
        /// <param name="keys">Keys extracted by the custom model.</param>
        /// <param name="trainResult">Custom model training result.</param>
        public Model(ModelInfo modelInfo, KeysResult keys = default(KeysResult), TrainResult trainResult = default(TrainResult))
        {
            ModelInfo = modelInfo;
            Keys = keys;
            TrainResult = trainResult;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets basic custom model information.
        /// </summary>
        [JsonProperty(PropertyName = "modelInfo")]
        public ModelInfo ModelInfo { get; set; }

        /// <summary>
        /// Gets or sets keys extracted by the custom model.
        /// </summary>
        [JsonProperty(PropertyName = "keys")]
        public KeysResult Keys { get; set; }

        /// <summary>
        /// Gets or sets custom model training result.
        /// </summary>
        [JsonProperty(PropertyName = "trainResult")]
        public TrainResult TrainResult { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (ModelInfo == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ModelInfo");
            }
            if (ModelInfo != null)
            {
                ModelInfo.Validate();
            }
            if (Keys != null)
            {
                Keys.Validate();
            }
            if (TrainResult != null)
            {
                TrainResult.Validate();
            }
        }
    }
}
