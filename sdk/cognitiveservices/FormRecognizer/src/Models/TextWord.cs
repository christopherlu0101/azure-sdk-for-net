// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// An object representing a word.
    /// </summary>
    public class TextWord
    {
        /// <summary>
        /// The text content of the word.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Bounding box of an extracted word.
        /// </summary>
        public IList<double> BoundingBox { get; set; }

        /// <summary>
        /// Confidence value.
        /// </summary>
        public double Confidence { get; set; }
    }
}
