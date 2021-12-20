using System;
using System.Runtime.Serialization;

namespace MXWidgetIntegration.Core.Model
{
    public partial class PaginationResponse : IEquatable<PaginationResponse>
    {
        public PaginationResponse(int currentPage = default(int), int perPage = default(int), int totalEntries = default(int), int totalPages = default(int))
        {
            this.CurrentPage = currentPage;
            this.PerPage = perPage;
            this.TotalEntries = totalEntries;
            this.TotalPages = totalPages;
        }
        [DataMember(Name = "current_page", EmitDefaultValue = false)]
        public int CurrentPage { get; set; }
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int PerPage { get; set; }
        [DataMember(Name = "total_entries", EmitDefaultValue = false)]
        public int TotalEntries { get; set; }
        [DataMember(Name = "total_pages", EmitDefaultValue = false)]
        public int TotalPages { get; set; }             
        public bool Equals(PaginationResponse input)
        {
            if (input == null)
                return false;

            return
                (
                    this.CurrentPage == input.CurrentPage ||
                    this.CurrentPage.Equals(input.CurrentPage)
                ) &&
                (
                    this.PerPage == input.PerPage ||
                    this.PerPage.Equals(input.PerPage)
                ) &&
                (
                    this.TotalEntries == input.TotalEntries ||
                    this.TotalEntries.Equals(input.TotalEntries)
                ) &&
                (
                    this.TotalPages == input.TotalPages ||
                    this.TotalPages.Equals(input.TotalPages)
                );
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 41;
                hashCode = hashCode * 59 + this.CurrentPage.GetHashCode();
                hashCode = hashCode * 59 + this.PerPage.GetHashCode();
                hashCode = hashCode * 59 + this.TotalEntries.GetHashCode();
                hashCode = hashCode * 59 + this.TotalPages.GetHashCode();
                return hashCode;
            }
        }        
    }
}
