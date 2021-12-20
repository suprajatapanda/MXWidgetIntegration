/*
 * MX Platform API
 *
 * The MX Platform API is a powerful, fully-featured API designed to make aggregating and enhancing financial data easy and reliable. It can seamlessly connect your app or website to tens of thousands of financial institutions.
 *
 * The version of the OpenAPI document: 0.1.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;


namespace MXWidgetIntegration.Core.Model
{
    /// <summary>
    /// ChallengeResponse
    /// </summary>
    [DataContract(Name = "ChallengeResponse")]
    public partial class ChallengeResponse : IEquatable<ChallengeResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChallengeResponse" /> class.
        /// </summary>
        /// <param name="fieldName">fieldName.</param>
        /// <param name="guid">guid.</param>
        /// <param name="imageData">imageData.</param>
        /// <param name="imageOptions">imageOptions.</param>
        /// <param name="label">label.</param>
        /// <param name="options">options.</param>
        /// <param name="type">type.</param>
        public ChallengeResponse(string fieldName = default(string), string guid = default(string), string imageData = default(string), List<ImageOptionResponse> imageOptions = default(List<ImageOptionResponse>), string label = default(string), List<OptionResponse> options = default(List<OptionResponse>), string type = default(string))
        {
            this.FieldName = fieldName;
            this.Guid = guid;
            this.ImageData = imageData;
            this.ImageOptions = imageOptions;
            this.Label = label;
            this.Options = options;
            this.Type = type;
        }

        /// <summary>
        /// Gets or Sets FieldName
        /// </summary>
        [DataMember(Name = "field_name", EmitDefaultValue = true)]
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or Sets Guid
        /// </summary>
        [DataMember(Name = "guid", EmitDefaultValue = false)]
        public string Guid { get; set; }

        /// <summary>
        /// Gets or Sets ImageData
        /// </summary>
        [DataMember(Name = "image_data", EmitDefaultValue = true)]
        public string ImageData { get; set; }

        /// <summary>
        /// Gets or Sets ImageOptions
        /// </summary>
        [DataMember(Name = "image_options", EmitDefaultValue = false)]
        public List<ImageOptionResponse> ImageOptions { get; set; }

        /// <summary>
        /// Gets or Sets Label
        /// </summary>
        [DataMember(Name = "label", EmitDefaultValue = true)]
        public string Label { get; set; }

        /// <summary>
        /// Gets or Sets Options
        /// </summary>
        [DataMember(Name = "options", EmitDefaultValue = false)]
        public List<OptionResponse> Options { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = true)]
        public string Type { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ChallengeResponse {\n");
            sb.Append("  FieldName: ").Append(FieldName).Append("\n");
            sb.Append("  Guid: ").Append(Guid).Append("\n");
            sb.Append("  ImageData: ").Append(ImageData).Append("\n");
            sb.Append("  ImageOptions: ").Append(ImageOptions).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  Options: ").Append(Options).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as ChallengeResponse);
        }

        /// <summary>
        /// Returns true if ChallengeResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of ChallengeResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ChallengeResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.FieldName == input.FieldName ||
                    (this.FieldName != null &&
                    this.FieldName.Equals(input.FieldName))
                ) && 
                (
                    this.Guid == input.Guid ||
                    (this.Guid != null &&
                    this.Guid.Equals(input.Guid))
                ) && 
                (
                    this.ImageData == input.ImageData ||
                    (this.ImageData != null &&
                    this.ImageData.Equals(input.ImageData))
                ) && 
                (
                    this.ImageOptions == input.ImageOptions ||
                    this.ImageOptions != null &&
                    input.ImageOptions != null &&
                    this.ImageOptions.SequenceEqual(input.ImageOptions)
                ) && 
                (
                    this.Label == input.Label ||
                    (this.Label != null &&
                    this.Label.Equals(input.Label))
                ) && 
                (
                    this.Options == input.Options ||
                    this.Options != null &&
                    input.Options != null &&
                    this.Options.SequenceEqual(input.Options)
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.FieldName != null)
                    hashCode = hashCode * 59 + this.FieldName.GetHashCode();
                if (this.Guid != null)
                    hashCode = hashCode * 59 + this.Guid.GetHashCode();
                if (this.ImageData != null)
                    hashCode = hashCode * 59 + this.ImageData.GetHashCode();
                if (this.ImageOptions != null)
                    hashCode = hashCode * 59 + this.ImageOptions.GetHashCode();
                if (this.Label != null)
                    hashCode = hashCode * 59 + this.Label.GetHashCode();
                if (this.Options != null)
                    hashCode = hashCode * 59 + this.Options.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}