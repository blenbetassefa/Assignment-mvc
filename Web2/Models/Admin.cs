using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Web2.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Username")]
        [StringLength(60, ErrorMessage = "Must be between 5 and 60 characters", MinimumLength = 5)]
        public string AdminUserName { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Name")]
        [StringLength(60, ErrorMessage = "Must be between 5 and 60 characters", MinimumLength = 5)]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Gender")]
        [StringLength(20, ErrorMessage = "Must be between 3 and 20 characters", MinimumLength = 3)]
        public string AdminGender { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Date Joined")]
        public DateTime? AdminJoinedDate { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Phone")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "This field should be an phone number.")]
        public string AdminPhone { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Email")]
        [StringLength(70, ErrorMessage = "Must be between 5 and 70 characters", MinimumLength = 5)]
        [DataType(DataType.EmailAddress, ErrorMessage = "This field should be an email.")]
        public string AdminEmail { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Date of Birth")]
        public DateTime? AdminDob { get; set; }

        [NotMapped]
        public string Error { get; set; }


    }
}
