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
    /// HoldingResponseBody
    /// </summary>
    [DataContract(Name = "HoldingResponseBody")]
    public partial class HoldingResponseBody : IEquatable<HoldingResponseBody>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HoldingResponseBody" /> class.
        /// </summary>
        /// <param name="holding">holding.</param>
        public HoldingResponseBody(HoldingResponse holding = default(HoldingResponse))
        {
            this.Holding = holding;
        }

        /// <summary>
        /// Gets or Sets Holding
        /// </summary>
        [DataMember(Name = "holding", EmitDefaultValue = false)]
        public HoldingResponse Holding { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class HoldingResponseBody {\n");
            sb.Append("  Holding: ").Append(Holding).Append("\n");
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
            return this.Equals(input as HoldingResponseBody);
        }

        /// <summary>
        /// Returns true if HoldingResponseBody instances are equal
        /// </summary>
        /// <param name="input">Instance of HoldingResponseBody to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(HoldingResponseBody input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Holding == input.Holding ||
                    (this.Holding != null &&
                    this.Holding.Equals(input.Holding))
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
                if (this.Holding != null)
                    hashCode = hashCode * 59 + this.Holding.GetHashCode();
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
