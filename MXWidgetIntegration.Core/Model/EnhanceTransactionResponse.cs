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
    /// EnhanceTransactionResponse
    /// </summary>
    [DataContract(Name = "EnhanceTransactionResponse")]
    public partial class EnhanceTransactionResponse : IEquatable<EnhanceTransactionResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnhanceTransactionResponse" /> class.
        /// </summary>
        /// <param name="amount">amount.</param>
        /// <param name="categorizedBy">categorizedBy.</param>
        /// <param name="category">category.</param>
        /// <param name="categoryGuid">categoryGuid.</param>
        /// <param name="describedBy">describedBy.</param>
        /// <param name="description">description.</param>
        /// <param name="extendedTransactionType">extendedTransactionType.</param>
        /// <param name="id">id.</param>
        /// <param name="isBillPay">isBillPay.</param>
        /// <param name="isDirectDeposit">isDirectDeposit.</param>
        /// <param name="isExpense">isExpense.</param>
        /// <param name="isFee">isFee.</param>
        /// <param name="isIncome">isIncome.</param>
        /// <param name="isInternational">isInternational.</param>
        /// <param name="isOverdraftFee">isOverdraftFee.</param>
        /// <param name="isPayrollAdvance">isPayrollAdvance.</param>
        /// <param name="isSubscription">isSubscription.</param>
        /// <param name="memo">memo.</param>
        /// <param name="merchantCategoryCode">merchantCategoryCode.</param>
        /// <param name="merchantGuid">merchantGuid.</param>
        /// <param name="merchantLocationGuid">merchantLocationGuid.</param>
        /// <param name="originalDescription">originalDescription.</param>
        /// <param name="type">type.</param>
        public EnhanceTransactionResponse(decimal? amount = default(decimal?), int? categorizedBy = default(int?), string category = default(string), string categoryGuid = default(string), int? describedBy = default(int?), string description = default(string), string extendedTransactionType = default(string), string id = default(string), bool? isBillPay = default(bool?), bool? isDirectDeposit = default(bool?), bool? isExpense = default(bool?), bool? isFee = default(bool?), bool? isIncome = default(bool?), bool? isInternational = default(bool?), bool? isOverdraftFee = default(bool?), bool? isPayrollAdvance = default(bool?), bool? isSubscription = default(bool?), string memo = default(string), int? merchantCategoryCode = default(int?), string merchantGuid = default(string), string merchantLocationGuid = default(string), string originalDescription = default(string), string type = default(string))
        {
            this.Amount = amount;
            this.CategorizedBy = categorizedBy;
            this.Category = category;
            this.CategoryGuid = categoryGuid;
            this.DescribedBy = describedBy;
            this.Description = description;
            this.ExtendedTransactionType = extendedTransactionType;
            this.Id = id;
            this.IsBillPay = isBillPay;
            this.IsDirectDeposit = isDirectDeposit;
            this.IsExpense = isExpense;
            this.IsFee = isFee;
            this.IsIncome = isIncome;
            this.IsInternational = isInternational;
            this.IsOverdraftFee = isOverdraftFee;
            this.IsPayrollAdvance = isPayrollAdvance;
            this.IsSubscription = isSubscription;
            this.Memo = memo;
            this.MerchantCategoryCode = merchantCategoryCode;
            this.MerchantGuid = merchantGuid;
            this.MerchantLocationGuid = merchantLocationGuid;
            this.OriginalDescription = originalDescription;
            this.Type = type;
        }

        /// <summary>
        /// Gets or Sets Amount
        /// </summary>
        [DataMember(Name = "amount", EmitDefaultValue = true)]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or Sets CategorizedBy
        /// </summary>
        [DataMember(Name = "categorized_by", EmitDefaultValue = true)]
        public int? CategorizedBy { get; set; }

        /// <summary>
        /// Gets or Sets Category
        /// </summary>
        [DataMember(Name = "category", EmitDefaultValue = true)]
        public string Category { get; set; }

        /// <summary>
        /// Gets or Sets CategoryGuid
        /// </summary>
        [DataMember(Name = "category_guid", EmitDefaultValue = true)]
        public string CategoryGuid { get; set; }

        /// <summary>
        /// Gets or Sets DescribedBy
        /// </summary>
        [DataMember(Name = "described_by", EmitDefaultValue = true)]
        public int? DescribedBy { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = true)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets ExtendedTransactionType
        /// </summary>
        [DataMember(Name = "extended_transaction_type", EmitDefaultValue = true)]
        public string ExtendedTransactionType { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = true)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets IsBillPay
        /// </summary>
        [DataMember(Name = "is_bill_pay", EmitDefaultValue = true)]
        public bool? IsBillPay { get; set; }

        /// <summary>
        /// Gets or Sets IsDirectDeposit
        /// </summary>
        [DataMember(Name = "is_direct_deposit", EmitDefaultValue = true)]
        public bool? IsDirectDeposit { get; set; }

        /// <summary>
        /// Gets or Sets IsExpense
        /// </summary>
        [DataMember(Name = "is_expense", EmitDefaultValue = true)]
        public bool? IsExpense { get; set; }

        /// <summary>
        /// Gets or Sets IsFee
        /// </summary>
        [DataMember(Name = "is_fee", EmitDefaultValue = true)]
        public bool? IsFee { get; set; }

        /// <summary>
        /// Gets or Sets IsIncome
        /// </summary>
        [DataMember(Name = "is_income", EmitDefaultValue = true)]
        public bool? IsIncome { get; set; }

        /// <summary>
        /// Gets or Sets IsInternational
        /// </summary>
        [DataMember(Name = "is_international", EmitDefaultValue = true)]
        public bool? IsInternational { get; set; }

        /// <summary>
        /// Gets or Sets IsOverdraftFee
        /// </summary>
        [DataMember(Name = "is_overdraft_fee", EmitDefaultValue = true)]
        public bool? IsOverdraftFee { get; set; }

        /// <summary>
        /// Gets or Sets IsPayrollAdvance
        /// </summary>
        [DataMember(Name = "is_payroll_advance", EmitDefaultValue = true)]
        public bool? IsPayrollAdvance { get; set; }

        /// <summary>
        /// Gets or Sets IsSubscription
        /// </summary>
        [DataMember(Name = "is_subscription", EmitDefaultValue = true)]
        public bool? IsSubscription { get; set; }

        /// <summary>
        /// Gets or Sets Memo
        /// </summary>
        [DataMember(Name = "memo", EmitDefaultValue = true)]
        public string Memo { get; set; }

        /// <summary>
        /// Gets or Sets MerchantCategoryCode
        /// </summary>
        [DataMember(Name = "merchant_category_code", EmitDefaultValue = true)]
        public int? MerchantCategoryCode { get; set; }

        /// <summary>
        /// Gets or Sets MerchantGuid
        /// </summary>
        [DataMember(Name = "merchant_guid", EmitDefaultValue = false)]
        public string MerchantGuid { get; set; }

        /// <summary>
        /// Gets or Sets MerchantLocationGuid
        /// </summary>
        [DataMember(Name = "merchant_location_guid", EmitDefaultValue = true)]
        public string MerchantLocationGuid { get; set; }

        /// <summary>
        /// Gets or Sets OriginalDescription
        /// </summary>
        [DataMember(Name = "original_description", EmitDefaultValue = true)]
        public string OriginalDescription { get; set; }

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
            sb.Append("class EnhanceTransactionResponse {\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  CategorizedBy: ").Append(CategorizedBy).Append("\n");
            sb.Append("  Category: ").Append(Category).Append("\n");
            sb.Append("  CategoryGuid: ").Append(CategoryGuid).Append("\n");
            sb.Append("  DescribedBy: ").Append(DescribedBy).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  ExtendedTransactionType: ").Append(ExtendedTransactionType).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  IsBillPay: ").Append(IsBillPay).Append("\n");
            sb.Append("  IsDirectDeposit: ").Append(IsDirectDeposit).Append("\n");
            sb.Append("  IsExpense: ").Append(IsExpense).Append("\n");
            sb.Append("  IsFee: ").Append(IsFee).Append("\n");
            sb.Append("  IsIncome: ").Append(IsIncome).Append("\n");
            sb.Append("  IsInternational: ").Append(IsInternational).Append("\n");
            sb.Append("  IsOverdraftFee: ").Append(IsOverdraftFee).Append("\n");
            sb.Append("  IsPayrollAdvance: ").Append(IsPayrollAdvance).Append("\n");
            sb.Append("  IsSubscription: ").Append(IsSubscription).Append("\n");
            sb.Append("  Memo: ").Append(Memo).Append("\n");
            sb.Append("  MerchantCategoryCode: ").Append(MerchantCategoryCode).Append("\n");
            sb.Append("  MerchantGuid: ").Append(MerchantGuid).Append("\n");
            sb.Append("  MerchantLocationGuid: ").Append(MerchantLocationGuid).Append("\n");
            sb.Append("  OriginalDescription: ").Append(OriginalDescription).Append("\n");
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
            return this.Equals(input as EnhanceTransactionResponse);
        }

        /// <summary>
        /// Returns true if EnhanceTransactionResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of EnhanceTransactionResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(EnhanceTransactionResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Amount == input.Amount ||
                    (this.Amount != null &&
                    this.Amount.Equals(input.Amount))
                ) && 
                (
                    this.CategorizedBy == input.CategorizedBy ||
                    (this.CategorizedBy != null &&
                    this.CategorizedBy.Equals(input.CategorizedBy))
                ) && 
                (
                    this.Category == input.Category ||
                    (this.Category != null &&
                    this.Category.Equals(input.Category))
                ) && 
                (
                    this.CategoryGuid == input.CategoryGuid ||
                    (this.CategoryGuid != null &&
                    this.CategoryGuid.Equals(input.CategoryGuid))
                ) && 
                (
                    this.DescribedBy == input.DescribedBy ||
                    (this.DescribedBy != null &&
                    this.DescribedBy.Equals(input.DescribedBy))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.ExtendedTransactionType == input.ExtendedTransactionType ||
                    (this.ExtendedTransactionType != null &&
                    this.ExtendedTransactionType.Equals(input.ExtendedTransactionType))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.IsBillPay == input.IsBillPay ||
                    (this.IsBillPay != null &&
                    this.IsBillPay.Equals(input.IsBillPay))
                ) && 
                (
                    this.IsDirectDeposit == input.IsDirectDeposit ||
                    (this.IsDirectDeposit != null &&
                    this.IsDirectDeposit.Equals(input.IsDirectDeposit))
                ) && 
                (
                    this.IsExpense == input.IsExpense ||
                    (this.IsExpense != null &&
                    this.IsExpense.Equals(input.IsExpense))
                ) && 
                (
                    this.IsFee == input.IsFee ||
                    (this.IsFee != null &&
                    this.IsFee.Equals(input.IsFee))
                ) && 
                (
                    this.IsIncome == input.IsIncome ||
                    (this.IsIncome != null &&
                    this.IsIncome.Equals(input.IsIncome))
                ) && 
                (
                    this.IsInternational == input.IsInternational ||
                    (this.IsInternational != null &&
                    this.IsInternational.Equals(input.IsInternational))
                ) && 
                (
                    this.IsOverdraftFee == input.IsOverdraftFee ||
                    (this.IsOverdraftFee != null &&
                    this.IsOverdraftFee.Equals(input.IsOverdraftFee))
                ) && 
                (
                    this.IsPayrollAdvance == input.IsPayrollAdvance ||
                    (this.IsPayrollAdvance != null &&
                    this.IsPayrollAdvance.Equals(input.IsPayrollAdvance))
                ) && 
                (
                    this.IsSubscription == input.IsSubscription ||
                    (this.IsSubscription != null &&
                    this.IsSubscription.Equals(input.IsSubscription))
                ) && 
                (
                    this.Memo == input.Memo ||
                    (this.Memo != null &&
                    this.Memo.Equals(input.Memo))
                ) && 
                (
                    this.MerchantCategoryCode == input.MerchantCategoryCode ||
                    (this.MerchantCategoryCode != null &&
                    this.MerchantCategoryCode.Equals(input.MerchantCategoryCode))
                ) && 
                (
                    this.MerchantGuid == input.MerchantGuid ||
                    (this.MerchantGuid != null &&
                    this.MerchantGuid.Equals(input.MerchantGuid))
                ) && 
                (
                    this.MerchantLocationGuid == input.MerchantLocationGuid ||
                    (this.MerchantLocationGuid != null &&
                    this.MerchantLocationGuid.Equals(input.MerchantLocationGuid))
                ) && 
                (
                    this.OriginalDescription == input.OriginalDescription ||
                    (this.OriginalDescription != null &&
                    this.OriginalDescription.Equals(input.OriginalDescription))
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
                if (this.Amount != null)
                    hashCode = hashCode * 59 + this.Amount.GetHashCode();
                if (this.CategorizedBy != null)
                    hashCode = hashCode * 59 + this.CategorizedBy.GetHashCode();
                if (this.Category != null)
                    hashCode = hashCode * 59 + this.Category.GetHashCode();
                if (this.CategoryGuid != null)
                    hashCode = hashCode * 59 + this.CategoryGuid.GetHashCode();
                if (this.DescribedBy != null)
                    hashCode = hashCode * 59 + this.DescribedBy.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.ExtendedTransactionType != null)
                    hashCode = hashCode * 59 + this.ExtendedTransactionType.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.IsBillPay != null)
                    hashCode = hashCode * 59 + this.IsBillPay.GetHashCode();
                if (this.IsDirectDeposit != null)
                    hashCode = hashCode * 59 + this.IsDirectDeposit.GetHashCode();
                if (this.IsExpense != null)
                    hashCode = hashCode * 59 + this.IsExpense.GetHashCode();
                if (this.IsFee != null)
                    hashCode = hashCode * 59 + this.IsFee.GetHashCode();
                if (this.IsIncome != null)
                    hashCode = hashCode * 59 + this.IsIncome.GetHashCode();
                if (this.IsInternational != null)
                    hashCode = hashCode * 59 + this.IsInternational.GetHashCode();
                if (this.IsOverdraftFee != null)
                    hashCode = hashCode * 59 + this.IsOverdraftFee.GetHashCode();
                if (this.IsPayrollAdvance != null)
                    hashCode = hashCode * 59 + this.IsPayrollAdvance.GetHashCode();
                if (this.IsSubscription != null)
                    hashCode = hashCode * 59 + this.IsSubscription.GetHashCode();
                if (this.Memo != null)
                    hashCode = hashCode * 59 + this.Memo.GetHashCode();
                if (this.MerchantCategoryCode != null)
                    hashCode = hashCode * 59 + this.MerchantCategoryCode.GetHashCode();
                if (this.MerchantGuid != null)
                    hashCode = hashCode * 59 + this.MerchantGuid.GetHashCode();
                if (this.MerchantLocationGuid != null)
                    hashCode = hashCode * 59 + this.MerchantLocationGuid.GetHashCode();
                if (this.OriginalDescription != null)
                    hashCode = hashCode * 59 + this.OriginalDescription.GetHashCode();
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
