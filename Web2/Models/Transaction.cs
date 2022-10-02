using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Web2.Models
{
    public partial class Transaction
    {
        public int TranId { get; set; }

        [Display(Name = "Book ID")]
        public string BookId { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Book Title")]
        [StringLength(60, ErrorMessage = "Must be between 1 and 60 characters", MinimumLength = 1)]
        public string BookTitle { get; set; }

        [Display(Name = "Book ISBN")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        public string BookIsbn { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Transaction Status")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        public string TranStatus { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Date of Transaction")]
        public DateTime TranDate { get; set; }

        [Display(Name = "Member ID")]
        public int? MemberId { get; set; }

        [Display(Name = "Member Name")]
        public string MemberName { get; set; }
    }
}
