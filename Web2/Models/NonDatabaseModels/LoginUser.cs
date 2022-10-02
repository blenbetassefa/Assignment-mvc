using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Web2.Models.NonDatabaseModels
{
    public partial class LoginUser
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}
