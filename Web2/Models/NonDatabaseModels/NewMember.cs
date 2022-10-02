using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web2.Models.NonDatabaseModels
{
    public class NewMember
    {
        [Key]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Username")]
        [StringLength(60, ErrorMessage = "Must be between 5 and 60 characters", MinimumLength = 5)]
        public string MemberUserName { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Name")]
        [StringLength(60, ErrorMessage = "Must be between 5 and 60 characters", MinimumLength = 5)]
        public string MemberName { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Gender")]
        [StringLength(20, ErrorMessage = "Must be between 3 and 20 characters", MinimumLength = 3)]
        public string MemberGender { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Date Joined")]
        public DateTime? MemberJoinedDate { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Phone")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "This field should be an phone number.")]
        public string MemberPhone { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Email")]
        [StringLength(70, ErrorMessage = "Must be between 5 and 70 characters", MinimumLength = 5)]
        [DataType(DataType.EmailAddress, ErrorMessage = "This field should be an email.")]
        public string MemberEmail { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Date of Birth")]
        public DateTime? MemberDob { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Password")]
        [StringLength(60, ErrorMessage = "Must be between 5 and 60 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Confirm Password"), Compare("Password", ErrorMessage = "This should be equal to the password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }
        public string AccountExistsErrorMessage { get; set; }

    }
}
