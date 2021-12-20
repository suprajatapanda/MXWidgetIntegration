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
    /// AccountNumberResponse
    /// </summary>
    [DataContract(Name = "AccountNumberResponse")]
    public partial class AccountNumberResponse : IEquatable<AccountNumberResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountNumberResponse" /> class.
        /// </summary>
        /// <param name="accountGuid">accountGuid.</param>
        /// <param name="accountNumber">accountNumber.</param>
        /// <param name="guid">guid.</param>
        /// <param name="institutionNumber">institutionNumber.</param>
        /// <param name="memberGuid">memberGuid.</param>
        /// <param name="routingNumber">routingNumber.</param>
        /// <param name="transitNumber">transitNumber.</param>
        /// <param name="userGuid">userGuid.</param>
        public AccountNumberResponse(string accountGuid = default(string), string accountNumber = default(string), string guid = default(string), string institutionNumber = default(string), string memberGuid = default(string), string routingNumber = default(string), string transitNumber = default(string), string userGuid = default(string))
        {
            this.AccountGuid = accountGuid;
            this.AccountNumber = accountNumber;
            this.Guid = guid;
            this.InstitutionNumber = institutionNumber;
            this.MemberGuid = memberGuid;
            this.RoutingNumber = routingNumber;
            this.TransitNumber = transitNumber;
            this.UserGuid = userGuid;
        }

        /// <summary>
        /// Gets or Sets AccountGuid
        /// </summary>
        [DataMember(Name = "account_guid", EmitDefaultValue = false)]
        public string AccountGuid { get; set; }

        /// <summary>
        /// Gets or Sets AccountNumber
        /// </summary>
        [DataMember(Name = "account_number", EmitDefaultValue = true)]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or Sets Guid
        /// </summary>
        [DataMember(Name = "guid", EmitDefaultValue = false)]
        public string Guid { get; set; }

        /// <summary>
        /// Gets or Sets InstitutionNumber
        /// </summary>
        [DataMember(Name = "institution_number", EmitDefaultValue = true)]
        public string InstitutionNumber { get; set; }

        /// <summary>
        /// Gets or Sets MemberGuid
        /// </summary>
        [DataMember(Name = "member_guid", EmitDefaultValue = false)]
        public string MemberGuid { get; set; }

        /// <summary>
        /// Gets or Sets RoutingNumber
        /// </summary>
        [DataMember(Name = "routing_number", EmitDefaultValue = true)]
        public string RoutingNumber { get; set; }

        /// <summary>
        /// Gets or Sets TransitNumber
        /// </summary>
        [DataMember(Name = "transit_number", EmitDefaultValue = true)]
        public string TransitNumber { get; set; }

        /// <summary>
        /// Gets or Sets UserGuid
        /// </summary>
        [DataMember(Name = "user_guid", EmitDefaultValue = false)]
        public string UserGuid { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AccountNumberResponse {\n");
            sb.Append("  AccountGuid: ").Append(AccountGuid).Append("\n");
            sb.Append("  AccountNumber: ").Append(AccountNumber).Append("\n");
            sb.Append("  Guid: ").Append(Guid).Append("\n");
            sb.Append("  InstitutionNumber: ").Append(InstitutionNumber).Append("\n");
            sb.Append("  MemberGuid: ").Append(MemberGuid).Append("\n");
            sb.Append("  RoutingNumber: ").Append(RoutingNumber).Append("\n");
            sb.Append("  TransitNumber: ").Append(TransitNumber).Append("\n");
            sb.Append("  UserGuid: ").Append(UserGuid).Append("\n");
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
            return this.Equals(input as AccountNumberResponse);
        }

        /// <summary>
        /// Returns true if AccountNumberResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of AccountNumberResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AccountNumberResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.AccountGuid == input.AccountGuid ||
                    (this.AccountGuid != null &&
                    this.AccountGuid.Equals(input.AccountGuid))
                ) && 
                (
                    this.AccountNumber == input.AccountNumber ||
                    (this.AccountNumber != null &&
                    this.AccountNumber.Equals(input.AccountNumber))
                ) && 
                (
                    this.Guid == input.Guid ||
                    (this.Guid != null &&
                    this.Guid.Equals(input.Guid))
                ) && 
                (
                    this.InstitutionNumber == input.InstitutionNumber ||
                    (this.InstitutionNumber != null &&
                    this.InstitutionNumber.Equals(input.InstitutionNumber))
                ) && 
                (
                    this.MemberGuid == input.MemberGuid ||
                    (this.MemberGuid != null &&
                    this.MemberGuid.Equals(input.MemberGuid))
                ) && 
                (
                    this.RoutingNumber == input.RoutingNumber ||
                    (this.RoutingNumber != null &&
                    this.RoutingNumber.Equals(input.RoutingNumber))
                ) && 
                (
                    this.TransitNumber == input.TransitNumber ||
                    (this.TransitNumber != null &&
                    this.TransitNumber.Equals(input.TransitNumber))
                ) && 
                (
                    this.UserGuid == input.UserGuid ||
                    (this.UserGuid != null &&
                    this.UserGuid.Equals(input.UserGuid))
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
                if (this.AccountGuid != null)
                    hashCode = hashCode * 59 + this.AccountGuid.GetHashCode();
                if (this.AccountNumber != null)
                    hashCode = hashCode * 59 + this.AccountNumber.GetHashCode();
                if (this.Guid != null)
                    hashCode = hashCode * 59 + this.Guid.GetHashCode();
                if (this.InstitutionNumber != null)
                    hashCode = hashCode * 59 + this.InstitutionNumber.GetHashCode();
                if (this.MemberGuid != null)
                    hashCode = hashCode * 59 + this.MemberGuid.GetHashCode();
                if (this.RoutingNumber != null)
                    hashCode = hashCode * 59 + this.RoutingNumber.GetHashCode();
                if (this.TransitNumber != null)
                    hashCode = hashCode * 59 + this.TransitNumber.GetHashCode();
                if (this.UserGuid != null)
                    hashCode = hashCode * 59 + this.UserGuid.GetHashCode();
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
