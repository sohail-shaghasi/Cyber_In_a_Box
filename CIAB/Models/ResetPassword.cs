﻿using System.ComponentModel.DataAnnotations;
namespace CIAB.Models
{
    public class ResetPassword
    {
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Remote( "IsCurrentPasswordAvailable", "Validation", ErrorMessage = "The password does not exist")]
        public string CurrentPass { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPass { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPass", ErrorMessage = "The Password and Confirm Password do not match")]//to compare the password fields.
        [StringLength(10, MinimumLength = 5, ErrorMessage =
           "5 characters minimum, no spaces, no special characters")]
        public string ConfirmNewPassword { get; set; }
    }
}