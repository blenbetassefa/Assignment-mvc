namespace Web2.Models.NonDatabaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class ChangePassword
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required"), StringLength(60, ErrorMessage = "Only 60 characters")]
        public string NewPassword1 { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required"), StringLength(60, ErrorMessage = "Only 60 characters"), Compare("NewPassword1", ErrorMessage = "The passwords should be equal")]
        public string NewPassword2 { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        public string OldPassword { get; set; }

        public string CPErrorMessage { get; set; }
    }


}
