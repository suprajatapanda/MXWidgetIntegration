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
    /// AccountsResponseBody
    /// </summary>
    [DataContract(Name = "AccountsResponseBody")]
    public partial class AccountsResponseBody : IEquatable<AccountsResponseBody>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsResponseBody" /> class.
        /// </summary>
        /// <param name="accounts">accounts.</param>
        /// <param name="pagination">pagination.</param>
        public AccountsResponseBody(List<AccountResponse> accounts = default(List<AccountResponse>), PaginationResponse pagination = default(PaginationResponse))
        {
            this.Accounts = accounts;
            this.Pagination = pagination;
        }

        /// <summary>
        /// Gets or Sets Accounts
        /// </summary>
        [DataMember(Name = "accounts", EmitDefaultValue = false)]
        public List<AccountResponse> Accounts { get; set; }

        /// <summary>
        /// Gets or Sets Pagination
        /// </summary>
        [DataMember(Name = "pagination", EmitDefaultValue = false)]
        public PaginationResponse Pagination { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AccountsResponseBody {\n");
            sb.Append("  Accounts: ").Append(Accounts).Append("\n");
            sb.Append("  Pagination: ").Append(Pagination).Append("\n");
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
            return this.Equals(input as AccountsResponseBody);
        }

        /// <summary>
        /// Returns true if AccountsResponseBody instances are equal
        /// </summary>
        /// <param name="input">Instance of AccountsResponseBody to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AccountsResponseBody input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Accounts == input.Accounts ||
                    this.Accounts != null &&
                    input.Accounts != null &&
                    this.Accounts.SequenceEqual(input.Accounts)
                ) && 
                (
                    this.Pagination == input.Pagination ||
                    (this.Pagination != null &&
                    this.Pagination.Equals(input.Pagination))
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
                if (this.Accounts != null)
                    hashCode = hashCode * 59 + this.Accounts.GetHashCode();
                if (this.Pagination != null)
                    hashCode = hashCode * 59 + this.Pagination.GetHashCode();
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
