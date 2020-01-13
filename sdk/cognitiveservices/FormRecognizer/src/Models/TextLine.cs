// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// An object representing an extracted text line.
    /// </summary>
    public class TextLine
    {
        /// <summary>
        /// The text content of the line.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Bounding box of an extracted line.
        /// </summary>
        public IList<double> BoundingBox { get; set; }

        /// <summary>
        /// The detected language of this line, if different from the overall page language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// List of words in the text line.
        /// </summary>
        public IList<TextWord> Words { get; set; }
    }
}
