using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Web2.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Username")]
        [StringLength(60, ErrorMessage = "Must be between 5 and 60 characters", MinimumLength = 5)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Account Type")]
        [StringLength(30, ErrorMessage = "Must be between 5 and 30 characters", MinimumLength = 5)]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "This field is required"), Display(Name = "Password")]
        [StringLength(60, ErrorMessage = "Must be between 5 and 60 characters", MinimumLength = 5)]
        public string AccountPassword { get; set; }
    }
}
