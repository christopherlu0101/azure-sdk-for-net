// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Recognized field value.
    /// </summary>
    [JsonConverter(typeof(FieldValueConverter))]
    public class FieldValue
    {
        /// <summary>
        /// Type of field value.
        /// </summary>
        public FieldValueType Type { get; set; }

        /// <summary>
        /// String value.
        /// </summary>
        [SetFieldValueType(FieldValueType.String)]
        public string ValueString { get; set; }

        /// <summary>
        /// Data value.
        /// </summary>
        [SetFieldValueType(FieldValueType.Date)]
        public string ValueDate { get; set; }

        /// <summary>
        /// Time value.
        /// </summary>
        [SetFieldValueType(FieldValueType.Time)]
        public string ValueTime { get; set; }

        /// <summary>
        /// Phone number value.
        /// </summary>
        [SetFieldValueType(FieldValueType.PhoneNumber)]
        public string ValuePhoneNumber { get; set; }

        /// <summary>
        /// Floating point value.
        /// </summary>
        [SetFieldValueType(FieldValueType.Number)]
        public double ValueNumber { get; set; }

        /// <summary>
        /// Integer value.
        /// </summary>
        [SetFieldValueType(FieldValueType.Integer)]
        public int ValueInteger { get; set; }

        /// <summary>
        /// Array of field values.
        /// </summary>
        [SetFieldValueType(FieldValueType.Array)]
        public IList<FieldValue> ValueArray { get; set; }

        /// <summary>
        /// Dictionary of named field values.
        /// </summary>
        [SetFieldValueType(FieldValueType.Object)]
        public IDictionary<string, FieldValue> ValueObject { get; set; }

        /// <summary>
        /// Text content of the extracted field.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Bounding box of the field value, if appropriate.
        /// </summary>
        public IList<double> BoundingBox { get; set; }

        /// <summary>
        /// Confidence score.
        /// </summary>
        public double? Confidence { get; set; }

        /// <summary>
        /// When includeTextDetails is set to true, a list of references to the text elements constituting this field.
        /// </summary>
        public IList<string> Elements { get; set; }

        /// <summary>
        /// The 1-based page number in the input document.
        /// </summary>
        [IgnoreDefault(default(int))]
        public int Page { get; set; }
    }
}
