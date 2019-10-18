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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An object representing a word.
    /// </summary>
    public partial class TextWord
    {
        /// <summary>
        /// Initializes a new instance of the TextWord class.
        /// </summary>
        public TextWord()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TextWord class.
        /// </summary>
        /// <param name="text">The text content of the word.</param>
        /// <param name="boundingBox">Bounding box of an extracted
        /// word.</param>
        /// <param name="confidence">Confidence value.</param>
        public TextWord(string text, IList<double> boundingBox, double confidence)
        {
            Text = text;
            BoundingBox = boundingBox;
            Confidence = confidence;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the text content of the word.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets bounding box of an extracted word.
        /// </summary>
        [JsonProperty(PropertyName = "boundingBox")]
        public IList<double> BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets confidence value.
        /// </summary>
        [JsonProperty(PropertyName = "confidence")]
        public double Confidence { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Text == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Text");
            }
            if (BoundingBox == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "BoundingBox");
            }
        }
    }
}
