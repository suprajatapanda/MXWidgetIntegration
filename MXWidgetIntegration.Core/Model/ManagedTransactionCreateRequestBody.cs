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
    /// ManagedTransactionCreateRequestBody
    /// </summary>
    [DataContract(Name = "ManagedTransactionCreateRequestBody")]
    public partial class ManagedTransactionCreateRequestBody : IEquatable<ManagedTransactionCreateRequestBody>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedTransactionCreateRequestBody" /> class.
        /// </summary>
        /// <param name="transaction">transaction.</param>
        public ManagedTransactionCreateRequestBody(ManagedTransactionCreateRequest transaction = default(ManagedTransactionCreateRequest))
        {
            this.Transaction = transaction;
        }

        /// <summary>
        /// Gets or Sets Transaction
        /// </summary>
        [DataMember(Name = "transaction", EmitDefaultValue = false)]
        public ManagedTransactionCreateRequest Transaction { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ManagedTransactionCreateRequestBody {\n");
            sb.Append("  Transaction: ").Append(Transaction).Append("\n");
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
            return this.Equals(input as ManagedTransactionCreateRequestBody);
        }

        /// <summary>
        /// Returns true if ManagedTransactionCreateRequestBody instances are equal
        /// </summary>
        /// <param name="input">Instance of ManagedTransactionCreateRequestBody to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ManagedTransactionCreateRequestBody input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Transaction == input.Transaction ||
                    (this.Transaction != null &&
                    this.Transaction.Equals(input.Transaction))
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
                if (this.Transaction != null)
                    hashCode = hashCode * 59 + this.Transaction.GetHashCode();
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
